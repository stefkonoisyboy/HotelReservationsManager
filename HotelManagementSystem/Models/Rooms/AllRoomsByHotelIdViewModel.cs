namespace HotelManagementSystem.Models.Rooms
{
    public class AllRoomsByHotelIdViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string MainImage { get; set; }

        public string? Description { get; set; }

        public int Capacity { get; set; }

        public decimal ChildPrice { get; set; }

        public decimal AdultPrice { get; set; }
    }
}
