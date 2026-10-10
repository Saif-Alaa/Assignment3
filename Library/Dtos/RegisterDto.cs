using System.ComponentModel.DataAnnotations;

namespace Library.Dtos
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;
    }
}
