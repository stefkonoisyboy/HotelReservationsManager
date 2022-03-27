using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Clients;
using HotelManagementSystem.Models.Reservations;
using Microsoft.AspNetCore.Authorization;

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

        public IEnumerable<AllClientsViewModel>? GetAll()
        {
            return this.dbContext.Clients?.Select(c => new AllClientsViewModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                ReservationsCount = c.Reservations.Count(),
            }).ToList();
        }
    }
}
