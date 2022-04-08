using HotelManagementSystem.Models.Paging;

namespace HotelManagementSystem.Models.Rooms
{
    public class AllRoomsListViewModel : PagingViewModel 
    {
        public IEnumerable<AllRoomsViewModel> Rooms { get; set; }

        public FilterRoomsInputModel InputModel { get; set; } = new FilterRoomsInputModel();
    }
}
