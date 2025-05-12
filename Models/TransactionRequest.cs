using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TuProyecto.Models
{
    public class TransactionRequest
    {
        [Required]
        [JsonPropertyName("sourceAccountId")]
        public Guid SourceAccountId { get; set; }

        [Required]
        [JsonPropertyName("targetAccountId")]
        public Guid TargetAccountId { get; set; }

        [Required]
        [JsonPropertyName("tranferTypeId")]
        public int TransferTypeId { get; set; }

        [Required]
        [JsonPropertyName("value")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor de la transacción debe ser mayor que cero.")]
        public decimal Value { get; set; }
    }
}