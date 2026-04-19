using System.Text.Json.Serialization;

namespace TravelPlaningProject2.Models;

public class Trip
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("destination_id")]
    public int? DestinationId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("destination_name")]
    public string? DestinationName { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    // Если API возвращает строку "2025-06-01", лучше использовать string или настроить JsonConverter
    [JsonPropertyName("date_start")]
    public DateTime DateStart { get; set; }

    [JsonPropertyName("date_end")]
    public DateTime DateEnd { get; set; }

    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    [JsonPropertyName("budget")]
    public decimal? Budget { get; set; }

    [JsonPropertyName("spent")]
    public decimal Spent { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = "planned";

    [JsonPropertyName("cover_image")]
    public string? CoverImage { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    // Связанные данные (если API их подгружает через with())
    [JsonPropertyName("expenses")]
    public List<Expense>? Expenses { get; set; }

    [JsonPropertyName("activities")]
    public List<Activity>? Activities { get; set; }
}