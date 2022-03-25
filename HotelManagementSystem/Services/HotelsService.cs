using HotelManagementSystem.Data;
using HotelManagementSystem.Models.IndexHotels;
using HotelManagementSystem.Models.RecommendedHotels;
using HotelManagementSystem.Models.SearchHotels;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Services
{
    public class HotelsService : IHotelsService
    {
        private readonly ApplicationDbContext dbContext;

        public HotelsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllHotelsBySearchViewModel>> GetAll()
        {
            var query = await this.dbContext.Hotels
               .OrderBy(h => h.Name)
               .Select(h => new AllHotelsBySearchViewModel
               {
                   Id = h.Id,
                   Name = h.Name,
                   Description = h.Descripton,
                   MainImage = h.MainImage,
                   Stars = h.Stars,
                   Discount = h.Discount,
                   AccommodationType = h.AccommodationType.ToString(),
                   Town = h.Town.Name,
                   Country = h.Country.Name,
                   ReviewsCount = h.Reviews.Count(),
                   Rating = h.Reviews.Count() == 0 ? 0 : h.Reviews.Average(r => r.Rating),
                   Amenities = h.Amenities
                   .Select(a => a)
                   .ToList(),
                   AveragePrice = h.Rooms
                   .OrderBy(r => r.AdultPrice + r.ChildPrice)
                   .Take(1)
                   .Sum(r => r.ChildPrice * 1 + r.AdultPrice * 2),
               })
               .ToListAsync();

            return query.ToList();
        }

        public async Task<IEnumerable<RecommendedHotelsViewModel>> RecommendedHotels()
        {
            var query = await this.dbContext.Hotels
                .OrderByDescending(h => h.Stars)
                .Select(h => new RecommendedHotelsViewModel
                {
                    Name = h.Name,
                    MainImage = h.MainImage,
                    Stars = h.Stars,
                    Discount = h.Discount,
                }).ToListAsync();

            return query.ToList();
        }

        public async Task<IEnumerable<AllHotelsBySearchViewModel>> Search(SearchHotelListInputModel input)
        {
            if (input.CheckIn < DateTime.UtcNow || input.CheckOut < DateTime.UtcNow)
            {
                throw new ArgumentException("Check in and check out dates must be greater than current date!");
            }

            if (input.CheckIn >= input.CheckOut)
            {
                throw new ArgumentException("Check in must be lower than check out date!");
            }

            if (input.Rooms <= 0 || input.Adults <= 0 || input.Kids <= 0)
            {
                throw new ArgumentException("Rooms, number of adults and number of kids should be positive numbers!");
            }

            int days = (input.CheckOut - input.CheckIn).Days;
            int totalPeople = input.Adults + input.Kids;

            var query = await this.dbContext.Hotels
                .Where(h => h.Rooms.Count(r => r.IsFree) >= input.Rooms && h.Rooms.Any(r => r.Capacity >= totalPeople))
                .OrderBy(h => h.Name)
                .Select(h => new AllHotelsBySearchViewModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    Description = h.Descripton,
                    MainImage = h.MainImage,
                    Stars = h.Stars,
                    Discount = h.Discount,
                    AccommodationType = h.AccommodationType.ToString(),
                    Town = h.Town.Name,
                    Country = h.Country.Name,
                    ReviewsCount = h.Reviews.Count(),
                    Rating = h.Reviews.Count() == 0 ? 0 : h.Reviews.Average(r => r.Rating),
                    Amenities = h.Amenities
                    .Select(a => a)
                    .ToList(),
                    AveragePrice = (h.Rooms
                    .Where(r => r.Capacity >= totalPeople)
                    .OrderBy(r => r.AdultPrice + r.ChildPrice)
                    .Take(input.Rooms)
                    .Sum(r => r.ChildPrice * input.Kids + r.AdultPrice * input.Adults)) * days,
                })
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(input.Destination))
            {
                query = query
               .Where(h => h.Name.ToLower().Contains(input.Destination.ToLower()))
               .ToList();
            }

            return query.ToList();
        }

        public async Task<IEnumerable<TravelersChoiceHotelsViewModel>> TravelersChoiceHotels()
        {
            var query = await this.dbContext.Hotels
                .OrderByDescending(h => h.Reviews.Count)
                .Select(h => new TravelersChoiceHotelsViewModel
                {
                    Name = h.Name,
                    Descripton = h.Descripton,
                    Discount = h.Discount,
                    MainImage = h.MainImage,
                    Stars = h.Stars,
                    Town = h.Town.Name,
                    Country = h.Country.Name,
                    Reviews = h.Reviews.Count,
                    ReviewsStars = h.Reviews.Count == 0 ? 0 : h.Reviews.Select(r => r.Rating).Average(),
                }).ToListAsync();

            return query.ToList();
        }
    }
}
