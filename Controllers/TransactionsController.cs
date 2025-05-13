using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetoTecnico.DataBase;
using RetoTecnico.Models;
using RetoTecnico.Services;

namespace RetoTecnico.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IAntiFraudService _antiFraudService;
        private readonly IKafkaProducerService _kafkaProducerService;

        public TransactionsController(AppDbContext dbContext, IAntiFraudService antiFraudService, IKafkaProducerService kafkaProducerService)
        {
            _dbContext = dbContext;
            _antiFraudService = antiFraudService;
            _kafkaProducerService = kafkaProducerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = new Transaction
            {
                TransactionExternalId = Guid.NewGuid(),
                SourceAccountId = request.SourceAccountId,
                TargetAccountId = request.TargetAccountId,
                TransferTypeId = request.TransferTypeId,
                Value = request.Value,
                Status = "pending", // Estado inicial
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            // Validar la transacción contra el fraude
            var validationResult = await _antiFraudService.ValidateTransaction(transaction);
            transaction.Status = validationResult;
            await _dbContext.SaveChangesAsync();

            // Enviar el estado actualizado a Kafka
            var statusUpdate = new TransactionStatusUpdate
            {
                TransactionExternalId = transaction.TransactionExternalId,
                Status = transaction.Status
            };
            await _kafkaProducerService.ProduceTransactionStatus(statusUpdate);

            return CreatedAtAction(nameof(GetTransaction), new { transactionExternalId = transaction.TransactionExternalId, createdAt = transaction.CreatedAt }, new { transactionExternalId = transaction.TransactionExternalId, status = transaction.Status });
        }

        [HttpGet("{transactionExternalId}/{createdAt}")]
        public async Task<IActionResult> GetTransaction(Guid transactionExternalId, DateTime createdAt)
        {
            var transaction = await _dbContext.Transactions
                .FirstOrDefaultAsync(t => t.TransactionExternalId == transactionExternalId && t.CreatedAt == createdAt);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                transactionExternalId = transaction.TransactionExternalId,
                createdAt = transaction.CreatedAt,
                status = transaction.Status,
                value = transaction.Value // Puedes incluir más detalles si es necesario
            });
        }
    }
}