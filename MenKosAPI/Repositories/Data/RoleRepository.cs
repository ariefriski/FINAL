using MenKosAPI.Context;
using MenKosAPI.Models;

namespace MenKosAPI.Repositories.Data
{
    public class RoleRepository : GeneralRepositories<Role>
    {
        private readonly MyContext _context;
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            _context = myContext;
        }
    }
}
