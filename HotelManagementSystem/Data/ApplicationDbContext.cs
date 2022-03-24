using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Hotel>? Hotels { get; set; }

        public virtual DbSet<Town>? Towns { get; set; }

        public virtual DbSet<Country>? Countries { get; set; }

        public virtual DbSet<Review>? Reviews { get; set; }

        public virtual DbSet<Room>? Rooms { get; set; }

        public virtual DbSet<Photo>? Photos { get; set; }

        public virtual DbSet<Amenity>? Amenities { get; set; }
    }
}