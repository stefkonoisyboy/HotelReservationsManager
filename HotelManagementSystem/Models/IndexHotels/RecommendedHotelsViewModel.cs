namespace HotelManagementSystem.Models.RecommendedHotels
{
    public class RecommendedHotelsViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? MainImage { get; set; }

        public int Stars { get; set; }

        public decimal AveragePrice { get; set; }
    }
}
