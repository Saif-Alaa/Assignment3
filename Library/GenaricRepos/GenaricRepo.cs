using Library.Dtos;

namespace Library.GenaricRepos
{
    public class GenaricRepo<T> : IGenaricRepo<T> where T : class
    {
        protected readonly AppDbContext appDbContext;
        public GenaricRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public void Create(T obj)
        {
            appDbContext.Set<T>().Add(obj);
        }

        public void Delete(int id)
        {
            var d = appDbContext.Set<T>().Find(id);
            appDbContext.Set<T>().Remove(d);
        }

        public T GetById(int id)
        {
            var d = appDbContext.Set<T>().Find(id);
            return d;
        }

        public List<T> Getall()
        {
           return appDbContext.Set<T>().ToList();
        }

        public void Update(T obj)
        {
            appDbContext.Set<T>().Update(obj);
        }
    }
}
