using Library.Dtos;
using Library.GenaricRepos;
using Library.Models;
using Library.Repos.InterFaces;

namespace Library.Work
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenaricRepo<Member> members { get; set; }

        public Icategory categorys { get; set; }

        public IGenaricRepo<Book> books { get; set; }

        public IBorrowing borrowings { get; set; }

        public IUserRepository Users { get; set; }

        protected readonly AppDbContext appDbContext;
        public UnitOfWork(AppDbContext appDbContext , IGenaricRepo<Member> members
            , Icategory categorys, IGenaricRepo<Book> books, IUserRepository Users
            , IBorrowing borrowings)
        {
            this.appDbContext = appDbContext;
            this.books = books;
            this.borrowings = borrowings;
            this.categorys = categorys;
            this.members = members;
            this.Users = Users;
        }
        public void Save()
        {
            appDbContext.SaveChanges();
        }
    }
}
