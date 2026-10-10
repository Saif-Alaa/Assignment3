using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Member
    {
        [Key]
      public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [EmailAddress , Required]
        public string Email { get; set; }
        [Phone , Required]
        public string Phone { get; set; }
        public List<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    }
}
