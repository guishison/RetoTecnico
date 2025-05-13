using System;
using System.Threading.Tasks;
using RetoTecnico.Models;

namespace RetoTecnico.Services
{
    public interface IAntiFraudService
    {
        Task<string> ValidateTransaction(Transaction transaction);
    }
}