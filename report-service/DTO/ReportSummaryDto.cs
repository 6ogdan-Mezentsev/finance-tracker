using System.Text.Json.Serialization;

namespace report_service.DTO
{
    public class ReportSummaryDto
    {
        [JsonPropertyName("totalIncome")]
        public decimal TotalIncome { get; set; }

        [JsonPropertyName("totalExpense")]
        public decimal TotalExpense { get; set; }

        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
