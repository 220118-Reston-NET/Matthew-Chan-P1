
using System.Collections.Generic;
using Moq;
using ShopBL;
using ShopDL;
using ShopModel;
using Xunit;

namespace ShopTest{
    public class ProductBLTest{
        
        
        [Fact]
        public void Should_Get_All_Products(){
            //Arrange
            string prodName = "spoon";
            int prodPrice = 2;
            Product prod = new Product(){
                Name = prodName,
                Price = prodPrice
            };

            List<Product> expectedListOfProduct = new List<Product>();
            expectedListOfProduct.Add(prod);

            //mocking dependencies
            Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();

            //guantee dependency will always work
            mockRepo.Setup(repo => repo.GetAllProducts()).Returns(expectedListOfProduct);

            // pass mock version of iproduct
            IProductBL prodBL = new ProductBL(mockRepo.Object);

            //act
            List<Product> actualListOfProduct = prodBL.GetAllProducts();

            //Assert
            Assert.Same(expectedListOfProduct, actualListOfProduct);
            Assert.Equal(prodName, actualListOfProduct[0].Name);
            Assert.Equal(prodPrice, actualListOfProduct[0].Price);

        }

        [Fact]
        public void Should_Get_All_Product2(){
            //Arrange
            string prodName = "fork";
            int prodPrice = 23;
            Product prod = new Product(){
                Name = prodName,
                Price = prodPrice
            };

            List<Product> expectedListOfProduct = new List<Product>();
            expectedListOfProduct.Add(prod);

            //mocking dependencies
            Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();

            //guantee dependency will always work
            mockRepo.Setup(repo => repo.GetAllProducts()).Returns(expectedListOfProduct);

            // pass mock version of iproduct
            IProductBL prodBL = new ProductBL(mockRepo.Object);

            //act
            List<Product> actualListOfProduct = prodBL.GetAllProducts();

            //Assert
            Assert.Same(expectedListOfProduct, actualListOfProduct);
            Assert.Equal(prodName, actualListOfProduct[0].Name);
            Assert.Equal(prodPrice, actualListOfProduct[0].Price);

        }
    }
} 