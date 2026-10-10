using Library.Dtos;
using Library.GenaricRepos;
using Library.Models;
using Library.Repos.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repos
{

    public class UserRepository : GenaricRepo<User>, IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<User?> GetByUserName(string userName)
        {
            return await context.users
                .FirstOrDefaultAsync(u => u.Username == userName);
        }

        public async Task<bool> UserNameExists(string userName)
        {
            return await context.users
                .AnyAsync(u => u.Username == userName);
        }
    }
}
