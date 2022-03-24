namespace HotelManagementSystem.Data
{
    public class Review
    {
        public int Id { get; set; }

        public string? Content { get; set; }

        public double Rating { get; set; }

        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public int HotelId { get; set; }

        public virtual Hotel? Hotel { get; set; }
    }
}
