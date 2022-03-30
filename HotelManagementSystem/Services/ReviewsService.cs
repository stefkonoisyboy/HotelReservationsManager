using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Reviews;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly ApplicationDbContext dbContext;

        public ReviewsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(CreateReviewInputModel input)
        {
            Review review = new Review
            {
                Content = input.Content,
                HotelId = input.HotelId,
                Rating = input.Rating,
                UserId = input.UserId,
            };

            await this.dbContext.Reviews.AddAsync(review);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllReviewsByHotelIdViewModel>> GetTop3Recent()
        {
            return await this.dbContext.Reviews
                .OrderByDescending(r => r.Rating)
                .ThenByDescending(r => r.CreatedOn)
                .Take(3)
                .Select(r => new AllReviewsByHotelIdViewModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    Rating = r.Rating,
                    UserFullName = r.User.FirstName + ' ' + r.User.LastName,
                    UserProfileImage = r.User.ProfileImage,
                })
                .ToListAsync();
        }
    }
}
