using MenKosAPI.Context;
using MenKosAPI.Handler;
using MenKosAPI.Helpers;
using MenKosAPI.Models;
using MenKosAPI.Responses;
using MenKosAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MenKosAPI.Repositories.Data
{
    public class TransactionRepository
    {
        private MyContext _context;
        private OccupantRepository _occupantRepository;
        private UserRepository _userRepository;
        private OrderRepository _orderRepository;
        private PaymentRepository _paymentRepository;
        private RoomRepository _roomRepository;
        public TransactionRepository(MyContext myContext, OccupantRepository occupantRepository, UserRepository userRepository, OrderRepository orderRepository, PaymentRepository paymentRepository, RoomRepository roomRepository)
        {
            _context = myContext;
            _occupantRepository = occupantRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _roomRepository = roomRepository;
        }

        public List<PaymentOrderOccupantVM> Get()
        {
            var Payments = _context.Payments.Include(p => p.Order).ThenInclude(order => order.Occupant).ToList();



            List<PaymentOrderOccupantVM> transactions = new List<PaymentOrderOccupantVM>();
            //Console.WriteLine(transactions[0]);

            foreach (var payment in Payments)
            {
                PaymentOrderOccupantVM transaction = new()
                {
                    Amount = payment.Amount,
                    PaymentDate = payment.PaymentDate,
                    PaymentId = payment.Id,
                    Order = new()
                    {
                        Id = payment.Order.Id,
                        EntryDate = payment.Order.EntryDate,
                        OutDate = payment.Order.OutDate,
                        OccupantId = null,
                        RoomId = payment.Order.RoomId,
                        Occupant = new()
                        {
                            Id = payment.Order.Occupant.Id,
                            BirthDate = payment.Order.Occupant.BirthDate,
                            City = payment.Order.Occupant.City,
                            Contact = payment.Order.Occupant.Contact,
                            CreatedAt = payment.Order.Occupant.CreatedAt,
                            Gender = payment.Order.Occupant.Gender,
                            Name = payment.Order.Occupant.Name,
                            NIK = payment.Order.Occupant.NIK,
                            Religion = payment.Order.Occupant.Religion,
                            UpdateAt = payment.Order.Occupant.UpdateAt,
                        }

                    },
                    ProofPayment = payment.ProofPayment,
                    Status = payment.Status
                };

                transactions.Add(transaction);
            }

            return transactions;
        }

        //public int CreateNewTransaction(NewTransactionVM newTransaction)
        public async Task<PostResponseNewTransaction> CreateNewTransaction(NewTransactionVM newTransaction)

        {


            Occupant occupant = new()
            {
                NIK = newTransaction.NIK,
                Name = newTransaction.Name,
                BirthDate = newTransaction.BirthDate,
                City = newTransaction.City,
                Contact = newTransaction.Contact,
                Gender = newTransaction.Gender,
                Religion = newTransaction.Religion,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now
            };

            _occupantRepository.Create(occupant);



            User user = new()
            {
                Email = newTransaction.Email,
                Password = Hashing.HashPassword(newTransaction.Password),
                OccupantId = occupant.Id,
                RoleId = 3
            };

            _userRepository.Create(user);



            Order order = new()
            {
                OccupantId = occupant.Id,
                EntryDate = newTransaction.EntryDate,
                OutDate = newTransaction.OutDate,
                RoomId = newTransaction.RoomId,
            };

            _orderRepository.Create(order);




            //var payment = new MyContext.Payment
            var payment = new Payment
            {
                Amount = newTransaction.Amount,
                OrderId = order.Id,
                PaymentDate = newTransaction.PaymentDate,
                Status = true,
                ProofPayment = newTransaction.ProofPayment

            };

            var postPayment = await _context.Payments.AddAsync(payment);

            var saveResponse = await _context.SaveChangesAsync();

            if (saveResponse < 0)
            {
                return new PostResponseNewTransaction { Success = false, Error = "Issue while saving the post", ErrorCode = "CP01" }; ;
            }

            var postEntity = postPayment.Entity;

            var paymentModel = new Payment
            {
                Id = postEntity.Id,
                Amount = postEntity.Amount,
                PaymentDate = postEntity.PaymentDate,
                Status = postEntity.Status,
                ProofPayment = Path.Combine(postEntity.ProofPayment),
                OrderId = postEntity.OrderId
            };


            //Payment payment = new()
            //{
            //    Amount = newTransaction.Amount,
            //    PaymentDate = newTransaction.PaymentDate,
            //    ProofPayment = newTransaction.ProofPayment,
            //    Status = true,
            //    OrderId = order.Id
            //};

            //_paymentRepository.Create(payment);


            var room = _context.Rooms.SingleOrDefault(r => r.Id == newTransaction.RoomId);

            room.Status = true;
            room.PaymentId = payment.Id;

            var result = _roomRepository.Update(room);

            if (result > 0)
            {
                //return 1; // return Ok
            return new PostResponseNewTransaction { Success = true, Post = paymentModel };
            }
            return new PostResponseNewTransaction { Success = false, Error = "Issue while saving the post", ErrorCode = "CP01" };
            //return 2; // return BadRequest

        }

        public async Task SavePaymentNewTransaction(NewTransactionVM newTransaction)
        {
            var uniqueFileName = FileHelper.GetUniqueFileName(newTransaction.Image.FileName);


            var uploads = Path.Combine(@"C:\Users\panji\Documents\panji\MCC\EndGame\MenKosClient\wwwroot", "users", "payment", newTransaction.NIK.ToString());


            var filePath = Path.Combine(uploads, uniqueFileName);


            Directory.CreateDirectory(Path.GetDirectoryName(filePath));


            await newTransaction.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));




            newTransaction.ProofPayment = Path.Combine("/users/payment/", newTransaction.NIK.ToString() + "/", uniqueFileName);


            return;

        }

        public IEnumerable<RoomPaymentOrderOccupantVM> GetBill()
        {

            var listRoom = _context.Rooms.Include(r => r.Payment).ThenInclude(p => p.Order).ThenInclude(o => o.Occupant).Include(r => r.RoomPrice).AsEnumerable().Where(r =>
            {
                try
                {
                    var currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    var outDate = new DateTime(r.Payment.Order.OutDate.Year, r.Payment.Order.OutDate.Month, r.Payment.Order.OutDate.Day);

                    int? intervalDayofOutToCurrent = (outDate - currentDate).Days;
                    return (intervalDayofOutToCurrent >= 0 && intervalDayofOutToCurrent <= 7) && r.Status == true;   //h-7 sampai hari h
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

            });


            List<RoomPaymentOrderOccupantVM> roomPaymentOrderOccupantVMs = new List<RoomPaymentOrderOccupantVM>();

            foreach (var room in listRoom)
            {
                RoomPaymentOrderOccupantVM roomPaymentOrderOccupant = new()
                {
                    RoomId = room.Id,
                    Description = room.Description,
                    Floor = room.Floor,
                    Status = room.Status,
                    RoomPrice = room.RoomPrice,
                    Payment = new()
                    {
                        Id = room.Payment.Id,
                        Amount = room.Payment.Amount,
                        PaymentDate = room.Payment.PaymentDate,
                        ProofPayment = room.Payment.ProofPayment,
                        Status = room.Payment.Status,
                        OrderId = null,
                        Order = new()
                        {
                            Id = room.Payment.Order.Id,
                            EntryDate = room.Payment.Order.EntryDate,
                            OutDate = room.Payment.Order.OutDate,
                            OccupantId = null,
                            Occupant = new()
                            {
                                Id = room.Payment.Order.Occupant.Id,
                                Name = room.Payment.Order.Occupant.Name,
                                NIK = room.Payment.Order.Occupant.NIK,
                                BirthDate = room.Payment.Order.Occupant.BirthDate,
                                City = room.Payment.Order.Occupant.City,
                                Contact = room.Payment.Order.Occupant.Contact,
                                Gender = room.Payment.Order.Occupant.Gender,
                                Religion = room.Payment.Order.Occupant.Religion,
                                CreatedAt = room.Payment.Order.Occupant.CreatedAt,
                                UpdateAt = room.Payment.Order.Occupant.UpdateAt
                            }
                        }
                    }
                };
                roomPaymentOrderOccupantVMs.Add(roomPaymentOrderOccupant);
            }
            return roomPaymentOrderOccupantVMs;

            //return listRoom;
        }

        public RoomPaymentOrderOccupantVM GetBill(int occupantId)
        {
            try
            {
                var room = _context.Rooms.Include(r => r.Payment).ThenInclude(p => p.Order).Include(r => r.RoomPrice).Where(r => r.Payment.Order.OccupantId == occupantId).AsEnumerable().FirstOrDefault(r =>
                    {
                        try
                        {
                            var currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                            var outDate = new DateTime(r.Payment.Order.OutDate.Year, r.Payment.Order.OutDate.Month, r.Payment.Order.OutDate.Day);
                            int? intervalDayofOutToCurrent = (outDate - currentDate).Days;
                            return (intervalDayofOutToCurrent >= 0 && intervalDayofOutToCurrent <= 7);
                        }
                        catch (Exception ex)
                        {
                            return false;
                        }
                    }
                );
                RoomPaymentOrderOccupantVM roomPaymentOrderOccupant = new()
                {
                    RoomId = room.Id,
                    Description = room.Description,
                    Floor = room.Floor,
                    Status = room.Status,
                    RoomPrice = new()
                    {
                        Id = room.RoomPrice.Id,
                        Price = room.RoomPrice.Price,
                        RoomType = room.RoomPrice.RoomType
                    },
                    Payment = new()
                    {
                        Id = room.Payment.Id,
                        Amount = room.Payment.Amount,
                        PaymentDate = room.Payment.PaymentDate,
                        ProofPayment = room.Payment.ProofPayment,
                        Status = room.Payment.Status,
                        OrderId = null,
                        Order = new()
                        {
                            Id = room.Payment.Order.Id,
                            EntryDate = room.Payment.Order.EntryDate,
                            OutDate = room.Payment.Order.OutDate,
                            OccupantId = room.Payment.Order.OccupantId,
                            Occupant = null
                        }
                    }
                };
                return roomPaymentOrderOccupant;
            }
            catch
            {
                return null;
            }

        }



        //public int CreateExtendTransaction(ExtendTransactionVM extendTransaction)
        public async Task<PostResponseExtendTransaction> CreateExtendTransaction(ExtendTransactionVM extendTransaction)
        {
            Order order = new()
            {
                EntryDate = extendTransaction.EntryDate,
                OutDate = extendTransaction.OutDate,
                OccupantId = extendTransaction.OccupantId,
                RoomId = extendTransaction.RoomId,
            };


            var orderResult = _orderRepository.Create(order);
            if (orderResult < 0)
            {
                return new PostResponseExtendTransaction { Success = false, Error = "Issue while saving the post", ErrorCode = "CP01" }; ;
            }

            Payment payment = new()
            {
                Amount = extendTransaction.Amount,
                OrderId = order.Id,
                PaymentDate = extendTransaction.PaymentDate,
                ProofPayment = extendTransaction.ProofPayment,
                Status = false,
            };

            var postPayment = await _context.Payments.AddAsync(payment);

            var saveResponse = await _context.SaveChangesAsync();

            if (saveResponse < 0)
            {
                return new PostResponseExtendTransaction { Success = false, Error = "Issue while saving the post", ErrorCode = "CP01" }; ;
            }

            var postEntity = postPayment.Entity;

            var paymentModel = new Payment
            {
                Amount = postEntity.Amount,
                PaymentDate = postEntity.PaymentDate,
                Status = postEntity.Status,
                ProofPayment = Path.Combine(postEntity.ProofPayment),
                OrderId = postEntity.OrderId
            };


            //_paymentRepository.Create(payment);
            //if (orderResult > 0)
            
                return new PostResponseExtendTransaction { Success = true, Post = paymentModel }; ;
            

            //return new PostResponseExtendTransaction { Success = false, Error = "Issue while saving the post", ErrorCode = "CP01" }; ;


        }

        public async Task SavePaymentExtendTransaction (ExtendTransactionVM extendTransaction)
        {
            var uniqueFileName = FileHelper.GetUniqueFileName(extendTransaction.Image.FileName);


            var uploads = Path.Combine(@"C:\Users\panji\Documents\panji\MCC\EndGame\MenKosClient\wwwroot", "users", "extend", extendTransaction.RoomId.ToString());


            var filePath = Path.Combine(uploads, uniqueFileName);


            Directory.CreateDirectory(Path.GetDirectoryName(filePath));


            await extendTransaction.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));




            extendTransaction.ProofPayment = Path.Combine("/users/extend/", extendTransaction.RoomId.ToString() + "/", uniqueFileName);


            return;

        }
    }


}
