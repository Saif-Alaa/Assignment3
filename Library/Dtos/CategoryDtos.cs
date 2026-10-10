using System.ComponentModel.DataAnnotations;

namespace Library.Dtos
{
    public class CategoryDtos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
    public class CategoryCreateDto 
    {
        [Required]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }

    }
}
