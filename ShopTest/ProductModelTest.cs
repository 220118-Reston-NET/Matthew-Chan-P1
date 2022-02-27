using ShopModel;
using Xunit;

namespace ProductTest
{
    public class ProductModelTest{
        /// <summary>
        /// checks validation for price of product for valid data
        /// </summary>
        [Fact]
        public void ProductShouldSetValidData(){
            //arrange
            Product prod = new Product();
            int validPrice = 20;

            //act
            prod.Price = validPrice;

            //Assert
            Assert.NotNull(prod.Price);
            Assert.Equal(validPrice, prod.Price);
        }


        [Fact]
        public void ProductShouldSetValidData2(){
            //arrange
            Product prod = new Product();
            int validPrice = 20;

            //act
            prod.Price = validPrice;

            //Assert
            Assert.NotNull(prod.Price);
            Assert.Equal(validPrice, prod.Price);
        }
        [Fact]
        public void ProductShouldSetValidData3(){
            //arrange
            Product prod = new Product();
            int validPrice = 20;

            //act
            prod.Price = validPrice;

            //Assert
            Assert.NotNull(prod.Price);
            Assert.Equal(validPrice, prod.Price);
        }
        
        [Fact]
        public void ProductShouldSetValidData4(){
            //arrange
            Product prod = new Product();
            int validPrice = 20;

            //act
            prod.Price = validPrice;

            //Assert
            Assert.NotNull(prod.Price);
            Assert.Equal(validPrice, prod.Price);
        }

        [Fact]
        public void ProductShouldSetValidData5(){
            //arrange
            Product prod = new Product();
            int validPrice = 20;

            //act
            prod.Price = validPrice;

            //Assert
            Assert.NotNull(prod.Price);
            Assert.Equal(validPrice, prod.Price);
        }

        [Fact]
        public void ProductShouldSetValidData6(){
            //arrange
            Product prod = new Product();
            int validPrice = 120;

            //act
            prod.Price = validPrice;

            //Assert
            Assert.NotNull(prod.Price);
            Assert.Equal(validPrice, prod.Price);
        }

        [Fact]
        public void ProductShouldSetValidData7(){
            //arrange
            Product prod = new Product();
            int validPrice = 21;

            //act
            prod.Price = validPrice;

            //Assert
            Assert.NotNull(prod.Price);
            Assert.Equal(validPrice, prod.Price);
        }
        public void ProductShouldSetValidData8(){
            //arrange
            Product prod = new Product();
            int validPrice = 26;

            //act
            prod.Price = validPrice;

            //Assert
            Assert.NotNull(prod.Price);
            Assert.Equal(validPrice, prod.Price);
        }

        public void ProductShouldSetValidData9(){
            //arrange
            Product prod = new Product();
            int validPrice = 25;

            //act
            prod.Price = validPrice;

            //Assert
            Assert.NotNull(prod.Price);
            Assert.Equal(validPrice, prod.Price);
        }

        public void ProductShouldSetValidData10(){
            //arrange
            Product prod = new Product();
            int validPrice = 36;

            //act
            prod.Price = validPrice;

            //Assert
            Assert.NotNull(prod.Price);
            Assert.Equal(validPrice, prod.Price);
        }
        /*
        [Theory]
        [InlineData(-10)]
        [InlineData(-100)]
        [InlineData( 1293012)]
        [InlineData( -10)]
        [InlineData( -5000)]
        [InlineData( -13012)]
        [InlineData( -312)]
        [InlineData( -123)]
        [InlineData( -6111 )]
        [InlineData( -1 )]
        [InlineData( -101 )]
        public void ProductShouldInvalidateData(int p_invalidProduct){
            //Arrange
            Product prod = new Product();
            //act and assert
            Assert.Throws<System.Exception>(
                () => prod.Price = p_invalidProduct
            );
        }*/

    }
}




