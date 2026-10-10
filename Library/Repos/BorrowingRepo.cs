using Library.Dtos;
using Library.GenaricRepos;
using Library.Models;
using Library.Repos.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repos
{
    public class BorrowingRepo : GenaricRepo<Borrowing>, IBorrowing
    {
        public BorrowingRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public List<BorrowingDtos> BorrowingswithBookandmember()
        {
            return appDbContext.borrowings.Include(b => b.Book).Include(m => m.Member).
                Select(e =>new BorrowingDtos
                {
                Id = e.BookId,
                MemberName = e.Member.FullName,
                MemberId = e.MemberId,
                BookName = e.Book.Title,
                BookId = e.BookId,
                BorrowDate = e.BorrowDate,
                ReturnDate = e.ReturnDate
                

                }).
                ToList();
        }
    }
}
