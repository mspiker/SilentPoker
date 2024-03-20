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

        public async Task CastVote(int vote, string storyId, string userId)
        {
            // Remove any existing votes for a story from the database
            await _data.Execute(
                "DELETE FROM Votes WHERE StoryId = @StoryId AND UserId = @UserId",
                new { StoryId = storyId, UserId = userId },
                _connectionString);

            // Add a new vote to the database
            await _data.Execute(
                @"INSERT INTO Votes (VoteValue, StoryId, UserId) 
                    VALUES (@VoteValue, @StoryId, @UserId);", 
                new { VoteValue = vote, StoryId = storyId, UserId = userId }, 
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

        public async Task<List<Vote>?> GetVotes(string StoryId, string UserId)
        {
            // Get all votes for a story from the database
            return await _data.GetRecords<Vote, dynamic>(
                @"SELECT * FROM Votes WHERE UserId = @UserId", 
                new { StoryId, UserId }, 
                _connectionString);
        }

        public async Task<List<Vote>?> GetVotes(string StoryId)
        {
            // Get all votes for a story from the database
            return await _data.GetRecords<Vote, dynamic>(
                @"SELECT * FROM Votes WHERE StoryId = @StoryId ORDER BY VoteValue ASC", 
                new { StoryId }, 
                _connectionString);
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
                @"INSERT INTO Rooms (Name, Sprint, Filter, AllowPass, OpenVoting) 
                    VALUES (@Name, @Sprint, @Filter, @AllowPass, @OpenVoting);", 
                new
                {
                    room.Name,
                    room.Sprint,
                    room.Filter,
                    room.AllowPass,
                    room.OpenVoting
                }, 
                _connectionString);
        }

        public async Task UpdateRoom(Room room)
        {
            // Update a room in the database
            await _data.Execute(
                @"UPDATE Rooms 
                    SET Name = @Name, Sprint = @Sprint, Filter = @Filter, AllowPass = @AllowPass, OpenVoting = @OpenVoting 
                    WHERE Id = @Id;", 
                new {
                    room.Id,
                    room.Name,
                    room.Sprint,
                    room.Filter,
                    room.AllowPass,
                    room.OpenVoting
                }, 
                _connectionString);
        }
    }
}
