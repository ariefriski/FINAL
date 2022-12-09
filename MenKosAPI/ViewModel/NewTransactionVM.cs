using MenKosAPI.Models;

namespace MenKosAPI.ViewModel
{
    public class NewTransactionVM
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string NIK { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string Religion { get; set; }
        public DateTime BirthDate { get; set; }

        public IFormFile? Image { get; set; }
        public string? ProofPayment { get; set; }

        public DateTime PaymentDate { get; set; }


        public DateTime EntryDate { get; set; }
        public DateTime OutDate { get; set; }


        public decimal Amount { get; set; }


        public int RoomId { get; set; }

    }
}