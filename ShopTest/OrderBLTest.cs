global using Serilog;
using System;
using System.Collections.Generic;
using Moq;
using ShopBL;
using ShopDL;
using ShopModel;
using Xunit;


Log.Logger = new LoggerConfiguration()
    .WriteTo.File("./logs/user.txt") //We configure our logger to save in this file
    .CreateLogger();

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
        
        [Fact]
        public void FailInvalidCustomerOrder(){

            int ordNum = 1;
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

            Mock<IOrderRepository> mockRepo = new Mock<IOrderRepository>();
            mockRepo.Setup(repo => repo.GetAllOrder()).Returns(expectedListOfOrder);
            IOrderBL ordBL = new OrderBL(mockRepo.Object);




            int _customId = 9001;
            string _custName = "Max";
            int _custAge = 22;
            string _custAddress = "UtopiaZexal";
            string _custNumber = "123-123-1230";
            string _userName = "fakeusername";
            string _password = "fakepassword";
            int _authority = 0;

            Customer cust = new Customer(){
                custId = _customId,
                Name = _custName,
                Age = _custAge,
                Address = _custAddress,
                PhoneNumber = _custNumber,
                UserName = _userName,
                Password = _password,
                Authority = _authority
            };

            List<Customer> expectedListOfCustomer = new List<Customer>();
            expectedListOfCustomer.Add(cust);

            //mocking dependencies
            Mock<ICustomerRepository> mockRepoc = new Mock<ICustomerRepository>();

            //guantee dependency will always work
            mockRepo.Setup(repo => repo.GetAllCustomer()).Returns(expectedListOfCustomer);

            // pass mock version of Customer
            ICustomerBL custBL = new CustomerBL(mockRepoc.Object);




            Assert.Throws<System.Exception>(
                () => ordBL.AddOrder(ord,100,1)
            );
        }

        [Fact]
        public void FailInvalidAge(){

            int ordNum = 1;
            List<LineItem>listOfLineItems = new List<LineItem>{new LineItem(-2,new Product(1,"spoon",1,"used to drink soup", 3), 3)}; 
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

            Mock<IOrderRepository> mockRepo = new Mock<IOrderRepository>();
            mockRepo.Setup(repo => repo.GetAllOrder()).Returns(expectedListOfOrder);
            IOrderBL ordBL = new OrderBL(mockRepo.Object);




            int _customId = 9001;
            string _custName = "Max";
            int _custAge = 10;
            string _custAddress = "UtopiaZexal";
            string _custNumber = "123-123-1230";
            string _userName = "fakeusername";
            string _password = "fakepassword";
            int _authority = 0;

            Customer cust = new Customer(){
                custId = _customId,
                Name = _custName,
                Age = _custAge,
                Address = _custAddress,
                PhoneNumber = _custNumber,
                UserName = _userName,
                Password = _password,
                Authority = _authority
            };

            List<Customer> expectedListOfCustomer = new List<Customer>();
            expectedListOfCustomer.Add(cust);

            //mocking dependencies
            Mock<ICustomerRepository> mockRepoc = new Mock<ICustomerRepository>();

            //guantee dependency will always work
            mockRepo.Setup(repo => repo.GetAllCustomer()).Returns(expectedListOfCustomer);

            // pass mock version of Customer
            ICustomerBL custBL = new CustomerBL(mockRepoc.Object);

            int pId = 1;
            string prodName = "spoon";
            int prodPrice = 1;
            string prodDesc =  "used to help drink soup";
            int ageMin = 3;
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
            Mock<IProductRepository> mockRepo2 = new Mock<IProductRepository>();

            //guantee dependency will always work
            mockRepo.Setup(repo => repo.GetAllProducts()).Returns(expectedListOfProduct);

            // pass mock version of iproduct
            IProductBL prodBL = new ProductBL(mockRepo2.Object);



            Assert.Throws<System.Exception>(
                () => ordBL.CheckValidAge(0,0)
            );
        }
        
    

    }
} 