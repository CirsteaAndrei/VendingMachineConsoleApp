using iQuest.VendingMachine.BusinessLayer;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iQuest.VendingMachine.DataAccessLayer
{
    internal class LiteDbProductRepository : IProductRepository
    {
        private readonly LiteDatabase database;
        public LiteDbProductRepository(string connectionString) 
        {
            database = new LiteDatabase(connectionString);
            var productCollection = this.database.GetCollection<Product>();

            var products = new List<Product>
            {
                new Product(1, "Soda", 1, 1),
                new Product(2, "Chips", (float)1.50, 10),
                new Product(3, "Chocolate", 2, 10),
                new Product(4, "Nutrition Bar", 2, 0)
            };
            
            productCollection.DeleteAll();
            productCollection.InsertBulk(products);
        }

        public IEnumerable<Product> GetAll()
        {
            var col = database.GetCollection<Product>();
            return col.FindAll().ToList();
        }

        public void AddProduct(Product product)
        {
            var col = database.GetCollection<Product>();
            col.Insert(product);
        }

        public Product GetProductByColumnId(int columnId)
        {
            var col = database.GetCollection<Product>();
            return col.Find(a => a.ColumnId == columnId).FirstOrDefault();
        }

        public void DeleteProductByColumnId(int columnId)
        {
            var col = database.GetCollection<Product>();
            col.Delete(columnId);
        }

        public void UpdateProductQuantityByColumnId(int columnId, int quantity)
        {
            var product = GetProductByColumnId(columnId);
            if (product != null)
            {
                product.Quantity = quantity;
                var col = database.GetCollection<Product>();
                col.Update(product);
            }
        }
    }
}
