using Microsoft.AspNetCore.Identity;

namespace HotelManagementSystem.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Reviews = new HashSet<Review>();
        }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
