using iQuest.VendingMachine.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace iQuest.VendingMachine.DataAccessLayer
{
    internal class SQLiteProductRepository : IProductRepository
    {
        private readonly SQLiteConnection connection;
        public SQLiteProductRepository(string connectionString) 
        {
            connection= new SQLiteConnection(connectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);

            cmd.CommandText = "DROP TABLE IF EXISTS Product";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE Product(ColumnId INTEGER PRIMARY KEY,
            Name TEXT, Price INT, Quantity INT)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Product(ColumnId, Name, Price, Quantity) VALUES(1, 'Soda', 1, 1)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Product(ColumnId, Name, Price, Quantity) VALUES(2, 'Chips', 1.50, 10)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Product(ColumnId, Name, Price, Quantity) VALUES(3, 'Chocolate', 2, 10)";
            cmd.ExecuteNonQuery();
        }
        
        public IEnumerable<Product> GetAll()
        {
            string selectCommand = "SELECT * FROM Product";
            using var cmd = new SQLiteCommand(selectCommand,connection);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            List<Product> products = new List<Product>();

            while (rdr.Read())
            {
               products.Add(new Product(rdr.GetInt32(0),rdr.GetString(1),rdr.GetFloat(2), rdr.GetInt32(3)));
            }
            
            return products;
        }

        public void AddProduct(Product product)
        {
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO Product(ColumnId, Name, Price, Quantity) VALUES(@columnId, @name, @price, @quantity)";

            cmd.Parameters.AddWithValue("@columnId", product.ColumnId);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@quantity", product.Quantity);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public Product GetProductByColumnId(int columnId)
        {
            string selectCommand = "SELECT * FROM Product WHERE ColumnId = @columnId";
            using var cmd = new SQLiteCommand(selectCommand,connection);
            cmd.Parameters.AddWithValue("@columnId", columnId);
            cmd.Prepare();
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            rdr.Read();
            return new Product(rdr.GetInt32(0), rdr.GetString(1), rdr.GetFloat(2), rdr.GetInt32(3));
        }

        public void DeleteProductByColumnId(int columnId)
        {
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "DELETE FROM Product WHERE ColumnId = @columnID";
            cmd.Parameters.AddWithValue("@columnID", columnId);
            cmd.ExecuteNonQuery();
        }
        public void UpdateProductQuantityByColumnId(int columnId, int quantity)
        {
            string updateCommand = "Update Product set Quantity = @quantity WHERE ColumnId = @columnId";
            using var cmd = new SQLiteCommand(updateCommand, connection);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@columnId", columnId);
            cmd.ExecuteNonQuery();
        }
    }
}
