using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Enumerations;
using HotelManagementSystem.Models.Rooms;

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

        public UpdateRoomInputModel GetByIdForUpdate(int id)
        {
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
                RoomType = c.RoomType
                
            });
        }

        public string GetRoomName(int id)
        {
            return this.dbContext.Rooms
                .FirstOrDefault(r => r.Id == id)
                .Name;
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
