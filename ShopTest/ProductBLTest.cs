
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
            int pId = -1;
            string prodName = "spoon";
            int prodPrice = 2;
            string prodDesc =  "used to help drink soup";
            int ageMin = 10;
            Product prod = new Product(){
                prodId = pId,
                Name = prodName,
                Price = prodPrice,
                Desc = prodDesc,
                Age_Restriction = ageMin
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
            Assert.Equal(pId, actualListOfProduct[0].prodId);
            Assert.Equal(prodName, actualListOfProduct[0].Name);
            Assert.Equal(prodPrice, actualListOfProduct[0].Price);
            Assert.Equal(prodDesc, actualListOfProduct[0].Desc);
            Assert.Equal(ageMin, actualListOfProduct[0].Age_Restriction);

        }

        
    }
} 