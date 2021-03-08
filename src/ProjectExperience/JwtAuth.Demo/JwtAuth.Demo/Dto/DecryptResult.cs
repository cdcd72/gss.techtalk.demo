using System.Text.Json.Serialization;

namespace JwtAuth.Demo.Dto
{
    public class DecryptResult
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
