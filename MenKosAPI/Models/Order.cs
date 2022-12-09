

using System.Text.Json.Serialization;

namespace MenKosAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime OutDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? OccupantId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Occupant? Occupant { get; set; }


        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int RoomId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Room? Room { get; set; }
    }
}
