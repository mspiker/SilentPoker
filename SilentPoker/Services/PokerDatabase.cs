using DataLibrary;

namespace SilentPoker.Services
{
    public class PokerDatabase
    {
        private string _connectionString;
        private IDataLibrary _data;

        public PokerDatabase(string connectionString)
        {
            _data = new SQLDataLibrary();
            _connectionString = connectionString;
        }

        public async Task AddRoom(string Name, string Sprint, string Filter, bool AllowPass)
        {
            // Add a new room to the database
            IDataLibrary data = new SQLDataLibrary();
            await data.Execute(
                @"INSERT INTO Rooms (Name, Sprint, Filter, AllowPass) 
                    VALUES (@Name, @Sprint, @Filter, @AllowPass);", 
                new
                {
                    Name = @Name,
                    Sprint = @Sprint,
                    Filter = @Filter,
                    AllowPass = @AllowPass
                }, 
                _connectionString);
        }
    }
}
