using HotelManagementSystem.Models.Paging;

namespace HotelManagementSystem.Models.Clients
{
    public class AllClientsListViewModel : PagingViewModel
    {
        public IEnumerable<AllClientsViewModel> Clients { get; set; }

        public FirstNameAndLastNameInputModel? InputModel { get; set; }
    }
}
