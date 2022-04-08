using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Hotels;
using HotelManagementSystem.Models.HotelsList;
using HotelManagementSystem.Models.IndexHotels;
using HotelManagementSystem.Models.Photos;
using HotelManagementSystem.Models.RecommendedHotels;
using HotelManagementSystem.Models.Reviews;
using HotelManagementSystem.Models.Rooms;
using HotelManagementSystem.Models.SearchHotels;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                    AveragePrice = h.Rooms.Count() == 0 ? 0 : h.Rooms.Select(x => x.AdultPrice).Average(),
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
                    AveragePrice = h.Rooms.Count() == 0 ? 0 : h.Rooms.Select(x => x.AdultPrice).Average(),
                    MainImage = h.MainImage,
                    Stars = h.Stars,
                    Town = h.Town.Name,
                    Country = h.Country.Name,
                    Reviews = h.Reviews.Count,
                    ReviewsStars = h.Reviews.Count == 0 ? 0 : h.Reviews.Select(r => r.Rating).Average(),
                }).ToListAsync();

            return query.ToList();
        }

        public async Task<IEnumerable<HotelInHotelsListViewModel>> GetBestStarredHotelsList()
        {
            var hotelsQuery = await this.dbContext.Hotels
               .OrderByDescending(h => h.Stars)
               .Select(h => new HotelInHotelsListViewModel
               {
                   Name = h.Name,
                   Descripton = h.Descripton,
                   Price = (decimal)(h.Stars * 50 + h.Country.Name.Length + h.Town.Name.Length),
                   MainImage = h.MainImage,
                   Discount = h.Discount,
                   Stars = h.Stars,
                   Town = h.Town.Name,
                   Country = h.Country.Name,
                   Reviews = h.Reviews.Count,
                   ReviewsStars = h.Reviews.Count == 0 ? 0 : h.Reviews.Average(r => r.Rating),
               })
               .ToListAsync();

            return hotelsQuery.ToList();
        }


        public async Task<IEnumerable<HotelInHotelsListViewModel>> GetMostReviewedHotelsList()
        {
            var hotelsQuery = await this.dbContext.Hotels
               .OrderByDescending(h => h.Reviews.Count)
               .ThenByDescending(h => h.Stars)
               .Select(h => new HotelInHotelsListViewModel
               {
                   Name = h.Name,
                   Descripton = h.Descripton,
                   Price = (decimal)(h.Stars * 50 + h.Country.Name.Length + h.TownId),
                   MainImage = h.MainImage,
                   Discount = h.Discount,
                   Stars = h.Stars,
                   Town = h.Town.Name,
                   Country = h.Country.Name,
                   Reviews = h.Reviews.Count,
                   ReviewsStars = h.Reviews.Count == 0 ? 0 : h.Reviews.Average(r => r.Rating),
               })
               .ToListAsync();

            return hotelsQuery.ToList();
        }


        public async Task<IEnumerable<HotelInHotelsListViewModel>> GetMostReviewsStarsHotelsList()
        {
            var hotelsQuery = await this.dbContext.Hotels
               .Select(h => new HotelInHotelsListViewModel
               {
                   Name = h.Name,
                   Descripton = h.Descripton,
                   Price = (decimal)(h.Stars * 50 + h.Country.Name.Length + h.TownId),
                   MainImage = h.MainImage,
                   Discount = h.Discount,
                   Stars = h.Stars,
                   Town = h.Town.Name,
                   Country = h.Country.Name,
                   Reviews = h.Reviews.Count,
                   ReviewsStars = h.Reviews.Count == 0 ? 0 : h.Reviews.Average(r => r.Rating),
               })
               .OrderByDescending(h => h.ReviewsStars)
               .ThenByDescending(h => h.Reviews)
               .ThenByDescending(h => h.Stars)
               .ToListAsync();

            return hotelsQuery.ToList();
        }


        public async Task<IEnumerable<HotelInHotelsListViewModel>> CheapestHotelsList()
        {
            var hotelsQuery = await this.dbContext.Hotels
                .Select(h => new HotelInHotelsListViewModel
                {
                    Name = h.Name,
                    Descripton = h.Descripton,
                    Price = (decimal)(h.Stars * 50 + h.Country.Name.Length + h.TownId),
                    MainImage = h.MainImage,
                    Discount = h.Discount,
                    Stars = h.Stars,
                    Town = h.Town.Name,
                    Country = h.Country.Name,
                    Reviews = h.Reviews.Count,
                    ReviewsStars = h.Reviews.Count == 0 ? 0 : h.Reviews.Average(r => r.Rating),
                })
                .OrderBy(h => h.Price)
                .ThenByDescending(h => h.Reviews)
                .ThenByDescending(h => h.Stars)
                .ToListAsync();

            return hotelsQuery.ToList();
        }

        public async Task<HotelsDetailsViewModel> GetById(int id)
        {
            return await this.dbContext.Hotels
               .Where(h => h.Id == id)
               .Select(h => new HotelsDetailsViewModel
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
                   Reviews = h.Reviews
                   .Where(r => r.HotelId == id)
                   .OrderByDescending(r => r.Rating)
                   .Take(3)
                   .Select(r => new AllReviewsByHotelIdViewModel
                   {
                       Content = r.Content,
                       Id = r.Id,
                       Rating = r.Rating,
                       UserFullName = r.User.FirstName + ' ' + r.User.LastName,
                       UserProfileImage = r.User.ProfileImage,
                   })
                   .ToList(),
                   Photos = h.Photos
                   .Where(p => p.HotelId == id)
                   .OrderBy(p => p.Id)
                   .Select(p => new AllPhotosByHotelIdViewModel
                   {
                       RemoteUrl = p.RemoteUrl,
                   })
                   .ToList(),
                   Rooms = h.Rooms
                   .Where(r => r.HotelId == id && r.IsFree)
                   .OrderBy(r => r.Id)
                   .Select(r => new AllRoomsByHotelIdViewModel
                   {
                       AdultPrice = r.AdultPrice,
                       Capacity = r.Capacity,
                       ChildPrice = r.ChildPrice,
                       Description = r.Description,
                       Id = r.Id,
                       Name = r.Name,
                       MainImage = r.MainImage,
                   })
                   .ToList(),
               })
               .FirstOrDefaultAsync();
        }

        public string GetHotelNameByRoomId(int roomId)
        {
            return this.dbContext.Hotels
                .FirstOrDefault(h => h.Rooms.Any(r => r.Id == roomId))
                .Name;
        }

        public async Task<ICollection<SelectListItem>> GetHotelsAsSelectListItem()
        {
            return await this.dbContext
                .Hotels
                .Select(h => new SelectListItem()
                {
                    Text = h.Name,
                    Value = h.Id.ToString(),
                })
                .ToListAsync();
        }
    }
}
