

using System.Text.Json.Serialization;

namespace MenKosAPI.Models
{
    public class RoomFacility
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? RoomId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Room? room { get; set; }
        public string Description { get; set; }

    }
}
