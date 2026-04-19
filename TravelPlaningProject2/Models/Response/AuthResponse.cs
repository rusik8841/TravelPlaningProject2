using System.Text.Json.Serialization;

namespace TravelPlaningProject2.Models.Responses;

public class AuthResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    [JsonPropertyName("user")]
    public User User { get; set; } = new User();
}