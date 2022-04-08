using HotelManagementSystem.Data.Enumerations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Models.Rooms
{
    public class FilterRoomsInputModel
    {
        public int Capacity { get; set; }

        public string RoomType { get; set; }

        public List<string> RoomItems => Enum.GetNames(typeof(RoomType)).ToList();

        public bool IsFree { get; set; }
    }
}
