using System.Text.Json.Serialization;

namespace Uprise.Requests;

public class RegistrationRequest
{

    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;
}
