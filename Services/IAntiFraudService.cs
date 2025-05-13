using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RetoTecnico.DataBase;
using RetoTecnico.Models;

namespace RetoTecnico.Services
{
    public class AntiFraudService : IAntiFraudService
    {
        private readonly AppDbContext _dbContext;

        public AntiFraudService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> ValidateTransaction(Transaction transaction)
        {
            if (transaction.Value > 2000)
            {
                return "rejected";
            }

            var today = DateTime.UtcNow.Date;
            var dailyAccumulated = await _dbContext.Transactions
                .Where(t => t.SourceAccountId == transaction.SourceAccountId && t.CreatedAt.Date == today)
                .SumAsync(t => t.Value);

            if (dailyAccumulated + transaction.Value > 20000)
            {
                return "rejected";
            }

            return "approved";
        }
    }
}