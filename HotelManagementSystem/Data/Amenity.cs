namespace HotelManagementSystem.Data
{
    public class Amenity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int HotelId { get; set; }

        public virtual Hotel? Hotel { get; set; }
    }
}
