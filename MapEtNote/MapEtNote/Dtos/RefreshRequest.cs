using Newtonsoft.Json;

namespace MapAndNotes.Dtos
{
    public class RefreshRequest
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}