namespace HotelManagementSystem.Models.Rooms
{
    public class AllRoomsViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? HotelName { get; set; }

        public string? RoomType { get; set; }

        public bool IsFree { get; set; }

        
        public int Capacity { get; set; }

        
        public decimal AdultPrice { get; set; }

        
        public decimal ChildPrice { get; set; }
    }
}
