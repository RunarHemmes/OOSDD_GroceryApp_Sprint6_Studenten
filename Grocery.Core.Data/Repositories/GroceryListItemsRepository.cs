using Grocery.Core.Data.Helpers;
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using Microsoft.Data.Sqlite;

namespace Grocery.Core.Data.Repositories
{
    public class GroceryListItemsRepository : DatabaseConnection, IGroceryListItemsRepository
    {
        private readonly List<GroceryListItem> groceryListItems;

        //public GroceryListItemsRepository()
        //{
        //    groceryListItems = [
        //        new GroceryListItem(1, 1, 1, 3),
        //        new GroceryListItem(2, 1, 2, 1),
        //        new GroceryListItem(3, 1, 3, 4),
        //        new GroceryListItem(4, 2, 1, 2),
        //        new GroceryListItem(5, 2, 2, 5),
        //    ];
        //}

        public GroceryListItemsRepository()
        {
            groceryListItems = [];
            //ISO 8601 format: date.ToString("o", CultureInfo.InvariantCulture)
            CreateTable(@"CREATE TABLE IF NOT EXISTS GroceryListItems (
                        [Id] INTEGER NOT NULL,
                        [GroceryListId] INTERGER NOT NULL,
                        [ProductId] INTEGER NOT NULL,
                        [Amount] INTEGER NOT NULL,
                        PRIMARY KEY (GrocerylistId, ProductId))");
            List<string> insertQueries = [@"INSERT OR IGNORE INTO GroceryListItems(Id, GroceryListId, ProductId, Amount) VALUES(1, 1, 1, 3)",
                                          @"INSERT OR IGNORE INTO GroceryListItems(Id, GroceryListId, ProductId, Amount) VALUES(2, 1, 2, 1)",
                                          @"INSERT OR IGNORE INTO GroceryListItems(Id, GroceryListId, ProductId, Amount) VALUES(3, 1, 3, 4)",
                                          @"INSERT OR IGNORE INTO GroceryListItems(Id, GroceryListId, ProductId, Amount) VALUES(4, 2, 1, 2)",
                                          @"INSERT OR IGNORE INTO GroceryListItems(Id, GroceryListId, ProductId, Amount) VALUES(5, 2, 2, 5)"];
            InsertMultipleWithTransaction(insertQueries);
            GetAll();
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int id)
        {
            return groceryListItems.Where(g => g.GroceryListId == id).ToList();
        }

        public List<GroceryListItem> GetAll()
        {
            groceryListItems.Clear();
            string selectQuery = "SELECT Id, GroceryListId, ProductId, Amount FROM GroceryListItems";
            OpenConnection();
            using (SqliteCommand command = new(selectQuery, Connection))
            {
                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int groceryListId = reader.GetInt32(1);
                    int productId = reader.GetInt32(2);
                    int amount = reader.GetInt32(3);
                    groceryListItems.Add(new(id, groceryListId, productId, amount));
                }
            }
            CloseConnection();
            return groceryListItems;
        }

        public GroceryListItem Add(GroceryListItem item)
        {
            //int recordsAffected;
            string insertQuery = $"INSERT INTO GroceryListItems(Id, GroceryListId, ProductId, Amount) VALUES(@Id, @GroceryListId, @ProductId, @Amount) Returning RowId;";
            OpenConnection();
            using (SqliteCommand command = new(insertQuery, Connection))
            {
                command.Parameters.AddWithValue("Id", item.Id);
                command.Parameters.AddWithValue("GroceryListId", item.GroceryListId);
                command.Parameters.AddWithValue("ProductId", item.ProductId);
                command.Parameters.AddWithValue("Amount", item.Amount);

                //recordsAffected = command.ExecuteNonQuery();
                //item.Id = Convert.ToInt32(command.ExecuteScalar());
                command.ExecuteScalar();
            }
            CloseConnection();
            return item;
        }

        public GroceryListItem? Delete(GroceryListItem item)
        {
            string deleteQuery = $"DELETE FROM GroceryListItems WHERE Id = {item.Id};";
            OpenConnection();
            Connection.ExecuteNonQuery(deleteQuery);
            CloseConnection();
            return item;
        }

        public GroceryListItem? Get(int id)
        {
            string selectQuery = $"SELECT Id, GroceryListId, ProductId, Amount FROM GroceryListItems WHERE Id = {id}";
            GroceryListItem? item = null;
            OpenConnection();
            using (SqliteCommand command = new(selectQuery, Connection))
            {
                SqliteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int Id = reader.GetInt32(0);
                    int groceryListId = reader.GetInt32(1);
                    int productId = reader.GetInt32(2);
                    int amount = reader.GetInt32(3);
                    item = (new(Id, groceryListId, productId, amount));
                }
            }
            CloseConnection();
            return item;
        }

        public GroceryListItem? Update(GroceryListItem item)
        {
            int recordsAffected;
            string updateQuery = $"UPDATE GroceryListItems SET GroceryListId = @GroceryListId, ProductId = @ProductId, Amount = @Amount  WHERE Id = {item.Id};";
            OpenConnection();
            using (SqliteCommand command = new(updateQuery, Connection))
            {
                command.Parameters.AddWithValue("GroceryListId", item.GroceryListId);
                command.Parameters.AddWithValue("ProductId", item.ProductId);
                command.Parameters.AddWithValue("Amount", item.Amount);

                recordsAffected = command.ExecuteNonQuery();
            }
            CloseConnection();
            return item;
        }
    }
}
