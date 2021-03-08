using System.Text.Json.Serialization;

namespace DataProtectionAPI.Demo.Dto
{
    public class DecryptResult
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
