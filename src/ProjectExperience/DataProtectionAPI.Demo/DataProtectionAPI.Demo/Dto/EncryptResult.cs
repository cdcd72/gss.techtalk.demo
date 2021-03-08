using System.Text.Json.Serialization;

namespace DataProtectionAPI.Demo.Dto
{
    public class EncryptResult
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
