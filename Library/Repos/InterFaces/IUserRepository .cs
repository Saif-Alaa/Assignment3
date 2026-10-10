using Library.GenaricRepos;
using Library.Models;

namespace Library.Repos.InterFaces
{
    public interface IUserRepository : IGenaricRepo<User>
    {
        Task<User> GetByUserName(string userName);

        Task<bool> UserNameExists(string userName);
    }
}
