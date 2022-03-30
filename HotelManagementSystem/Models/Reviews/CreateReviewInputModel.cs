using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Reviews
{
    public class CreateReviewInputModel
    {
        [Required]
        [MaxLength(100)]
        public string? Content { get; set; }

        public string? UserId { get; set; }

        public int HotelId { get; set; }

        public double Rating { get; set; }
    }
}
