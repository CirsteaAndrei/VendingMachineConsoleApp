using iQuest.VendingMachine.DataAccessLayer;
using iQuest.VendingMachine.PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iQuest.VendingMachine.BusinessLayer.UseCases
{
    internal class LookUseCase : IUseCase
    {
        private readonly ShelfView shelfView;
        private readonly IProductRepository productRepository;

        private IEnumerable<Product> products;

        public string Name => "look";

        public string Description => "Show the vending machine products";

        public bool CanExecute => true;

        public LookUseCase(ShelfView shelfView, IProductRepository productRepository)
        {
            this.shelfView = shelfView ?? throw new ArgumentNullException(nameof(shelfView)); ;
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public void Execute()
        {
            products = SelectAvailableProducts(productRepository.GetAll(), 1);
            shelfView.DisplayProducts(products);
        }

        public IEnumerable<Product> SelectAvailableProducts(IEnumerable<Product> products, int quantityLimit)
        {
            if (quantityLimit > 0)
            {
                var selectedProducts = products.Where(product => product.Quantity >= quantityLimit);
                return selectedProducts;
            }
            else
                return products;
        }
    }
}
