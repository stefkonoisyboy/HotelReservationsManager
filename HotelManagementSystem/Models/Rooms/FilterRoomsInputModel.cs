using HotelManagementSystem.Data.Enumerations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Models.Rooms
{
    public class FilterRoomsInputModel
    {
        public int Capacity { get; set; } = 1;

        public string RoomType { get; set; } = Enum.GetNames(typeof(RoomType)).First();

        public List<string> RoomItems => Enum.GetNames(typeof(RoomType)).ToList();

        public bool IsFree { get; set; } = true;
    }
}
