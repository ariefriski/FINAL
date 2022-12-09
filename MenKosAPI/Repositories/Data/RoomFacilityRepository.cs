using MenKosAPI.Context;
using MenKosAPI.Models;

namespace MenKosAPI.Repositories.Data
{
    public class RoomFacilityRepository : GeneralRepositories<RoomFacility>
    {
        private MyContext _context;
        public RoomFacilityRepository(MyContext myContext) : base(myContext)
        {
            _context = myContext;
        }
    }
}
