using Newtonsoft.Json;

namespace MapAndNotes.Dtos
{
	public class ImageItem
	{
		[JsonProperty("id")]
		public int Id { get; set; }
	}
}