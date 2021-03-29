using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataProtectionAPI.Demo.Dto
{
    public class CommonRequest
    {
        [Required]
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
