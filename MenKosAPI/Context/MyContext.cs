using MenKosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace MenKosAPI.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Occupant> Occupants{ get; set; }
        public DbSet<Order> Orders{ get; set; }

        public DbSet<MenKosAPI.Models.Payment> Payments{ get; set; }

        public DbSet<Room> Rooms{ get; set; }

        public DbSet<MenKosAPI.Models.RoomPicture> RoomPictures { get; set; }

        public DbSet<User> Users  { get; set; }

        public DbSet<Complaint> Complaints { get; set; }

        public partial class Payment
        {
            public int Id { get; set; }
            public decimal Amount { get; set; }
            public DateTime PaymentDate { get; set; }

            public bool Status { get; set; }

            public string ProofPayment { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public int? OrderId { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public Order? Order { get; set; }



        }
        public partial class RoomPicture

        {

            public int Id { get; set; }

            public int RoomId { get; set; }

            public string Name { get; set; }

        }
    }
}
