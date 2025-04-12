using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        // Better way to handle this IFormFile as we had to import Microsoft.AspNetCore.Http inside application level
        // Image should be uploaded as a file
        public IFormFile? Image { get; set; }
    }
}