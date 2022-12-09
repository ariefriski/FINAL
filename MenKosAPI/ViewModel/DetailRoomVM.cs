using MenKosAPI.Models;
using System.Text.Json.Serialization;

namespace MenKosAPI.ViewModel
{
    public class DetailRoomVM
    {
        public int Id { get; set; }
        public int Floor { get; set; }
        //[JsonIgnore]
        //[JsonIgnore]
        public DateTime OutDate { get; set; }
        public Decimal Price { get; set; }
        public string Facility { get; set; }
        public int? PaymentId { get; set; }

        public bool Status { get; set; }

        public string Description { get; set; }
        public Payment? Payment { get; set; }
        public RoomPrice? RoomPrice { get; set; }
    }
}
