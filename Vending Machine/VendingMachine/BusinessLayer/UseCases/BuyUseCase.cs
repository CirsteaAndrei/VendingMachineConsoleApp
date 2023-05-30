using System;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.PresentationLayer.Exceptions;
using iQuest.VendingMachine.PresentationLayer;
using iQuest.VendingMachine.DataAccessLayer;

namespace iQuest.VendingMachine.BusinessLayer.UseCases
{
    internal class BuyUseCase : IUseCase
    {
        private readonly IBuyView buyView;
        private readonly IProductRepository productRepository;
        private readonly AuthenticationService authenticationService;
        private readonly IPayUseCase payUseCase;

        public string Name => "buy";
        public string Description => "Buy a product";
        public bool CanExecute => !authenticationService.IsUserAuthenticated;
        public BuyUseCase(AuthenticationService authenticationService, IBuyView buyView, IPayUseCase payUseCase, IProductRepository productRepository)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.buyView = buyView ?? throw new ArgumentNullException(nameof(buyView));
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.payUseCase = payUseCase ?? throw new ArgumentNullException(nameof(payUseCase));
        }

        public void Execute()
        {
            int columnID = buyView.RequestProduct();
            Product selectedProduct = productRepository.GetProductByColumnId(columnID);
            CheckStock(selectedProduct);
            payUseCase.Execute(selectedProduct.Price);
            DecreaseQuantityOfProduct(selectedProduct);
            buyView.DispenseProduct(selectedProduct.Name);
        }
        private void DecreaseQuantityOfProduct(Product selectedProduct)
        {
            selectedProduct.Quantity--;
            productRepository.UpdateProductQuantityByColumnId(selectedProduct.ColumnId, selectedProduct.Quantity);
        }
        private void CheckStock(Product selectedProduct)
        {
            if (selectedProduct == null)
            {
                throw new InvalidColumnException();
            }
            if (selectedProduct.Quantity < 1)
            {
                throw new InsufficientStockException();
            }
        }
    }
}
