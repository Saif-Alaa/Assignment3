using Library.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Dtos
{
    public class BorrowingDtos
    {
        public int Id { get; set; }
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
     
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
    }

    public class BorrowingCreateDtos
    {
        [Required]
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
        [Required]

        public int BookId { get; set; }
        [Required]

        public int MemberId { get; set; }
    }
}
