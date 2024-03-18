using DataLibrary;
using Microsoft.Extensions.Configuration;
using SilentPoker.Models;

namespace SilentPoker.Services
{
    public class PokerDatabase
    {
        private string _connectionString;
        private IDataLibrary _data;

        /// <summary>
        /// Instantiates a new instance of PokerDatabase.  To configure for DI use the following:
        /// builder.Services.AddSingleton<PokerDatabase>(
        ///     _ => new PokerDatabase(builder.Configuration.GetConnectionString("default")));
        /// </summary>
        /// <param name="connectionString"></param>
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
        
        public async Task AddRoom(Room room)
        {
            // Add a new room to the database
            await _data.Execute(
                @"INSERT INTO Rooms (Name, Sprint, Filter, AllowPass) 
                    VALUES (@Name, @Sprint, @Filter, @AllowPass);", 
                new
                {
                    Name = room.Name,
                    Sprint = room.Sprint,
                    Filter = room.Filter,
                    AllowPass = room.AllowPass
                }, 
                _connectionString);
        }
    }
}
