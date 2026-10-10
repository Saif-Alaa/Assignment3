namespace Library.GenaricRepos
{
    public interface IGenaricRepo<T> where T : class
    {
        void Create(T obj);
        void Update(T obj);
        List<T> Getall();
        T GetById(int id);
        void Delete(int id);
    }
}
