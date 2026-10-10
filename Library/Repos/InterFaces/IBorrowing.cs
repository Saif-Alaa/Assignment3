using Library.Dtos;
using Library.GenaricRepos;
using Library.Models;

namespace Library.Repos.InterFaces
{
    public interface IBorrowing : IGenaricRepo<Borrowing>
    {
        List<BorrowingDtos> BorrowingswithBookandmember();
    }
}
