using System;
using System.Collections.Generic;
using Moq;
using ShopBL;
using ShopDL;
using ShopModel;
using Xunit;

namespace ShopTest{
    public class OrderBLTest{
        
        
        [Fact]
        public void Should_Get_All_Orders(){
            //Arrange
            int ordNum = -1;
            List<LineItem>listOfLineItems = new List<LineItem>{new LineItem(-2,new Product(-3,"spoon",1,"used to drink soup", 3), 3)}; 
            List<string>ListOfSFLoc = new List<string>{"TestAddress"};
            int totalPrice = 3;
            DateTime creationTimes = DateTime.Now;

            Order ord = new Order(){
                orderNumber = ordNum,
                LineItems = listOfLineItems,
                StoreFrontLocation = ListOfSFLoc,
                totalPrice = totalPrice,
                creationTime = creationTimes
            };

            List<Order> expectedListOfOrder = new List<Order>();
            expectedListOfOrder.Add(ord);

            //mocking dependencies
            Mock<IOrderRepository> mockRepo = new Mock<IOrderRepository>();

            //guantee dependency will always work
            mockRepo.Setup(repo => repo.GetAllOrder()).Returns(expectedListOfOrder);

            // pass mock version of iproduct
            IOrderBL ordBL = new OrderBL(mockRepo.Object);

            //act
            List<Order> actualListOfOrder = ordBL.GetAllOrder();

            //Assert
            Assert.Same(expectedListOfOrder, actualListOfOrder);
            Assert.Equal(ordNum, actualListOfOrder[0].orderNumber);
            Assert.Equal(listOfLineItems, actualListOfOrder[0].LineItems);
            Assert.Equal(ListOfSFLoc, actualListOfOrder[0].StoreFrontLocation);
            Assert.Equal(totalPrice, actualListOfOrder[0].totalPrice);
            Assert.Equal(creationTimes, actualListOfOrder[0].creationTime);

        }
       
    }
} 