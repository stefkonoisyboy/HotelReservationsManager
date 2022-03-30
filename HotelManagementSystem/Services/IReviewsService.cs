using HotelManagementSystem.Models.Reviews;

namespace HotelManagementSystem.Services
{
    public interface IReviewsService
    {
        Task Create(CreateReviewInputModel input);

        Task<IEnumerable<AllReviewsByHotelIdViewModel>> GetTop3Recent();
    }
}
