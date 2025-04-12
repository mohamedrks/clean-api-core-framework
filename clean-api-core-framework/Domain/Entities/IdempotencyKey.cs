using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class IdempotencyKey
    {
        public int Id { get; set; }

        [Required]
        public string RequestHash { get; set; }

        [Required]
        public string ResponseBody { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime? ExpiryAt { get; set; }

    }
}