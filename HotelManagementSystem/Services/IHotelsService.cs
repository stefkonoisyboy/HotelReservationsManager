using HotelManagementSystem.Models.HotelsList;
using HotelManagementSystem.Models.IndexHotels;
using HotelManagementSystem.Models.RecommendedHotels;
using HotelManagementSystem.Models.SearchHotels;

namespace HotelManagementSystem.Services
{
    public interface IHotelsService
    {
        Task<IEnumerable<AllHotelsBySearchViewModel>> Search(SearchHotelListInputModel input);

        Task<IEnumerable<AllHotelsBySearchViewModel>> GetAll();

        Task<IEnumerable<RecommendedHotelsViewModel>> RecommendedHotels();

        Task<IEnumerable<TravelersChoiceHotelsViewModel>> TravelersChoiceHotels();

        Task<IEnumerable<HotelInHotelsListViewModel>> GetHotelsList();
    }
}
