using AutoMapper.Execution;
using Library.GenaricRepos;
using Library.Models;
using Library.Repos.InterFaces;

namespace Library.Work
{
    public interface IUnitOfWork
    {
        IGenaricRepo<Models.Member> members { get; }
        Icategory categorys { get; }
        IGenaricRepo<Book> books { get; }
        IBorrowing borrowings { get; }
        IUserRepository Users { get; }
        void Save();
    }
}
