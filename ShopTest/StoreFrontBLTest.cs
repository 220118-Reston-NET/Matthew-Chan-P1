/*
using System.Collections.Generic;
using Moq;
using ShopBL;
using ShopDL;
using ShopModel;
using Xunit;

namespace ShopTest{
    public class LineItemTest{
        
        
        [Fact]
        public void Should_Get_All_StoreFronts(){
            //Arrange
            int _storeId = -10;
            string _name = "TheTesterOfLegends";
            string _address = "In the burning abyss";
            Inventory Inv = ({ Product(-3,"spoon",1,"used to drink soup", 3)}, {2}, -10)
            List<Order> Orders = new Order( -1,line item, {"9th circle"}, 2, DateTime.Now) 

            LineItem li = new LineItem(){
                LineItemId = _LineItemId,
                Products = _products,
                Quantity = _quantity,
                TotalPrice = _totalPrice
            }

            List<LineItem> expectedListOfLineItem = new List<LineItem>();
            expectedListOfLineItem.Add(li);

            //mocking dependencies
            Mock<ILineItemRepository> mockRepo = new Mock<ILineItemRepository>();

            //guantee dependency will always work
            mockRepo.Setup(repo => repo.GetAllLineItem()).Returns(expectedListOfLineItem);

            // pass mock version of LineItem
            ILineItemBL custBL = new LineItemBL(mockRepo.Object);

            //act
            List<LineItem> actualListOfLineItem = custBL.GetAllLineItems();

            //Assert
            Assert.Same(expectedListOfLineItem, actualListOfLineItem);
            Assert.Equal(_lineItemId, actualListOfLineItem[0].lineItemId);
            Assert.Equal(_products, actualListOfLineItem[0].Products);
            Assert.Equal(_quantity, actualListOfLineItem[0].Quantity);
            Assert.Equal(_totalPrice, actualListOfLineItem[0].TotalPrice);

        }



    }
}
*/