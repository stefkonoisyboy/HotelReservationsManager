namespace HotelManagementSystem.Models.Reviews
{
    public class AllReviewsByHotelIdViewModel
    {
        public int Id { get; set; }

        public string? Content { get; set; }

        public string? UserFullName { get; set; }

        public string? UserProfileImage { get; set; }

        public double Rating { get; set; }
    }
}
