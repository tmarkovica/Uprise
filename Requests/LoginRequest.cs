using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Uprise.Requests;

public class LoginRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;
}
