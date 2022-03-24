using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Photos;

namespace HotelManagementSystem.Models.SearchHotels
{
    public class AllHotelsBySearchViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? MainImage { get; set; }

        public string? AccommodationType { get; set; }

        public int Stars { get; set; }

        public int Discount { get; set; }

        public string? Town { get; set; }

        public string? Country { get; set; }

        public decimal AveragePrice { get; set; }

        public int ReviewsCount { get; set; }

        public double Rating { get; set; }

        public double StarsPercent => ((double)(this.Stars / 5)) * 100;

        public double RatingPercent => ((double)(this.Rating / 5)) * 100;

        public IEnumerable<AllPhotosByHotelIdViewModel>? Photos { get; set; }

        public IEnumerable<Amenity>? Amenities { get; set; }
    }
}
