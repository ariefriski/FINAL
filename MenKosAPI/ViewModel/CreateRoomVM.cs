using MenKosAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MenKosAPI.ViewModel
{
    public class CreateRoomVM
    {
        //public int? Id { get; set; }
        public string? Name { get; set; }
        public int Floor { get; set; }

        public int RoomPriceId { get; set; }
        public int? RoomId { get; set; }
        public int? PaymentId { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public string Facility { get; set; }

        public IFormFile Image { get; set; }
    }
}
