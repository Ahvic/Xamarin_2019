using Newtonsoft.Json;

namespace MapAndNotes.Dtos
{
    public class UpdatePasswordRequest
    {
        [JsonProperty("old_password")]
        public string OldPassword { get; set; }
        
        [JsonProperty("new_password")]
        public string NewPassword { get; set; }
    }
}