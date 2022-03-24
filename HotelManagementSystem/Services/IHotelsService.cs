using HotelManagementSystem.Models.SearchHotels;

namespace HotelManagementSystem.Services
{
    public interface IHotelsService
    {
        Task<IEnumerable<AllHotelsBySearchViewModel>> Search(SearchHotelListInputModel input);

        Task<IEnumerable<AllHotelsBySearchViewModel>> GetAll();
    }
}
