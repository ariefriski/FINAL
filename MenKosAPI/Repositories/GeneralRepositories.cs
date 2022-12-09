using MenKosAPI.Context;
using MenKosAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MenKosAPI.Repositories
{
    public class GeneralRepositories<Entity> : IRepository<Entity, int>
        where Entity : class
    {
        MyContext myContext;
        public GeneralRepositories(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Create(Entity entity)
        {
            myContext.Set<Entity>().Add(entity);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Delete(int id)
        {
            var data = Get(id);
            myContext.Set<Entity>().Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public ICollection<Entity> Get()
        {
            return myContext.Set<Entity>().ToList();
        }

        public Entity Get(int id)
        {
            return myContext.Set<Entity>().Find(id);
        }

        public int Update(Entity entity)
        {
            myContext.Set<Entity>().Update(entity);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
