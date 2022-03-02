
using System.Collections.Generic;
using Moq;
using ShopBL;
using ShopDL;
using ShopModel;
using Xunit;

namespace ShopTest{
    public class CustomerTest{
        
        
        [Fact]
        public void Should_Get_All_Customers(){
            //Arrange
            int customId = 9001;
            string custName = "Max";
            int custAge = 22;
            string custAddress = "Utopia";
            string custNumber = "123-123-1230";

            Customer cust = new Customer(){
                custId = customId,
                Name = custName,
                Age = custAge,
                Address = custAddress,
                PhoneNumber = custNumber
            };

            List<Customer> expectedListOfCustomer = new List<Customer>();
            expectedListOfCustomer.Add(cust);

            //mocking dependencies
            Mock<ICustomerRepository> mockRepo = new Mock<ICustomerRepository>();

            //guantee dependency will always work
            mockRepo.Setup(repo => repo.GetAllCustomer()).Returns(expectedListOfCustomer);

            // pass mock version of Customer
            ICustomerBL custBL = new CustomerBL(mockRepo.Object);

            //act
            List<Customer> actualListOfCustomer = custBL.GetAllCustomers();

            //Assert
            Assert.Same(expectedListOfCustomer, actualListOfCustomer);
            Assert.Equal(customId, actualListOfCustomer[0].custId);
            Assert.Equal(custName, actualListOfCustomer[0].Name);
            Assert.Equal(custAge, actualListOfCustomer[0].Age);
            Assert.Equal(custAddress, actualListOfCustomer[0].Address);
            Assert.Equal(custNumber, actualListOfCustomer[0].PhoneNumber);

        }
    


        [Fact]
        public void Should_Set_Valid_Phone_Number()
        {
            //Arrange
            Customer cust = new Customer();
            string validPhoneNumber = "112-223-3334";
            //Act
            cust.PhoneNumber = validPhoneNumber;
            //Assert
            Assert.NotNull(cust.PhoneNumber);
            Assert.Equal(validPhoneNumber, cust.PhoneNumber);

        }

        [Fact]
        public void NewCustLoginSuccessful(){
            int _customId = 9001;
            string _custName = "Max";
            int _custAge = 22;
            string _custAddress = "Utopia";
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
            Mock<ICustomerRepository> mockRepo = new Mock<ICustomerRepository>();

            //guantee dependency will always work
            mockRepo.Setup(repo => repo.GetAllCustomer()).Returns(expectedListOfCustomer);

            // pass mock version of Customer
            ICustomerBL custBL = new CustomerBL(mockRepo.Object);

            List<Customer> actualListOfCustomer = custBL.GetAllCustomers();

            //act and assert
            Assert.Same(expectedListOfCustomer[0], custBL.GetCustomerFromLogin(cust.UserName,cust.Password));
            Assert.Same(actualListOfCustomer[0], custBL.GetCustomerFromLogin(cust.UserName,cust.Password));
        }
        
        ///////////////////////////

        [Fact]
        public void NewCustomerDoesNotHavePermission(){
            int _customId = 9001;
            string _custName = "Max";
            int _custAge = 22;
            string _custAddress = "Utopia";
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
            Mock<ICustomerRepository> mockRepo = new Mock<ICustomerRepository>();

            //guantee dependency will always work
            mockRepo.Setup(repo => repo.GetAllCustomer()).Returns(expectedListOfCustomer);

            // pass mock version of Customer
            ICustomerBL custBL = new CustomerBL(mockRepo.Object);

            List<Customer> actualListOfCustomer = custBL.GetAllCustomers();

            Assert.Throws<System.Exception>(
                () => custBL.CheckAuthorityClearance(actualListOfCustomer[0], 1 ));
        }

        [Fact]
        public void NewCustFoundFromName(){
            int _customId = 9002;
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
            Mock<ICustomerRepository> mockRepo = new Mock<ICustomerRepository>();

            //guantee dependency will always work
            mockRepo.Setup(repo => repo.GetAllCustomer()).Returns(expectedListOfCustomer);

            // pass mock version of Customer
            ICustomerBL custBL = new CustomerBL(mockRepo.Object);

            List<Customer> actualCustSearch = custBL.SearchCustomerFromNumber("123-123-1230");

            //act and assert
            Assert.Same(expectedListOfCustomer[0], actualCustSearch[0]);
        }
        

        [Fact]
        public void NewCustFoundFromNum(){
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
            Mock<ICustomerRepository> mockRepo = new Mock<ICustomerRepository>();

            //guantee dependency will always work
            mockRepo.Setup(repo => repo.GetAllCustomer()).Returns(expectedListOfCustomer);

            // pass mock version of Customer
            ICustomerBL custBL = new CustomerBL(mockRepo.Object);

            List<Customer> actualCustSearch = custBL.SearchCustomerFromNumber("123-123-1230");

            //act and assert
            Assert.Same(expectedListOfCustomer[0], actualCustSearch[0]);
        }
    
        [Fact]
        public void FailedToFindCustFromEMail(){
            int _customId = 9001;
            string _custName = "Max";
            int _custAge = 22;
            string _custAddress = "UtopiaZexal";
            string _email = "maxEmailExists@gmail.com";
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
            Mock<ICustomerRepository> mockRepo = new Mock<ICustomerRepository>();

            //guantee dependency will always work
            mockRepo.Setup(repo => repo.GetAllCustomer()).Returns(expectedListOfCustomer);

            // pass mock version of Customer
            ICustomerBL custBL = new CustomerBL(mockRepo.Object);

            List<Customer> actualListOfCustomer = custBL.GetAllCustomers();

            //act and assert
            
            Assert.Throws<System.Exception>(
                () => custBL.SearchCustomerFromEMail("fakeEmail@fake.com")
            ); 
        }
    }
}