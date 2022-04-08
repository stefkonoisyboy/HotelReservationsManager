using HotelManagementSystem.Models.Hotels;
using HotelManagementSystem.Models.HotelsList;
using HotelManagementSystem.Models.IndexHotels;
using HotelManagementSystem.Models.RecommendedHotels;
using HotelManagementSystem.Models.SearchHotels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Services
{
    public interface IHotelsService
    {
        string GetHotelNameByRoomId(int roomId);

        Task<HotelsDetailsViewModel> GetById(int id);

        Task<IEnumerable<AllHotelsBySearchViewModel>> Search(SearchHotelListInputModel input);

        Task<IEnumerable<AllHotelsBySearchViewModel>> GetAll();

        Task<IEnumerable<RecommendedHotelsViewModel>> RecommendedHotels();

        Task<IEnumerable<TravelersChoiceHotelsViewModel>> TravelersChoiceHotels();

        Task<IEnumerable<HotelInHotelsListViewModel>> GetBestStarredHotelsList();

        Task<IEnumerable<HotelInHotelsListViewModel>> GetMostReviewedHotelsList();

        Task<IEnumerable<HotelInHotelsListViewModel>> GetMostReviewsStarsHotelsList();

        Task<IEnumerable<HotelInHotelsListViewModel>> CheapestHotelsList();

        Task<ICollection<SelectListItem>> GetHotelsAsSelectListItem();
    }
}
