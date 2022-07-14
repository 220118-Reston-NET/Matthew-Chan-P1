// using System;
// using System.Collections.Generic;
// using Moq;
// using ShopBL;
// using ShopDL;
// using ShopModel;
// using Xunit;

// namespace ShopTest{
//     public class StoreFrontTest{
        
        
//         [Fact]
//         public void Should_Get_All_StoreFronts(){
//             //Arrange
//             int _storeId = -10;
//             string _name = "TheTesterOfLegendsShop";
//             string _address = "In the burning abyss";
//             Inventory _inv = new Inventory( new List<Product>{new Product(-3,"spoon",1,"used to drink soup", 3)},
//                                          new List<int>{2}, 
//                                          -10);
//             List<Order> _orders = new List<Order>{new Order( -1,
//                                                 new List<LineItem>{new LineItem(-2,new Product(-3,"Lancea",1,"nice lance", 3), 3)}, 
//                                                 new List<string>{"9th circle"}, 
//                                                 2, 
//                                                 DateTime.Now)}; 
            
//             StoreFront sf = new StoreFront(){
//                 storeId = _storeId,
//                 Name = _name,
//                 Address = _address,
//                 Inv = _inv,
//                 Orders = _orders
//             };

//             List<StoreFront> expectedListOfStoreFront = new List<StoreFront>();
//             expectedListOfStoreFront.Add(sf);

//             //mocking dependencies
//             Mock<IStoreFrontRepository> mockRepo = new Mock<IStoreFrontRepository>();

//             //guantee dependency will always work
//             mockRepo.Setup(repo => repo.GetAllStoreFront()).Returns(expectedListOfStoreFront);

//             // pass mock version of StoreFront
//             IStoreFrontBL storeBL = new StoreFrontBL(mockRepo.Object);

//             //act
//             List<StoreFront> actualListOfStoreFront = storeBL.GetAllStoreFronts();

//             //Assert
//             Assert.Same(expectedListOfStoreFront, actualListOfStoreFront);
//             Assert.Equal(_storeId, actualListOfStoreFront[0].storeId);
//             Assert.Equal(_name, actualListOfStoreFront[0].Name);
//             Assert.Equal(_address, actualListOfStoreFront[0].Address);
//             Assert.Equal(_inv, actualListOfStoreFront[0].Inv);
//             Assert.Equal(_orders, actualListOfStoreFront[0].Orders);
//         }

//         [Fact]
//         public void ChecksStoreForAProduct(){
//             //Arrange
//             int _storeId = -10;
//             string _name = "TheTesterOfLegendsShop";
//             string _address = "In the burning abyss";
//             Inventory _inv = new Inventory( new List<Product>{new Product(-3,"spoon",1,"used to drink soup", 3)},
//                                          new List<int>{2}, 
//                                          -10);
//             List<Order> _orders = new List<Order>{new Order( -1,
//                                                 new List<LineItem>{new LineItem(-2,new Product(-3,"Lancea",1,"nice lance", 3), 3)}, 
//                                                 new List<string>{"9th circle"}, 
//                                                 2, 
//                                                 DateTime.Now)}; 
            
//             StoreFront sf = new StoreFront(){
//                 storeId = _storeId,
//                 Name = _name,
//                 Address = _address,
//                 Inv = _inv,
//                 Orders = _orders
//             };

//             List<StoreFront> expectedListOfStoreFront = new List<StoreFront>();
//             expectedListOfStoreFront.Add(sf);

//             //mocking dependencies
//             Mock<IStoreFrontRepository> mockRepo = new Mock<IStoreFrontRepository>();

//             //guantee dependency will always work
//             mockRepo.Setup(repo => repo.GetAllStoreFront()).Returns(expectedListOfStoreFront);

//             // pass mock version of StoreFront
//             IStoreFrontBL storeBL = new StoreFrontBL(mockRepo.Object);

//             //act
//             List<StoreFront> actualListOfStoreFront = storeBL.GetAllStoreFronts();
//             List<StoreFront> storeFrontWithProduct = storeBL.checkStoresForAProduct(-3);

//             //Assert
//             Assert.Same(expectedListOfStoreFront, actualListOfStoreFront);
//             Assert.Same(expectedListOfStoreFront[0], storeFrontWithProduct[0]);
//         }

//         [Theory]
//         [InlineData(-10)]
//         public void ChecksValidStoreId(int testId){
//             //Arrange
//             int _storeId = -10;
//             string _name = "TheTesterOfLegendsShop";
//             string _address = "In the burning abyss";
//             Inventory _inv = new Inventory( new List<Product>{new Product(-3,"spoon",1,"used to drink soup", 3)},
//                                          new List<int>{2}, 
//                                          -10);
//             List<Order> _orders = new List<Order>{new Order( -1,
//                                                 new List<LineItem>{new LineItem(-2,new Product(-3,"Lancea",1,"nice lance", 3), 3)}, 
//                                                 new List<string>{"9th circle"}, 
//                                                 2, 
//                                                 DateTime.Now)}; 
            
//             StoreFront sf = new StoreFront(){
//                 storeId = _storeId,
//                 Name = _name,
//                 Address = _address,
//                 Inv = _inv,
//                 Orders = _orders
//             };

//             List<StoreFront> expectedListOfStoreFront = new List<StoreFront>();
//             expectedListOfStoreFront.Add(sf);

//             //mocking dependencies
//             Mock<IStoreFrontRepository> mockRepo = new Mock<IStoreFrontRepository>();

//             //guantee dependency will always work
//             mockRepo.Setup(repo => repo.GetAllStoreFront()).Returns(expectedListOfStoreFront);

//             // pass mock version of StoreFront
//             IStoreFrontBL storeBL = new StoreFrontBL(mockRepo.Object);

//             //act
//             List<StoreFront> actualListOfStoreFront = storeBL.GetAllStoreFronts();
//             bool checkIfValidStoreID = storeBL.CheckValidStoreId(testId);

//             //Assert
//             Assert.Same(expectedListOfStoreFront, actualListOfStoreFront);
//             Assert.Equal(true, checkIfValidStoreID);
//         }

//         [Theory]
//         [InlineData(-100)]
//         [InlineData(100)]
//         public void FailsInvalidStoreId(int testId){
//             //Arrange
//             int _storeId = -10;
//             string _name = "TheTesterOfLegendsShop";
//             string _address = "In the burning abyss";
//             Inventory _inv = new Inventory( new List<Product>{new Product(-3,"spoon",1,"used to drink soup", 3)},
//                                          new List<int>{2}, 
//                                          -10);
//             List<Order> _orders = new List<Order>{new Order( -1,
//                                                 new List<LineItem>{new LineItem(-2,new Product(-3,"Lancea",1,"nice lance", 3), 3)}, 
//                                                 new List<string>{"9th circle"}, 
//                                                 2, 
//                                                 DateTime.Now)}; 
            
//             StoreFront sf = new StoreFront(){
//                 storeId = _storeId,
//                 Name = _name,
//                 Address = _address,
//                 Inv = _inv,
//                 Orders = _orders
//             };

//             List<StoreFront> expectedListOfStoreFront = new List<StoreFront>();
//             expectedListOfStoreFront.Add(sf);

//             //mocking dependencies
//             Mock<IStoreFrontRepository> mockRepo = new Mock<IStoreFrontRepository>();

//             //guantee dependency will always work
//             mockRepo.Setup(repo => repo.GetAllStoreFront()).Returns(expectedListOfStoreFront);

//             // pass mock version of StoreFront
//             IStoreFrontBL storeBL = new StoreFrontBL(mockRepo.Object);

//             //act
//             List<StoreFront> actualListOfStoreFront = storeBL.GetAllStoreFronts();
            
//             //Assert
//             Assert.Throws<System.Exception>(
//                 () => storeBL.CheckValidStoreId(testId)
//             );
//         }

//         [Fact]
//         public void Should_Get_StoreFront_FromName(){
//             //Arrange
//             int _storeId = -10;
//             string _name = "TheTesterOfLegendsShop";
//             string _address = "In the burning abyss";
//             Inventory _inv = new Inventory( new List<Product>{new Product(-3,"spoon",1,"used to drink soup", 3)},
//                                          new List<int>{2}, 
//                                          -10);
//             List<Order> _orders = new List<Order>{new Order( -1,
//                                                 new List<LineItem>{new LineItem(-2,new Product(-3,"Lancea",1,"nice lance", 3), 3)}, 
//                                                 new List<string>{"9th circle"}, 
//                                                 2, 
//                                                 DateTime.Now)}; 
            
//             StoreFront sf = new StoreFront(){
//                 storeId = _storeId,
//                 Name = _name,
//                 Address = _address,
//                 Inv = _inv,
//                 Orders = _orders
//             };

//             List<StoreFront> expectedListOfStoreFront = new List<StoreFront>();
//             expectedListOfStoreFront.Add(sf);

//             //mocking dependencies
//             Mock<IStoreFrontRepository> mockRepo = new Mock<IStoreFrontRepository>();

//             //guantee dependency will always work
//             mockRepo.Setup(repo => repo.GetAllStoreFront()).Returns(expectedListOfStoreFront);

//             // pass mock version of StoreFront
//             IStoreFrontBL storeBL = new StoreFrontBL(mockRepo.Object);

//             //act
//             List<StoreFront> actualListOfStoreFront = storeBL.SearchStoreFrontName(_name);

//             //Assert
//             Assert.Same(expectedListOfStoreFront[0], actualListOfStoreFront[0]);
//             Assert.Equal(_storeId, actualListOfStoreFront[0].storeId);
//             Assert.Equal(_name, actualListOfStoreFront[0].Name);
//             Assert.Equal(_address, actualListOfStoreFront[0].Address);
//             Assert.Equal(_inv, actualListOfStoreFront[0].Inv);
//             Assert.Equal(_orders, actualListOfStoreFront[0].Orders);
//         }
        



//     }
// }
