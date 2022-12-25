using MenKosAPI.Context;
using MenKosAPI.Helpers;
using MenKosAPI.Models;
using MenKosAPI.Response;
using MenKosAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MenKosAPI.Repositories.Data
{
    public class DetailRoomRepository
    {
        private MyContext myContext;
        private RoomRepository roomRepository;
        public DetailRoomRepository(MyContext myContext, RoomRepository roomRepository)
        {
            this.myContext = myContext;
            this.roomRepository = roomRepository;
        }

        public List<DetailRoomVM> Get()
        {
            //var detail = myContext.RoomFacilities.Include(x=> x.room).ThenInclude(pay => pay.payment).ToList();
            var Detail = myContext.Rooms.Include(x => x.Payment).ThenInclude(x => x.Order).Include(x => x.RoomPrice).ToList();
            //var detail = myContext.RoomFacilities.Include(x => additional).ToList();
            List<DetailRoomVM> list = new List<DetailRoomVM>();

            foreach (var detail in Detail)
            {
                if (detail.Payment != null)
                {
                    DetailRoomVM detaill = new()
                    {
                        Id = detail.Id,
                        Floor = detail.Floor,
                        Status = detail.Status,
                        Facility = detail.Facility,
                        PaymentId = detail.PaymentId,
                        Price = (decimal)detail.RoomPriceId,
                        //roomPrice = detail.room.RoomPriceId,
                        //payment = detail.room.PaymentId

                        Payment = new()
                        {
                            Id = detail.Payment.Id,
                            Amount = detail.Payment.Amount,
                            PaymentDate = detail.Payment.PaymentDate,
                            OrderId = detail.Payment.OrderId,
                            Order = new()
                            {
                                Id = detail.Payment.Order.Id,
                                EntryDate = detail.Payment.Order.EntryDate,
                                OutDate = detail.Payment.Order.OutDate,
                            },
                        },
                        RoomPrice = new()
                        {
                            Id = detail.RoomPrice.Id,
                            Price = detail.RoomPrice.Price,
                            RoomType = detail.RoomPrice.RoomType
                        },
                        Description = detail.Description,

                    };
                    list.Add(detaill);

                    //return list;
                }
                else
                {
                    DetailRoomVM detaill = new()
                    {
                        Id = detail.Id,
                        Floor = detail.Floor,
                        Status = detail.Status,
                        Facility = detail.Facility,
                        PaymentId = 0,
                        Price = (decimal)detail.RoomPriceId,
                        Description = detail.Description,
                        RoomPrice= detail.RoomPrice,
                        //payment = detail.room.PaymentId

                        Payment = null,
                    };

                        //return list;
                        list.Add(detaill);
                }
            }
            //list.Add(detaill);


            return list;

        }
        public DetailRoomVM GetById(int id)
        {
            //var detail = myContext.RoomFacilities.Include(x=> x.room).ThenInclude(pay => pay.payment).ToList();
            var detail = myContext.Rooms.Include(x => x.Payment).ThenInclude(x => x.Order).Include(x => x.RoomPrice).Where(x => x.Id == id).AsEnumerable().FirstOrDefault();
            //var detail = myContext.RoomFacilities.Include(x => additional).ToList();
            if (detail.Payment != null)
            {
                DetailRoomVM detaill = new()
                {
                    Id = detail.Id,
                    Floor = detail.Floor,
                    Status = detail.Status,
                    Facility = detail.Facility,
                    PaymentId = detail.PaymentId,
                    Price = (decimal)detail.RoomPriceId,
                    
                    //roomPrice = detail.room.RoomPriceId,
                    //payment = detail.room.PaymentId

                    Payment = new()
                    {
                        Id = detail.Payment.Id,
                        Amount = detail.Payment.Amount,
                        PaymentDate = detail.Payment.PaymentDate,
                        OrderId = detail.Payment.OrderId,
                        Order = new()
                        {
                            Id = detail.Payment.Order.Id,
                            EntryDate = detail.Payment.Order.EntryDate,
                            OutDate = detail.Payment.Order.OutDate,
                        },

                    },
                    RoomPrice = new()
                    {
                        Id = detail.RoomPrice.Id,
                        Price = detail.RoomPrice.Price,
                        RoomType = detail.RoomPrice.RoomType
                    },
                    Description = detail.Description,

                };
                return detaill;


            }
            else
            {
                DetailRoomVM detaill = new()
                {
                    Id = detail.Id,
                    Floor = detail.Floor,
                    Status = detail.Status,
                    Facility = detail.Facility,
                    PaymentId = 0,
                    Price = (decimal)detail.RoomPriceId,
                    //roomPrice = detail.room.RoomPriceId,
                    //payment = detail.room.PaymentId
                     Description = detail.Description,
                    Payment = null,
                };

                //return list;
                //list.Add(detaill);
            return detaill;
            }
            //return detaill;
        }
        //return myContext.Rooms.Find(id);


        public async Task<PostResponse> CreateRoom(CreateRoomVM createRoomVM)
        {
            Room room = new()
            {
                Floor = createRoomVM.Floor,
                Status = createRoomVM.Status,
                Facility = createRoomVM.Facility,
                RoomPriceId = createRoomVM.RoomPriceId,
                Description = createRoomVM.Description,
            };
                roomRepository.Create(room);
            RoomPicture roomPicture = new()
            {
                Name = createRoomVM.Name,
                RoomId = room.Id
            };
            createRoomVM.RoomId = roomPicture.RoomId;


            var postRoom = await myContext.RoomPictures.AddAsync(roomPicture); 
            var saveResponse = await myContext.SaveChangesAsync();

            if (saveResponse < 0)
            {
                return new PostResponse { Success = false, Error = "Issue while saving the post", ErrorCode = "CP01" }; ;
            }

            var postEntity = postRoom.Entity;

            var roomModel = new RoomPicture
            {
                Name = Path.Combine(postEntity.Name),
                RoomId = postEntity.RoomId
            };

            return new PostResponse { Success = true, Post = roomModel };
        }


        public async Task SaveImage(CreateRoomVM createRoomVM)
        {
            var uniqueFileName = FileHelper.GetUniqueFileName(createRoomVM.Image.FileName);


            var uploads = Path.Combine(@"D:\MetroData\TestVS\Kosan\ENDGAME\ManajemenKos-endgame\FINAL\MenKosClient\wwwroot", "users", "roomPicture");


            var filePath = Path.Combine(uploads, uniqueFileName);


            Directory.CreateDirectory(Path.GetDirectoryName(filePath));


            await createRoomVM.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));

            createRoomVM.Name = Path.Combine("/users/roomPicture/", uniqueFileName);


            return;

        }

        public int Update(Room room)
        {
            myContext.Entry(room).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }
        public int Delete(int id)
        {
            var data = myContext.Rooms.Find(id);
            if (data != null)
            {
                myContext.Remove(data);
                var result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }
    }


}
