using Library.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Dtos
{
    public class BookDtos
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }

        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;

        public int CategoryId { get; set; }

    }

    public class BookCreateDtos
    {
        [Required]
        public string Title { get; set; }
        [Required]

        public string Author { get; set; }
        [Required]

        public string ISBN { get; set; }
        [Required]

        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        [Required]


        public int CategoryId { get; set; }

    }
}
