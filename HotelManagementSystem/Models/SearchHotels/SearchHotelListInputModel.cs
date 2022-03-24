using HotelManagementSystem.Models.Amenities;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.SearchHotels
{
    public class SearchHotelListInputModel
    {
        public SearchHotelListInputModel()
        {
            this.Hotels = new List<AllHotelsBySearchViewModel>();
        }

        public IEnumerable<AllAmenitiesByNameAndIdViewModel>? AmenitiesViewModel { get; set; }

        public IEnumerable<int>? Amenities { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MinLength(1)]
        public string? Destination { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        [Required]
        public int Rooms { get; set; }

        [Required]
        public int Adults { get; set; }

        [Required]
        public int Kids { get; set; }

        public IEnumerable<AllHotelsBySearchViewModel> Hotels { get; set; }
    }
}
