using System.Text.Json.Serialization;

namespace MenKosAPI.Requests
{
    public class PostRequest
    {
        public int RoomId { get; set; }


        public IFormFile Image { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]


        public string? Name { get; set; }
    }
}
