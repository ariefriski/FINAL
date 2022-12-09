using MenKosAPI.Models;

namespace MenKosAPI.ViewModel
{
    public class RoomPaymentOrderOccupantVM
    {


        public int RoomId { get; set; }
        public int Floor { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }

        public RoomPrice? RoomPrice { get; set; }
        public Payment? Payment { get; set; }







    }
}