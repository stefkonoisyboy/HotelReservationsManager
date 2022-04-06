using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Clients;
using HotelManagementSystem.Models.Reservations;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly ApplicationDbContext dbContext;

        public ReservationsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(int roomId, string creatorId, CreateReservationInputModel input)
        {
            if (input.AccommodationDate < DateTime.UtcNow || input.ExemptionDate < DateTime.UtcNow)
            {
                throw new ArgumentException("Accommodation and exemption dates must be greater than current date!");
            }

            if (input.AccommodationDate >= input.ExemptionDate)
            {
                throw new ArgumentException("Accommodation must be lower than exemption date!");
            }

            if (input.Adults < 0 || input.Kids < 0 || input.Adults + input.Kids == 0)
            {
                throw new ArgumentException("Number of adults and number of kids should be positive numbers!");
            }

            int totalPeopleToBeAccommodated = input.Adults + input.Kids;
            if (input.Clients.Count() != totalPeopleToBeAccommodated)
            {
                throw new ArgumentException("Number of people should match the selected people!");
            }

            Room room = await this.dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
            room.IsFree = false;

            if (room.Capacity < totalPeopleToBeAccommodated)
            {
                throw new ArgumentException("No enough space for people to be accommodated!");
            }

            int discount = dbContext.Hotels.FirstOrDefault(h => h.Id == room.HotelId).Discount;
            decimal generalAmount = input.Adults * room.AdultPrice + input.Kids * room.ChildPrice;

            if (discount > 0)
            {
                decimal percent = (decimal)discount / 100;
                generalAmount = generalAmount - generalAmount * percent;
            }

            if (input.IsBreakfastIncluded == "yes")
            {
                generalAmount += 50;
            }

            if (input.IsAllInclusive == "yes")
            {
                generalAmount += 50;
            }

            Reservation reservation = new Reservation
            {
                ReservedRoomId = roomId,
                CreatorId = creatorId,
                AccomodationDate = input.AccommodationDate,
                ExemptionDate = input.ExemptionDate,
                IsBreakfastIncluded = input.IsBreakfastIncluded == "yes",
                IsAllInclusive = input.IsAllInclusive == "yes",
                GeneralAmount = generalAmount,
            };

            await this.dbContext.Reservations.AddAsync(reservation);
            await this.dbContext.SaveChangesAsync();

            foreach (var client in input.Clients)
            {
                ClientReservation clientReservation = new ClientReservation
                {
                    ClientId = client,
                    ReservationId = reservation.Id,
                };

                await this.dbContext.ClientReservations.AddAsync(clientReservation);
            }

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAllExpiredAsync()
        {
            IEnumerable<Reservation> reservations = await this.dbContext.Reservations
                .Where(r => r.ExemptionDate < DateTime.UtcNow)
                .ToListAsync();

            foreach (Reservation reservation in reservations)
            {
                Room room = await this.dbContext.Rooms
                    .Where(r => r.Id == reservation.ReservedRoomId)
                    .FirstOrDefaultAsync();

                room.IsFree = true;
            }

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Reservation reservation = await this.dbContext.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            this.dbContext.Reservations.Remove(reservation);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReservationViewModel>> GetAllAsync(int page, int itemsPerPage = 10)
        {
            return await this.dbContext.Reservations
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .OrderBy(r => r.Id)
                .Select(r => new ReservationViewModel
                {
                    Id = r.Id,
                    AccommodationDate = r.AccomodationDate,
                    GeneralAmount = r.GeneralAmount,
                    IsAllInclusive = r.IsAllInclusive == true ? "Yes" : "No",
                    ExemptionDate = r.ExemptionDate,
                    HotelName = r.ReservedRoom.Hotel.Name,
                    IsBreakfastIncluded = r.IsBreakfastIncluded == true ? "Yes" : "No",
                    PhotoRemoteUrl = r.ReservedRoom.MainImage,
                    RoomName = r.ReservedRoom.Name,
                    Clients = r.Clients
                    .OrderBy(c => c.Client.FirstName + ' ' + c.Client.LastName)
                    .Select(c => new AllClientsByReservationViewModel
                    {
                        Id = c.ClientId,
                        FirstName = c.Client.FirstName,
                        LastName = c.Client.LastName,
                    })
                    .ToList(),
                })
                .ToListAsync();
        }

        public int GetAllReservationsCount()
        {
            return this.dbContext.Reservations
                    .Count();
        }

        public async Task<ReservationViewModel> GetByIdAsync(int id)
        {
            return await this.dbContext.Reservations
                .Where(r => r.Id == id)
                .Select(r => new ReservationViewModel
                {
                    Id = r.Id,
                    AccommodationDate = r.AccomodationDate,
                    GeneralAmount = r.GeneralAmount,
                    IsAllInclusive = r.IsAllInclusive == true ? "Yes" : "No",
                    ExemptionDate = r.ExemptionDate,
                    HotelName = r.ReservedRoom.Hotel.Name,
                    IsBreakfastIncluded = r.IsBreakfastIncluded == true ? "Yes" : "No",
                    PhotoRemoteUrl = r.ReservedRoom.MainImage,
                    RoomName = r.ReservedRoom.Name,
                    Clients = r.Clients
                    .OrderBy(c => c.Client.FirstName + ' ' + c.Client.LastName)
                    .Select(c => new AllClientsByReservationViewModel
                    {
                        Id = c.ClientId,
                        FirstName = c.Client.FirstName,
                        LastName = c.Client.LastName,
                    })
                    .ToList(),
                })
                .FirstOrDefaultAsync();
        }

        public async Task<EditReservationInputModel> GetByIdForUpdateAsync(int id)
        {
            return await this.dbContext.Reservations
                .Where(r => r.Id == id)
                .Select(r => new EditReservationInputModel
                {
                    Id = r.Id,
                    AccommodationDate = r.AccomodationDate,
                    ExemptionDate = r.ExemptionDate,
                    IsAllInclusive = r.IsAllInclusive == true ? "Yes" : "No",
                    IsBreakfastIncluded = r.IsBreakfastIncluded == true ? "Yes" : "No",
                })
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(EditReservationInputModel input)
        {
            if (input.AccommodationDate < DateTime.UtcNow || input.ExemptionDate < DateTime.UtcNow)
            {
                throw new ArgumentException("Accommodation and exemption dates must be greater than current date!");
            }

            if (input.AccommodationDate >= input.ExemptionDate)
            {
                throw new ArgumentException("Accommodation must be lower than exemption date!");
            }

            Reservation reservation = await this.dbContext.Reservations.FirstOrDefaultAsync(r => r.Id == input.Id);

            reservation.AccomodationDate = input.AccommodationDate;
            reservation.ExemptionDate = input.ExemptionDate;
            reservation.IsAllInclusive = input.IsAllInclusive == "yes" ? true : false;
            reservation.IsBreakfastIncluded = input.IsBreakfastIncluded == "yes" ? true : false;

            await this.dbContext.SaveChangesAsync();
        }
    }
}
