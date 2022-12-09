using MenKosAPI.Context;
using MenKosAPI.Models;

namespace MenKosAPI.Repositories.Data
{
    public class RoomRepository : GeneralRepositories<Room>
    {
        private readonly MyContext _context;
        public RoomRepository(MyContext myContext) : base(myContext)
        {
            _context = myContext;
        }
    }
}
