using System.Collections.Generic;
using iQuest.VendingMachine.BusinessLayer;

namespace iQuest.VendingMachine.DataAccessLayer
{
    internal class InMemoryRepository : IProductRepository
    {
        private List<Product> products;

        public InMemoryRepository()
        {
            products = new List<Product>
            {
                new Product(1, "Soda", 1, 1),
                new Product(2, "Chips", (float)1.50, 10),
                new Product(3, "Chocolate", 2, 10),
                new Product(4, "Nutrition Bar", 2, 0)
            };
        }

        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public Product GetProductByColumnId(int columnId)
        {
            Product product = products.Find(p => p.ColumnId == columnId);
            return product;
        }

        public void DeleteProductByColumnId(int columnId)
        {
            products.RemoveAll(product => product.ColumnId == columnId);
        }

        public void UpdateProductQuantityByColumnId(int columnId, int quantity)
        {
            Product p = GetProductByColumnId(columnId);
            p.Quantity= quantity;
        }
    }

}
