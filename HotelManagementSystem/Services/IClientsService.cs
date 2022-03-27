using HotelManagementSystem.Models.Clients;

namespace HotelManagementSystem.Services
{
    public interface IClientsService
    {
        Task<int> CreateAsync(CreateClientInputModel inputModel);

        Task DeleteAsync(int clientId);

        Task UpdateAsync(UpdateClientInputModel inputModel);


        IEnumerable<AllClientsViewModel>? GetAll();

        ClientViewModel? GetById(int id);
    }
}
