using DataLibrary;
using Microsoft.Extensions.Configuration;
using SilentPoker.Models;

namespace SilentPoker.Services
{
    public class PokerDatabase
    {
        private readonly string _connectionString;
        private readonly IDataLibrary _data;

        public PokerDatabase(string connectionString)
        {
            _data = new SQLDataLibrary();
            _connectionString = connectionString;
        }
        
        public async Task<List<Room>> GetRooms()
        {
            // Get all rooms from the database
            return await _data.GetRecords<Room, dynamic>(
                @"SELECT * FROM Rooms", 
                new { }, 
                _connectionString);
        }

        public async Task<Room?> GetRoom(int id)
        {
            // Get a room from the database by id
            var rooms = await _data.GetRecords<Room, dynamic>(
                @"SELECT * FROM Rooms WHERE Id = @Id", 
                new { Id = id }, 
                _connectionString);
            return rooms.FirstOrDefault();
        }

        public async Task DeleteRoom(int id)
        {
            // Delete a room from the database by id
            await _data.Execute(
                @"DELETE FROM Rooms WHERE Id = @Id", 
                new { Id = id }, 
                _connectionString);
        }
        
        public async Task AddRoom(Room room)
        {
            // Add a new room to the database
            await _data.Execute(
                @"INSERT INTO Rooms (Name, Sprint, Filter, AllowPass) 
                    VALUES (@Name, @Sprint, @Filter, @AllowPass);", 
                new
                {
                    room.Name,
                    room.Sprint,
                    room.Filter,
                    room.AllowPass
                }, 
                _connectionString);
        }

        public async Task UpdateRoom(Room room)
        {
            // Update a room in the database
            await _data.Execute(
                @"UPDATE Rooms 
                    SET Name = @Name, Sprint = @Sprint, Filter = @Filter, AllowPass = @AllowPass 
                    WHERE Id = @Id;", 
                new {
                    room.Id,
                    room.Name,
                    room.Sprint,
                    room.Filter,
                    room.AllowPass
                }, 
                _connectionString);
        }
    }
}
