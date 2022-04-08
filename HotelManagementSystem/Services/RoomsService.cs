using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Enumerations;
using HotelManagementSystem.Models.Rooms;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Services
{
    public class RoomsService : IRoomsService
    {
        private readonly ApplicationDbContext dbContext;

        public RoomsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(CreateRoomInputModel inputModel)
        {
            Room room = new Room()
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                MainImage = inputModel.MainImage,
                RoomType = (RoomType)Enum.Parse(typeof(RoomType), inputModel.RoomType),
                IsFree = inputModel.IsFree,
                Capacity = inputModel.Capacity,
                AdultPrice = inputModel.AdultPrice,
                ChildPrice = inputModel.ChildPrice,
                HotelId = inputModel.HotelId,
            };

            await this.dbContext.Rooms.AddAsync(room);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Room room = await this.dbContext.Rooms.FindAsync(id);

            if(room == null)
            {
                throw new ArgumentException("No room with such id.");
            }
            
            this.dbContext.Rooms.Remove(room);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllRoomsViewModel>> GetAll(int page, int itemsPerPage = 5)
        {
            return await this.dbContext
                .Rooms
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(r => new AllRoomsViewModel()
                {
                    Name = r.Name,
                    HotelName = r.Hotel.Name,
                    RoomType = r.RoomType.ToString(),
                    IsFree =r.IsFree,
                    Capacity = r.Capacity,
                    AdultPrice = r.AdultPrice,
                    ChildPrice = r.ChildPrice,

                }).ToListAsync();
        }

        public async Task<IEnumerable<AllRoomsViewModel>> GetAllFiltered(int page, FilterRoomsInputModel inputModel, int itemsPerPage = 5)
        {
            return await this.dbContext
                .Rooms.Where(r => r.Capacity == inputModel.Capacity ||
                r.RoomType == (RoomType)Enum.Parse(typeof(RoomType), inputModel.RoomType) ||
                r.IsFree == r.IsFree)
               .Skip((page - 1) * itemsPerPage)
               .Take(itemsPerPage)
               .Select(r=>new AllRoomsViewModel()
               {
                   Name = r.Name,
                   HotelName = r.Hotel.Name,
                   RoomType = r.RoomType.ToString(),
                   IsFree = r.IsFree,
                   Capacity = r.Capacity,
                   AdultPrice = r.AdultPrice,
                   ChildPrice = r.ChildPrice,
               })    
               .ToListAsync();
        }

        public UpdateRoomInputModel GetByIdForUpdate(int id)
        {
            Room room = this.dbContext.Rooms.FirstOrDefault(r => r.Id == id);

            return this.dbContext.Rooms.Select(c => new UpdateRoomInputModel()
            {
                Id = c.Id,
                AdultPrice=c.AdultPrice,
                ChildPrice = c.ChildPrice,
                Capacity = c.Capacity,
                Description = c.Description,
                HotelId = c.HotelId,
                IsFree = c.IsFree,
                MainImage = c.MainImage,
                Name = c.Name,
                RoomType = Enum.GetName<RoomType>(c.RoomType),
            }).FirstOrDefault(r => r.Id == id);
        }

        public string GetRoomName(int id)
        {
            return this.dbContext.Rooms
                .FirstOrDefault(r => r.Id == id)
                .Name;
        }

        public int GetRoomsCount()
        {
            return this.dbContext.Rooms.Count();
        }

        public int GetFilteredRoomsCount(FilterRoomsInputModel inputModel)
        {
            return this.dbContext.Rooms.Where(r => r.Capacity == inputModel.Capacity ||
                r.RoomType == (RoomType)Enum.Parse(typeof(RoomType), inputModel.RoomType) ||
                r.IsFree == r.IsFree).Count();
        }

        public async Task UpdateAsync(UpdateRoomInputModel inputModel)
        {
            Room room = await this.dbContext.Rooms.FindAsync(inputModel.Id);
            room.Name = inputModel.Name;
            room.Description = inputModel.Description;
            room.RoomType =(RoomType)Enum.Parse(typeof(RoomType),inputModel.RoomType);
            room.Capacity = inputModel.Capacity;
            room.AdultPrice = inputModel.AdultPrice;
            room.ChildPrice = inputModel.ChildPrice;
            room.MainImage = inputModel.MainImage;
            room.IsFree = inputModel.IsFree;

            this.dbContext.Rooms.Update(room);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
