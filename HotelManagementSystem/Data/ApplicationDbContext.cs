using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
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

        public virtual DbSet<Client>? Clients{ get; set; }

        public virtual DbSet<ClientReservation>? ClientReservations { get; set; }

        public virtual DbSet<Reservation>? Reservations { get; set; }

    }
}