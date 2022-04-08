using HotelManagementSystem.Data.Enumerations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Rooms
{
    public abstract class BaseInputModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? MainImage { get; set; }

        public ICollection<string> RoomTypes => Enum.GetNames(typeof(RoomType)).ToList();

        public ICollection<SelectListItem>? HotelItems { get; set; }

        [Required]
        public int HotelId { get; set; }

        [Required]
        public string? RoomType { get; set; }

        [Required]
        public bool IsFree { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public decimal AdultPrice { get; set; }

        [Required]
        public decimal ChildPrice { get; set; }

    }
}
