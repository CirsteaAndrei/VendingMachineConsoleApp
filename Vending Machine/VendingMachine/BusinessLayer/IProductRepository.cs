using System.Collections.Generic;
using iQuest.VendingMachine.DataAccessLayer;

namespace iQuest.VendingMachine.BusinessLayer
{
    internal interface IProductRepository
    {
        abstract public IEnumerable<Product> GetAll();

        abstract public void AddProduct(Product product);

        abstract public Product GetProductByColumnId(int columnId);

        abstract public void DeleteProductByColumnId(int columnId);

        abstract public void UpdateProductQuantityByColumnId(int columnId, int quantity);
    }
}
