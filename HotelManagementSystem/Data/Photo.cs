namespace HotelManagementSystem.Data
{
    public class Photo
    {
        public int Id { get; set; }

        public string? Extension { get; set; }

        public string? RemoteUrl { get; set; }

        public int HotelId { get; set; }

        public virtual Hotel? Hotel { get; set; }
    }
}
