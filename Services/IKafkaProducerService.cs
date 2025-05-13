using RetoTecnico.Models;
using System;
using System.Threading.Tasks;

namespace RetoTecnico.Services
{
    public interface IKafkaProducerService
    {
        Task ProduceTransactionStatus(TransactionStatusUpdate statusUpdate);
    }
}