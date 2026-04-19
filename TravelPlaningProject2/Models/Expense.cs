using System.Text.Json.Serialization;

namespace TravelPlaningProject2.Models;

public class Expense
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("trip_id")]
    public int TripId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = "RUB";

    [JsonPropertyName("category")]
    public string Category { get; set; } = "other";

    [JsonPropertyName("expense_date")]
    public DateTime ExpenseDate { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}