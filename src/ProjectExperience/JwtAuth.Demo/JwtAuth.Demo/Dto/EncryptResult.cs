using System.Text.Json.Serialization;

namespace JwtAuth.Demo.Dto
{
    public class EncryptResult
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
