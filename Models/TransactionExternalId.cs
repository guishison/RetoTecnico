using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TuProyecto.Models
{
    public class TransactionStatusUpdate
    {
        [Required]
        [JsonPropertyName("transactionExternalId")]
        public Guid TransactionExternalId { get; set; }

        [Required]
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}