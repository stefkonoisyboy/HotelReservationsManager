using HotelManagementSystem.Models.Paging;

namespace HotelManagementSystem.Models.Users
{
    public class AllUsersListViewModel : PagingViewModel
    {
        public IEnumerable<AllUsersViewModel> Users { get; set; }
    }
}
