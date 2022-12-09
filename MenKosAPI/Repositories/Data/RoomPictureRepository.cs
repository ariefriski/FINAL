using MenKosAPI.Context;
using MenKosAPI.Models;

namespace MenKosAPI.Repositories.Data
{
    public class RoomPictureRepository : GeneralRepositories<RoomPicture>
    {
        private readonly MyContext myContext;
        public RoomPictureRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
