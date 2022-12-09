
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace MenKosAPI.Models
{
    public class Complaint
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Problem { get; set; }
        public string? Reply { get; set; }
        public bool Status { get; set; }
        //[JsonIgnore]
        public int? RoomId {get; set;}
        [JsonIgnore]
        public Room? Room{ get; set; }
    }
}
