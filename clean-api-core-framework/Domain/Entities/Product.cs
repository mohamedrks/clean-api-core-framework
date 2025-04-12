using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        //[Required]
        [MaxLength(100)]
        public string? ImageName { get; set; }

        // New: Image stored as a BLOB (byte array)
        public byte[]? ImageData { get; set; }

        // Optional: MIME type for serving the image (e.g., "image/png")
        [MaxLength(50)]
        public string? ImageContentType { get; set; }

        //public bool IsActive { get; set; } = true;

        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //public DateTime? UpdatedAt { get; set; }

        [NotMapped] // temp file path used only during creation
        public string ImageTempPath { get; set; }

    }

}