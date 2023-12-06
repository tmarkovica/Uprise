using System.Text.Json.Serialization;

namespace Uprise.Dto;

public class JwtDto
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = null!;
}
