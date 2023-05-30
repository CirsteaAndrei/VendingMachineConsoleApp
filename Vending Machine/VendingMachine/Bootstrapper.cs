using System;
using System.Collections.Generic;
using System.Configuration;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.BusinessLayer;
using iQuest.VendingMachine.BusinessLayer.Payment;
using iQuest.VendingMachine.BusinessLayer.UseCases;
using iQuest.VendingMachine.DataAccessLayer;
using iQuest.VendingMachine.PresentationLayer;
using iQuest.VendingMachine.PresentationLayer.PaymentTerminals;


namespace iQuest.VendingMachine
{
    internal class Bootstrapper
    {
        public void Run()
        {
            VendingMachineApplication vendingMachineApplication = BuildApplication();
            vendingMachineApplication.Run();
        }

        private static VendingMachineApplication BuildApplication()
        {
            MainView mainView = new MainView();
            LoginView loginView = new LoginView();
            ShelfView shelfView = new ShelfView();
            IBuyView buyView = new BuyView();
            CachePaymentTerminal cachePaymentTerminal = new CachePaymentTerminal();
            CardPaymentTerminal cardPaymentTerminal = new CardPaymentTerminal();

            IProductRepository productRepository = new LiteDbProductRepository(ConfigurationManager.ConnectionStrings["LiteDb"].ConnectionString);
            //IProductRepository productRepository = new SQLiteProductRepository(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);
            //IProductRepository productRepository = new InMemoryRepository();

            AuthenticationService authenticationService = new AuthenticationService();

            List<IPayAlgorithm> paymentAlgorithms = new List<IPayAlgorithm>
            {
                new CachePayment(cachePaymentTerminal),
                new CardPayment(cardPaymentTerminal)
            };

            List<PaymentMethod> paymentMethods = new List<PaymentMethod>
            {
                new PaymentMethod(0,paymentAlgorithms[0].Name),
                new PaymentMethod(1,paymentAlgorithms[1].Name),
            };

            IPayUseCase payUseCase = new PayUseCase(buyView,paymentAlgorithms,paymentMethods);

            List<IUseCase> useCases = new List<IUseCase>
            {
                new LoginUseCase(authenticationService, loginView),
                new LogoutUseCase(authenticationService),
                new LookUseCase(shelfView, productRepository),
                new BuyUseCase(authenticationService, buyView, payUseCase, productRepository),
            };

            return new VendingMachineApplication(useCases, mainView);
        }
    }
}