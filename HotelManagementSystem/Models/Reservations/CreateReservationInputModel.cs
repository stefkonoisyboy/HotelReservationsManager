using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Models.Reservations
{
    public class CreateReservationInputModel : BaseInputModel
    {
        public int Adults { get; set; }

        public int Kids { get; set; }

        public IEnumerable<SelectListItem>? ClientItems { get; set; }

        public IEnumerable<int>? Clients { get; set; }
    }
}
