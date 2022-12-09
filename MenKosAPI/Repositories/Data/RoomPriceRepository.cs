using MenKosAPI.Context;
using MenKosAPI.Models;

namespace MenKosAPI.Repositories.Data
{
    public class RoomPriceRepository : GeneralRepositories<RoomPrice>
    {
        private readonly MyContext _context;
        public RoomPriceRepository(MyContext myContext) : base(myContext)
        {
            _context = myContext;
        }
    }
}
