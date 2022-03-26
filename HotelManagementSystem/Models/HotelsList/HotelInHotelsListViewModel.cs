namespace HotelManagementSystem.Models.HotelsList
{
    public class HotelInHotelsListViewModel
    {
        public string? Name { get; set; }

        public string? Descripton { get; set; }

        public string? MainImage { get; set; }

        public int Stars { get; set; }

        public int Discount { get; set; }

        public string? Town { get; set; }

        public string? Country { get; set; }

        public int Reviews { get; set; }

        public double ReviewsStars { get; set; }
    }
}
