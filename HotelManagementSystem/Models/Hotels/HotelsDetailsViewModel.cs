using HotelManagementSystem.Models.Amenities;
using HotelManagementSystem.Models.Reviews;
using HotelManagementSystem.Models.Rooms;
using HotelManagementSystem.Models.SearchHotels;

namespace HotelManagementSystem.Models.Hotels
{
    public class HotelsDetailsViewModel : AllHotelsBySearchViewModel
    {
        public IEnumerable<AllRoomsByHotelIdViewModel>? Rooms { get; set; }

        public IEnumerable<AllReviewsByHotelIdViewModel>? Reviews { get; set; }
    }
}
