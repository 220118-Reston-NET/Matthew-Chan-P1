
// using System.Collections.Generic;
// using Moq;
// using ShopBL;
// using ShopDL;
// using ShopModel;
// using Xunit;

// namespace ShopTest{
//     public class ProfileTest{
        
        
//         [Fact]
//         public void Should_Get_All_Profiles(){
//             //Arrange
//             int profomId = 9001;
//             string profName = "Max";
//             int profAge = 22;
//             string profAddress = "Utopia";
//             string profNumber = "123-123-1230";

//             Profile prof = new Profile(){
//                 profId = profomId,
//                 Name = profName,
//                 Age = profAge,
//                 Address = profAddress,
//                 PhoneNumber = profNumber
//             };

//             List<Profile> expectedListOfProfile = new List<Profile>();
//             expectedListOfProfile.Add(prof);

//             //mocking dependencies
//             Mock<IProfileRepository> mockRepo = new Mock<IProfileRepository>();

//             //guantee dependency will always work
//             mockRepo.Setup(repo => repo.GetAllProfile()).Returns(expectedListOfProfile);

//             // pass mock version of Profile
//             IProfileBL profBL = new ProfileBL(mockRepo.Object);

//             //act
//             List<Profile> actualListOfProfile = profBL.GetAllProfiles();

//             //Assert
//             Assert.Same(expectedListOfProfile, actualListOfProfile);
//             Assert.Equal(profomId, actualListOfProfile[0].profId);
//             Assert.Equal(profName, actualListOfProfile[0].Name);
//             Assert.Equal(profAge, actualListOfProfile[0].Age);
//             Assert.Equal(profAddress, actualListOfProfile[0].Address);
//             Assert.Equal(profNumber, actualListOfProfile[0].PhoneNumber);

//         }
    


//         [Fact]
//         public void Should_Set_Valid_Phone_Number()
//         {
//             //Arrange
//             Profile prof = new Profile();
//             string validPhoneNumber = "112-223-3334";
//             //Act
//             prof.PhoneNumber = validPhoneNumber;
//             //Assert
//             Assert.NotNull(prof.PhoneNumber);
//             Assert.Equal(validPhoneNumber, prof.PhoneNumber);

//         }

//         [Fact]
//         public void NewCustLoginSuccessful(){
//             int _profomId = 9001;
//             string _profName = "Max";
//             int _profAge = 22;
//             string _profAddress = "Utopia";
//             string _profNumber = "123-123-1230";
//             string _userName = "fakeusername";
//             string _password = "fakepassword";
//             int _authority = 0;

//             Profile prof = new Profile(){
//                 profId = _profomId,
//                 Name = _profName,
//                 Age = _profAge,
//                 Address = _profAddress,
//                 PhoneNumber = _profNumber,
//                 UserName = _userName,
//                 Password = _password,
//                 Authority = _authority
//             };

//             List<Profile> expectedListOfProfile = new List<Profile>();
//             expectedListOfProfile.Add(prof);

//             //mocking dependencies
//             Mock<IProfileRepository> mockRepo = new Mock<IProfileRepository>();

//             //guantee dependency will always work
//             mockRepo.Setup(repo => repo.GetAllProfile()).Returns(expectedListOfProfile);

//             // pass mock version of Profile
//             IProfileBL profBL = new ProfileBL(mockRepo.Object);

//             List<Profile> actualListOfProfile = profBL.GetAllProfiles();

//             //act and assert
//             Assert.Same(expectedListOfProfile[0], profBL.GetProfileFromLogin(prof.UserName,prof.Password));
//             Assert.Same(actualListOfProfile[0], profBL.GetProfileFromLogin(prof.UserName,prof.Password));
//         }
        
//         ///////////////////////////

//         [Fact]
//         public void NewProfileDoesNotHavePermission(){
//             int _profomId = 9001;
//             string _profName = "Max";
//             int _profAge = 22;
//             string _profAddress = "Utopia";
//             string _profNumber = "123-123-1230";
//             string _userName = "fakeusername";
//             string _password = "fakepassword";
//             int _authority = 0;

//             Profile prof = new Profile(){
//                 profId = _profomId,
//                 Name = _profName,
//                 Age = _profAge,
//                 Address = _profAddress,
//                 PhoneNumber = _profNumber,
//                 UserName = _userName,
//                 Password = _password,
//                 Authority = _authority
//             };

//             List<Profile> expectedListOfProfile = new List<Profile>();
//             expectedListOfProfile.Add(prof);

//             //mocking dependencies
//             Mock<IProfileRepository> mockRepo = new Mock<IProfileRepository>();

//             //guantee dependency will always work
//             mockRepo.Setup(repo => repo.GetAllProfile()).Returns(expectedListOfProfile);

//             // pass mock version of Profile
//             IProfileBL profBL = new ProfileBL(mockRepo.Object);

//             List<Profile> actualListOfProfile = profBL.GetAllProfiles();

//             Assert.Throws<System.Exception>(
//                 () => profBL.CheckAuthorityClearance(actualListOfProfile[0], 1 ));
//         }

//         [Fact]
//         public void NewCustFoundFromName(){
//             int _profomId = 9002;
//             string _profName = "Max";
//             int _profAge = 22;
//             string _profAddress = "UtopiaZexal";
//             string _profNumber = "123-123-1230";
//             string _userName = "fakeusername";
//             string _password = "fakepassword";
//             int _authority = 0;

//             Profile prof = new Profile(){
//                 profId = _profomId,
//                 Name = _profName,
//                 Age = _profAge,
//                 Address = _profAddress,
//                 PhoneNumber = _profNumber,
//                 UserName = _userName,
//                 Password = _password,
//                 Authority = _authority
//             };

//             List<Profile> expectedListOfProfile = new List<Profile>();
//             expectedListOfProfile.Add(prof);

//             //mocking dependencies
//             Mock<IProfileRepository> mockRepo = new Mock<IProfileRepository>();

//             //guantee dependency will always work
//             mockRepo.Setup(repo => repo.GetAllProfile()).Returns(expectedListOfProfile);

//             // pass mock version of Profile
//             IProfileBL profBL = new ProfileBL(mockRepo.Object);

//             List<Profile> actualCustSearch = profBL.SearchProfileFromNumber("123-123-1230");

//             //act and assert
//             Assert.Same(expectedListOfProfile[0], actualCustSearch[0]);
//         }
        

//         [Fact]
//         public void NewCustFoundFromNum(){
//             int _profomId = 9001;
//             string _profName = "Max";
//             int _profAge = 22;
//             string _profAddress = "UtopiaZexal";
//             string _profNumber = "123-123-1230";
//             string _userName = "fakeusername";
//             string _password = "fakepassword";
//             int _authority = 0;

//             Profile prof = new Profile(){
//                 profId = _profomId,
//                 Name = _profName,
//                 Age = _profAge,
//                 Address = _profAddress,
//                 PhoneNumber = _profNumber,
//                 UserName = _userName,
//                 Password = _password,
//                 Authority = _authority
//             };

//             List<Profile> expectedListOfProfile = new List<Profile>();
//             expectedListOfProfile.Add(prof);

//             //mocking dependencies
//             Mock<IProfileRepository> mockRepo = new Mock<IProfileRepository>();

//             //guantee dependency will always work
//             mockRepo.Setup(repo => repo.GetAllProfile()).Returns(expectedListOfProfile);

//             // pass mock version of Profile
//             IProfileBL profBL = new ProfileBL(mockRepo.Object);

//             List<Profile> actualCustSearch = profBL.SearchProfileFromNumber("123-123-1230");

//             //act and assert
//             Assert.Same(expectedListOfProfile[0], actualCustSearch[0]);
//         }
    
//         [Fact]
//         public void FailedToFindCustFromEMail(){
//             int _profomId = 9001;
//             string _profName = "Max";
//             int _profAge = 22;
//             string _profAddress = "UtopiaZexal";
//             string _email = "maxEmailExists@gmail.com";
//             string _profNumber = "123-123-1230";
//             string _userName = "fakeusername";
//             string _password = "fakepassword";
//             int _authority = 0;

//             Profile prof = new Profile(){
//                 profId = _profomId,
//                 Name = _profName,
//                 Age = _profAge,
//                 Address = _profAddress,
//                 PhoneNumber = _profNumber,
//                 UserName = _userName,
//                 Password = _password,
//                 Authority = _authority
//             };

//             List<Profile> expectedListOfProfile = new List<Profile>();
//             expectedListOfProfile.Add(prof);

//             //mocking dependencies
//             Mock<IProfileRepository> mockRepo = new Mock<IProfileRepository>();

//             //guantee dependency will always work
//             mockRepo.Setup(repo => repo.GetAllProfile()).Returns(expectedListOfProfile);

//             // pass mock version of Profile
//             IProfileBL profBL = new ProfileBL(mockRepo.Object);

//             List<Profile> actualListOfProfile = profBL.GetAllProfiles();

//             //act and assert
            
//             Assert.Throws<System.Exception>(
//                 () => profBL.SearchProfileFromEMail("fakeEmail@fake.com")
//             ); 
//         }
//     }
// }