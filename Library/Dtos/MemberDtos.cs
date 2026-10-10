using System.ComponentModel.DataAnnotations;

namespace Library.Dtos
{
    public class MemberDtos
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }

    }

    public class MemberCreate 
    {


        [Required]
        public string FullName { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
        [Phone, Required]
        public string Phone { get; set; }
    }
}
