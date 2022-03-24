namespace HotelManagementSystem.Data
{
    public class Town
    {
        public Town()
        {
            this.Hotels = new HashSet<Hotel>();
        }

        public int Id { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
