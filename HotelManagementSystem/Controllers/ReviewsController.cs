using HotelManagementSystem.Models.Reviews;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelManagementSystem.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewsService reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(IFormCollection form)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string content = form["Content"].ToString();
            int hotelId = int.Parse(form["HotelId"]);
            double rating = double.Parse(form["Rating"]);

            CreateReviewInputModel input = new CreateReviewInputModel
            {
                UserId = userId,
                Content = content,
                HotelId = hotelId,
                Rating = rating,
            };

            await this.reviewsService.Create(input);
            this.TempData["Message"] = "Added Review successfully!";

            return this.Redirect($"/Home/Index");
        }
    }
}
