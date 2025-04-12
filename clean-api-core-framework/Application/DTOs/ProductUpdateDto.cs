using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ProductUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        // Better way to handle this IFormFile as we had to import Microsoft.AspNetCore.Http inside application level
        // Optional: New image to replace the old one
        public IFormFile Image { get; set; }
    }
}