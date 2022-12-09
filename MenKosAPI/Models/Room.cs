

using MenKosAPI.Context;
using System.Text.Json.Serialization;

namespace MenKosAPI.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int Floor { get; set; }



        public bool Status { get; set; }

        public string Description { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? RoomPriceId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public RoomPrice? RoomPrice { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? PaymentId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Payment? Payment { get; set; }
        public string Facility { get; set; }

    }
}
