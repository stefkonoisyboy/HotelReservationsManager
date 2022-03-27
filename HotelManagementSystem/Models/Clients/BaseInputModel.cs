using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Clients
{
    public class BaseInputModel
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public bool IsAdult { get; set; }
    }
}
