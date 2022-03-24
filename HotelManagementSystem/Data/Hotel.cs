using HotelManagementSystem.Data.Enumerations;

namespace HotelManagementSystem.Data
{
    public class Hotel
    {
        public Hotel()
        {
            this.Reviews = new List<Review>();
            this.Photos = new HashSet<Photo>();
            this.Amenities = new HashSet<Amenity>();
            this.Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Descripton { get; set; }

        public string? MainImage { get; set; }

        public int Stars { get; set; }

        public int Discount { get; set; }

        public AccommodationType AccommodationType { get; set; }

        public int TownId { get; set; }

        public virtual Town? Town { get; set; }

        public int CountryId { get; set; }

        public virtual Country? Country { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<Amenity> Amenities { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
