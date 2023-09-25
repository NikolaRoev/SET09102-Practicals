using SQLite;
using Test.Models;

namespace Test.Data {

    public class Database {
        SQLiteAsyncConnection DatabaseConnection;

        public Database() {
            DatabaseConnection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            DatabaseConnection.CreateTableAsync<Role>().Wait();
        }

        public Database(string path) {
            DatabaseConnection = new SQLiteAsyncConnection(path, Constants.Flags);
            DatabaseConnection.CreateTableAsync<Role>().Wait();
        }

        public async Task<List<T>> GetAll<T>() where T : new() {
            return await DatabaseConnection.Table<T>().ToListAsync();
        }

        public async Task<int> AddItem<T>(T item) {
            if ((int)item.GetType().GetProperty("ID").GetValue(item) != 0)
                return await DatabaseConnection.UpdateAsync(item);
            else
                return await DatabaseConnection.InsertAsync(item);
        }

        public async Task<int> DeleteItem<T>(T item) {
            return await DatabaseConnection.DeleteAsync(item);
        }
    }

}
