using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Clients
{
    public class FirstNameAndLastNameInputModel
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }
    }
}
