using MenKosAPI.Models;

namespace MenKosAPI.ViewModel
{
    public class PaymentOrderOccupantVM
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public bool Status { get; set; }
        public string ProofPayment { get; set; }


        public Order? Order { get; set; }
    }
}
