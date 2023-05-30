using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.PresentationLayer.Exceptions;
using iQuest.VendingMachine.PresentationLayer;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iQuest.VendingMachine.BusinessLayer.UseCases;
using iQuest.VendingMachine.DataAccessLayer;
using iQuest.VendingMachine.BusinessLayer;

namespace VendingMachine.Test
{

    [TestClass]
    public class BuyUseCaseTests
    {
        private Mock<IProductRepository> productRepositoryMock;
        private Mock<IBuyView> buyViewMock;
        private Mock<IPayUseCase> payUseCaseMock;
        private AuthenticationService authService;
        private BuyUseCase buyUseCase;

        [TestInitialize]
        public void TestInit()
        {
            productRepositoryMock = new ();
            buyViewMock = new ();
            payUseCaseMock = new ();
            authService = new AuthenticationService();
            buyUseCase = new BuyUseCase(authService, buyViewMock.Object, payUseCaseMock.Object, productRepositoryMock.Object);
        }

        [TestMethod]
        public void HavingBuyUseCase_BuyProduct_ExpectedQuantityDecrease()
        {
            //Arrange 
            const int columnIdToTest = 1;
            Product testProd = new Product(columnIdToTest, "Product", 10, 10);
            buyViewMock.Setup(buyV => buyV.RequestProduct()).Returns(columnIdToTest);
            productRepositoryMock.Setup(pRepo => pRepo.GetProductByColumnId(columnIdToTest)).Returns(testProd);
            //Act
            buyUseCase.Execute();
            //Assert
            Assert.AreEqual(9, testProd.Quantity);
        }

        [TestMethod]
        public void HavingBuyUseCase_BuyOutOfStockProduct_ExpectedInsufficientStockException()
        {
            //Arrange 
            const int columnIdToTest = 1;
            Product testProd = new Product(columnIdToTest, "Product", 10, 0);
            buyViewMock.Setup(buyV => buyV.RequestProduct()).Returns(columnIdToTest);
            productRepositoryMock.Setup(pRepo => pRepo.GetProductByColumnId(columnIdToTest)).Returns(testProd);
            //Assert
            Assert.ThrowsException<InsufficientStockException>(()=>buyUseCase.Execute());
        }
        [TestMethod]
        public void HavingBuyUseCase_BuyNonExistentIdProduct_ExpectedInvalidColumnException()
        {
            //Arrange 
            const int columnIdToTest = 1;
            Product testProd = null;
            buyViewMock.Setup(buyV => buyV.RequestProduct()).Returns(columnIdToTest);
            productRepositoryMock.Setup(pRepo => pRepo.GetProductByColumnId(columnIdToTest)).Returns(testProd);
            //Assert
            Assert.ThrowsException<InvalidColumnException>(() => buyUseCase.Execute());
        }
    }
}
