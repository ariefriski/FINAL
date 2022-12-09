using MenKosAPI.Context;
using MenKosAPI.Models;

namespace MenKosAPI.Repositories.Data
{
    public class OccupantRepository : GeneralRepositories<Occupant>
    {
        private MyContext _context;
        public OccupantRepository(MyContext myContext) : base(myContext)
        {
            _context = myContext;
        }
    }
}
