using System.Text.Json.Serialization;

namespace TravelPlaningProject2.Models;

public class Activity
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("trip_id")]
    public int TripId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }

    [JsonPropertyName("end_time")]
    public DateTime? EndTime { get; set; }

    [JsonPropertyName("location")]
    public string? Location { get; set; }

    [JsonPropertyName("estimated_cost")]
    public decimal? EstimatedCost { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = "other";
}