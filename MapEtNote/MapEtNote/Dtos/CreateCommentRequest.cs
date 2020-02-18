using Newtonsoft.Json;

namespace MapAndNotes.Dtos
{
	public class CreateCommentRequest
	{
		[JsonProperty("text")]
		public string Text { get; set; }
	}
}