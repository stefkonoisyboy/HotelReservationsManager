using Microsoft.AspNetCore.Identity;

namespace HotelManagementSystem.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Reviews = new HashSet<Review>();
            this.CreatedReservations = new HashSet<Reservation>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileImage { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Reservation> CreatedReservations { get; set; }
    }
}
