using Newtonsoft.Json;

namespace SampleAuthWebApp.Authorization
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonProperty("expiresAt")]
        public DateTime ExpiresAt { get; set; }
    }
}
