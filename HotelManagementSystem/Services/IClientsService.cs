using HotelManagementSystem.Models.Clients;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Services
{
    public interface IClientsService
    {
        Task<int> CreateAsync(CreateClientInputModel inputModel);

        Task DeleteAsync(int clientId);

        Task UpdateAsync(UpdateClientInputModel inputModel);


        Task<IEnumerable<AllClientsViewModel>> GetAllAsync(int page, int itemsPerPage = 5);

        int GetClientsCount();

        int GetFilteredClientsCount(FirstNameAndLastNameInputModel inputModel);

        Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync();

        ClientViewModel? GetById(int id);

        Task<IEnumerable<AllClientsViewModel>> FilterByFirstNameAndLastName(FirstNameAndLastNameInputModel inputModel, int page, int itemsPerPage = 5);
    }
}
