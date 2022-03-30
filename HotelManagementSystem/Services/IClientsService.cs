using HotelManagementSystem.Models.Clients;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Services
{
    public interface IClientsService
    {
        Task<int> CreateAsync(CreateClientInputModel inputModel);

        Task DeleteAsync(int clientId);

        Task UpdateAsync(UpdateClientInputModel inputModel);


        IEnumerable<AllClientsViewModel>? GetAll();

        Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync();

        ClientViewModel? GetById(int id);
    }
}
