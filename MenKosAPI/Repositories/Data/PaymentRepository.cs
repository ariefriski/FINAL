using MenKosAPI.Context;
using MenKosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MenKosAPI.Repositories.Data
{
    public class PaymentRepository : GeneralRepositories<Payment>
    {
        private MyContext _context;
        private OrderRepository _orderRepository;
        private RoomRepository _roomRepository;
        public PaymentRepository(MyContext myContext, OrderRepository orderRepository, RoomRepository roomRepository) : base(myContext)
        {
            _context = myContext;
            _orderRepository = orderRepository;
            _roomRepository = roomRepository;
        }

        public int UpdateStatusPayment(int paymentId)
        {
            var payment = this.Get(paymentId);
            payment.Status = true;
            int resultUpdatePayment = this.Update(payment);

            if (resultUpdatePayment <= 0)
            {
                return 2; // update gagal
            }


            var orderId = payment.OrderId;
            
            if(orderId == null)
            {
                return 2;
            }

            var order = _orderRepository.Get((int)orderId);
          

            var roomId = order.RoomId;

            var room = _roomRepository.Get(roomId);

            room.PaymentId = payment.Id;
            room.Status = true;
            int resultUpdateRoom = _roomRepository.Update(room);
            if(resultUpdateRoom <= 0)
            {
                return 2; //update gagal
            }



            return 1; // update berhasil


        }


        public List<Payment> GetPayment()
        {
            return _context.Payments.ToList();
        }

    }
}
