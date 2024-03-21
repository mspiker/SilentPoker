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
        
        /// <summary>
        /// Get a list of all rooms from the database
        /// </summary>
        /// <returns>List of rooms</returns>
        public async Task<List<Room>> GetRooms()
        {
            // Get all rooms from the database
            return await _data.GetRecords<Room, dynamic>(
                @"SELECT * FROM Rooms", 
                new { }, 
                _connectionString);
        }

        /// <summary>
        /// Cast a vote for a story in a room
        /// </summary>
        /// <param name="vote">The value of the vote</param>
        /// <param name="storyId">The Id of the story</param>
        /// <param name="userId">The user Id casting the vote</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a room from the database by id
        /// </summary>
        /// <param name="id">Room Id to get</param>
        /// <returns>Room retrieved</returns>
        public async Task<Room?> GetRoom(int id)
        {
            // Get a room from the database by id
            List<Member>? members = await GetMembers(id);
            var rooms = await _data.GetRecords<Room, dynamic>(
                @"SELECT * FROM Rooms WHERE Id = @Id", 
                new { Id = id }, 
                _connectionString);
            var room = rooms.FirstOrDefault();
            room.Members = members ?? new List<Member>();
            return room;
        }

        /// <summary>
        /// Get all the votes for a user from the database
        /// </summary>
        /// <param name="UserId">The user you want the vote for</param>
        /// <returns></returns>
        public async Task<List<Vote>?> GetVotesForUser(string UserId)
        {
            // Get all votes for a story from the database
            return await _data.GetRecords<Vote, dynamic>(
                @"SELECT * FROM Votes WHERE UserId = @UserId", 
                new { UserId }, 
                _connectionString);
        }

        /// <summary>
        /// Get all the votes for a story from the database
        /// </summary>
        /// <param name="StoryId">The Story Id</param>
        /// <returns></returns>
        public async Task<List<Vote>?> GetVotesForStory(string StoryId)
        {
            // Get all votes for a story from the database
            return await _data.GetRecords<Vote, dynamic>(
                @"SELECT * FROM Votes WHERE StoryId = @StoryId ORDER BY VoteValue ASC", 
                new { StoryId }, 
                _connectionString);
        }

        /// <summary>
        /// Get all the members for a room from the database
        /// </summary>
        /// <param name="RoomId">The Room Id</param>
        /// <returns></returns>
        public async Task<List<Member>?> GetMembers(int RoomId)
        {
            // Get all members for a room from the database
            return await _data.GetRecords<Member, dynamic>(
                @"SELECT * FROM Members WHERE RoomId = @RoomId", 
                new { RoomId }, 
                _connectionString);
        }

        /// <summary>
        /// Add a member to a room
        /// </summary>
        /// <param name="member">The member to add the the room</param>
        /// <returns></returns>
        public async Task AddMember(Member member)
        {
            // Add a new member to the database
            await _data.Execute(
                @"INSERT INTO Members (RoomId, UserId, Role) 
                    VALUES (@RoomId, @UserId, @Role);", 
                new { member.RoomId, member.UserId, member.Role }, 
                _connectionString);
        }

        /// <summary>
        /// Delete a member from a room
        /// </summary>
        /// <param name="userId">The User Id to be removed</param>
        /// <param name="roomId">The Room Id to have the member removed from</param>
        /// <returns></returns>
        public async Task DeleteMember(string userId, int roomId)
        {
            // Delete a member from the database
            await _data.Execute(
                @"DELETE FROM Members WHERE RoomId = @RoomId AND UserId = @UserId", 
                new { roomId, userId }, 
                _connectionString);
        }

        /// <summary>
        /// Delete a room from the database by id
        /// </summary>
        /// <param name="id">The Id of the room to remove</param>
        /// <returns></returns>
        public async Task DeleteRoom(int id)
        {
            // Delete a room from the database by id
            await _data.Execute(
                @"DELETE FROM Rooms WHERE Id = @Id", 
                new { Id = id }, 
                _connectionString);
        }
        
        /// <summary>
        /// Add a room to the database
        /// </summary>
        /// <param name="room">Room to add</param>
        /// <returns></returns>
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

        /// <summary>
        /// Update a room in the database
        /// </summary>
        /// <param name="room">The room to update</param>
        /// <returns></returns>
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
