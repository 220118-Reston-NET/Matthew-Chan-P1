// using ShopModel;
// using Xunit;

// namespace ProductTest
// {
//     public class ProductModelTest{
//         /// <summary>
//         /// checks validation for price of product for valid data
//         /// </summary>
//         [Fact]
//         public void ProductShouldSetValidData(){
//             //arrange
//             Product prod = new Product();
//             int validPrice = 20;

//             //act
//             prod.Price = validPrice;

//             //Assert
//             Assert.Equal(validPrice, prod.Price);
//         }


//         [Theory]
//         [InlineData(-10)]
//         [InlineData(-1293012)]
        
//         public void ProductShouldInvalidateData(int p_invalidProduct){
//             //Arrange
//             Product prod = new Product();
//             //act and assert
//             Assert.Throws<System.Exception>(
//                 () => prod.Age_Restriction = p_invalidProduct
//             );
//         }
        
        

//     }
// }




