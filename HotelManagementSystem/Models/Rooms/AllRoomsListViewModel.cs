using HotelManagementSystem.Models.Paging;

namespace HotelManagementSystem.Models.Rooms
{
    public class AllRoomsListViewModel : PagingViewModel 
    {
        public IEnumerable<AllRoomsViewModel> Rooms { get; set; }

        public FilterRoomsInputModel FilterInputModel { get; set; } = new FilterRoomsInputModel();
    }
}
