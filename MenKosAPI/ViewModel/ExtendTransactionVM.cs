using MenKosAPI.Models;

namespace MenKosAPI.ViewModel
{
    public class ExtendTransactionVM
    {
        public IFormFile Image { get; set; }
        public string? ProofPayment { get; set; }

        public DateTime PaymentDate { get; set; }


        public DateTime EntryDate { get; set; }
        public DateTime OutDate { get; set; }


        public decimal Amount { get; set; }


        public int OccupantId { get; set; }

        public int RoomId { get; set; }

    }
}