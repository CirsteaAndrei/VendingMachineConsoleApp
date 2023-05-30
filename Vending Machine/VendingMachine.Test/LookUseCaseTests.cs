using iQuest.VendingMachine.BusinessLayer;
using iQuest.VendingMachine.BusinessLayer.UseCases;
using iQuest.VendingMachine.DataAccessLayer;
using iQuest.VendingMachine.PresentationLayer;
using iQuest.VendingMachine.PresentationLayer.Exceptions;
using Moq;
using System.Runtime.CompilerServices;

namespace VendingMachine.Test

{
    [TestClass]
    public class LookUseCaseTests
    {
        private Mock<IProductRepository> productRepositoryMock;
        private LookUseCase lookUseCase;
        private ShelfView shelfView;

        [TestInitialize]
        public void TestInit()
        {
            productRepositoryMock = new();
            shelfView= new ShelfView();
            lookUseCase = new LookUseCase(shelfView,productRepositoryMock.Object);
        }

        [TestMethod]
        public void HavingProductsOnRepositorySelectAvailableProductsExpectedProductsListUnchanged()
        {
            //Arrange
            List<Product> TestProduct = new List<Product>();
            TestProduct.Add(new Product(1, "Product", 10, 10));
            //Act
            var result = lookUseCase.SelectAvailableProducts(TestProduct, 0);
            //Assert
            Assert.AreEqual(result, TestProduct);
        }

        [TestMethod]
        public void HavingLookUseCase_Execute_ExpectedAvailableProductsDisplayed()
        {
            //Arrange
            List<Product> TestProduct = new List<Product>();
            TestProduct.Add(new Product(1, "Product", 10, 10));
            productRepositoryMock.Setup(pRepo => pRepo.GetAll()).Returns(TestProduct);

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var result = $"1 Product 10$\r\n";
            //Act
            lookUseCase.Execute();
            //Assert
            Assert.AreEqual(result, stringWriter.ToString());
        }

        [TestMethod]
        public void HavingEmptyShelf_ExecuteUseCase_ExpectedMachineEmptyError()
        {
            //Arrange
            List<Product> TestProduct = new List<Product>();
            productRepositoryMock.Setup(pRepo => pRepo.GetAll()).Returns(TestProduct);
            //Assert
            Assert.ThrowsException<MachineEmptyException>(()=>shelfView.DisplayProducts(TestProduct));

        }
    }
}