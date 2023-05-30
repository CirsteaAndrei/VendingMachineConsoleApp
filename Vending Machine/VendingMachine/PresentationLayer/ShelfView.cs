using System;
using System.Collections.Generic;
using System.Linq;
using iQuest.VendingMachine.PresentationLayer.Exceptions;
using iQuest.VendingMachine.DataAccessLayer;

namespace iQuest.VendingMachine.PresentationLayer
{
    internal class ShelfView : DisplayBase
    {
        public void DisplayProducts(IEnumerable<Product> products)
        {

            if (!products.Any())
            {
                throw new MachineEmptyException();
            }
            else
            {
                foreach (Product product in products)
                {

                    DisplayLine($"{product.ColumnId.ToString()} {product.Name} {product.Price}$", ConsoleColor.Yellow);

                }
            }
        }
    }
}
