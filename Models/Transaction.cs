using System;
using System.ComponentModel.DataAnnotations;

namespace TuProyecto.Models
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; }
        public Guid TransactionExternalId { get; set; }
        public Guid SourceAccountId { get; set; }
        public Guid TargetAccountId { get; set; }
        public int TransferTypeId { get; set; }
        public decimal Value { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}