using SQLite;
using Test.Models;

namespace Test.Data
{
    public class Database
    {
        SQLiteAsyncConnection DatabaseConnection;

        public Database()
        {
            DatabaseConnection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            DatabaseConnection.CreateTableAsync<Role>().Wait();
        }

        public Database(string path) {
            DatabaseConnection = new SQLiteAsyncConnection(path, Constants.Flags);
            DatabaseConnection.CreateTableAsync<Role>().Wait();
        }

        public async Task<List<Role>> GetAll()
        {
            return await DatabaseConnection.Table<Role>().ToListAsync();
        }

        public async Task<int> AddItem(Role item)
        {
            if (item.ID != 0)
                return await DatabaseConnection.UpdateAsync(item);
            else
                return await DatabaseConnection.InsertAsync(item);
        }

        public async Task<int> DeleteItem(Role item)
        {
            return await DatabaseConnection.DeleteAsync(item);
        }
    }
}
