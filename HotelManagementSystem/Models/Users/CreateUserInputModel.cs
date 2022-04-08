using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Users
{
    public class CreateUserInputModel : BaseInputModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string EGN { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        public string ProfileImage { get; set; }
    }
}
