using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Clients;
using HotelManagementSystem.Models.Reservations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Services
{
    public class ClientsService : IClientsService
    {
        private readonly ApplicationDbContext dbContext;

        public ClientsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ClientViewModel? GetById(int id)
        {
            if(!this.dbContext.Clients.Any(c=>c.Id == id))
            {
                throw new ArgumentException("No client with this id!");
            }
            return this.dbContext.Clients?.Select(c => new ClientViewModel()
            {
                Id = id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                IsAdult = c.IsAdult,
                Reservations = c.Reservations.ToList().Select(r => new ReservationViewModel()
                {
                    Id =  r.Id,
                    RoomName = r.Reservation.ReservedRoom.Name,
                    GeneralAmount = r.Reservation.GeneralAmount,
                    ClientsCount = r.Reservation.Clients.Count(),
                    HotelName = r.Reservation.ReservedRoom.Hotel.Name,
                    PhotoRemoteUrl = r.Reservation.ReservedRoom.Hotel.MainImage,
                }).ToList(),
            }).FirstOrDefault(c=>c.Id == id);
        }

        public async Task<int> CreateAsync(CreateClientInputModel inputModel)
        {
            Client client = new Client()
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                PhoneNumber = inputModel.PhoneNumber,
                Email = inputModel.Email,
                IsAdult = inputModel.IsAdult,
            };

            await this.dbContext.AddAsync(client);
            await this.dbContext.SaveChangesAsync();

            return client.Id;
        }

        public async Task DeleteAsync(int clientId)
        {
            Client? client = this.dbContext.Clients?.FirstOrDefault(c => c.Id == clientId);
            if(client == null)
            {
                throw new ArgumentException("Client with this id couldn't delete!");
            }

            this.dbContext.Clients?.Remove(client);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateClientInputModel inputModel)
        {
            Client? client = this.dbContext.Clients?.FirstOrDefault(c => c.Id == inputModel.Id);
            if(client == null)
            {
                throw new ArgumentException("Client couldn't update!");
            }

            client.FirstName = inputModel.FirstName;
            client.LastName = inputModel.LastName;
            client.PhoneNumber = inputModel.PhoneNumber;
            client.Email = inputModel.Email;
            client.IsAdult = inputModel.IsAdult;
            

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllClientsViewModel>> GetAllAsync(int page, int itemsPerPage)
        {
            return await this.dbContext.Clients
                .Skip((page-1)*itemsPerPage)
                .Take(itemsPerPage)
                .OrderBy(c=>c.FirstName)
                .ThenBy(c=>c.LastName)
                .Select(c => new AllClientsViewModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                ReservationsCount = c.Reservations.Count(),
            })
            .ToListAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync()
        {
            return await this.dbContext.Clients
                .OrderBy(c => c.FirstName + ' ' + c.LastName)
                .Select(c => new SelectListItem
                {
                    Text = c.FirstName + ' ' + c.LastName,
                    Value = c.Id.ToString(),
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<AllClientsViewModel>> FilterByFirstNameAndLastName(FirstNameAndLastNameInputModel inputModel, int page, int itemsPerPage = 5)
        {
            return await this.dbContext.Clients
                .Where(c => c.FirstName.Contains(inputModel.FirstName) || c.LastName.Contains(inputModel.LastName))
                .OrderBy(c => c.FirstName)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(c => new AllClientsViewModel()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    ReservationsCount = c.Reservations.Count(),
                }).ToListAsync();
        }

        public int GetClientsCount()
        {
            return this.dbContext.Clients.Count();
        }

        public int GetFilteredClientsCount(FirstNameAndLastNameInputModel inputModel)
        {
            return this.dbContext.Clients
                .Where(c => c.FirstName.Contains(inputModel.FirstName) && c.LastName.Contains(inputModel.LastName)).Count();
        }
    }
}
