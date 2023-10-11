using SQLite;
using Test.Models;

namespace Test.Data {

    /*!
     * The database class for the application.
     */
    public class Database {
        SQLiteAsyncConnection DatabaseConnection;

        /*!
         * Default constructor. Creates the production database used by the application.
         */
        public Database() : this("database.db3") {}

        /*!
         * Creates a database and all the needed tables.
         * @param path (string) The path to the database file.
         */
        public Database(string path) {
            DatabaseConnection = new SQLiteAsyncConnection(
                path,
                SQLite.SQLiteOpenFlags.ReadWrite |
                SQLite.SQLiteOpenFlags.Create |
                SQLite.SQLiteOpenFlags.SharedCache
            );
            DatabaseConnection.CreateTableAsync<Role>().Wait();
        }

        public async Task<List<T>> GetAll<T>() where T : new() {
            return await DatabaseConnection.Table<T>().ToListAsync();
        }

        /*!
         * Add or update an item to its specific table. The item must have the 'ID' property.
         * If the item needs to be added set the ID to 0, if the item needs to be updated set the ID to greater than 0.
         * @param item (T) The generic item to add or update.
         * @return The number of rows updated.
         */
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
