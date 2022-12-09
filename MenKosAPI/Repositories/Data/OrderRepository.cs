using MenKosAPI.Context;
using MenKosAPI.Models;

namespace MenKosAPI.Repositories.Data
{
    public class OrderRepository : GeneralRepositories<Order>
    {
        private MyContext _context;
        public OrderRepository(MyContext myContext) : base(myContext)
        {
            _context = myContext;
        }
    }
}
