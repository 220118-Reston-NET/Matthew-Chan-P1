// using CRUDShopDL;
// using ShopModel;

// // verification for entering name, phone, and email
// namespace ShopBL{
//     public class CrudProfileBL: IProfileBL {
//         // Dependency Injection Pattern
//         // ==================================
//         private IProfileRepository _crepo;
//         private I???Repository
//         public ProfileBL(IProfileRepository c_repo){
//             _crepo = c_repo;
//             _repo = o_repo;
//         }
//         // ==================================

        
        
//         public Profile AddProfile(Profile c_profomer){
//             return _crepo.Add(c_profomer);
//         } 
        
//         /*
//         public Profile GetProfileByCustId(int c_profId){
//             return _crepo.GetProfileByCustId(c_profId);
//         }
//         */
//         public List<Profile> GetAllProfiles(){
//             return _crepo.GetAll();
//         }
//         public void CheckValidUserName(string username){
//             List<Profile> listOfProfiles = _crepo.GetAll();
//             foreach(Profile c in listOfProfiles)
//             {
//                 if(c.UserName == username){
//                     throw new Exception("Error, invalid username");
//                 }
//             }
//         }

//         public Profile GetProfileFromLogin(string username, string password){
//             List<Profile> listOfProfiles = _repo.GetAllProfile();
//             foreach(Profile c in listOfProfiles){
//                 if(c.UserName == username && c.Password == password){
//                     return c;
//                 }
//             }
//             throw new Exception("Profile not found from username + password.");
//         }
//         public bool CheckAuthorityClearance(Profile c, int minClearanceLevel){
//             if(c.Authority >= minClearanceLevel){
//                 return true;
//             }
//             else{
//                 throw new Exception("This profomer does not have the authority to perform this action");
//             }
//         }

//         public List<Profile> SearchProfileFromCustId(int c_Id){
//             List<Profile> listOfProfiles = _repo.GetAllProfile();
            
//             List<Profile> listOfCustWithId = listOfProfiles
//                                 .Where(prof => prof.profId == c_Id)
//                                 .ToList();
//             if(CheckIfEmpty(listOfCustWithId) == true){
//                 throw new Exception("There is no Profile with this ID");
//             }
//             // LINQ library
//             return listOfCustWithId;
//         }
//         public List<Profile> SearchProfile(string c_name){
//             List<Profile> listOfProfiles = _repo.GetAll();
//             // LINQ library
//             List<Profile> listOfProfilesName = listOfProfiles
//                         .Where(prof => prof.Name.Contains(c_name))
//                         .ToList();
//             if(CheckIfEmpty(listOfProfilesName) == true){
//                 throw new Exception("There is no Profile with this ID");
//             }
//             // LINQ library
//             return listOfProfilesName;
//         }
        
//         public List<Profile> SearchProfileFromNumber(string c_pnum){
//             List<Profile> listOfProfiles = _repo.GetAll();
//             // LINQ library
//             return listOfProfiles
//                         .Where(prof => prof.PhoneNumber.Contains(c_pnum))
//                         .ToList();
//         }
//         public List<Profile> SearchProfileFromEMail(string c_email){
//             List<Profile> listOfProfiles = _repo.GetAll();
//             // LINQ library
//             List<Profile> listOfCustFromEmail = listOfProfiles
//                         .Where(prof => prof.Email.Contains(c_email))
//                         .ToList();
//             if(CheckIfEmpty(listOfCustFromEmail) == true){
//                 throw new Exception("There is no Profile with this Email");
//             }
//             return listOfCustFromEmail;
//         }

//         public void CheckIfValidPhoneNumber(string userInput){
//             if(userInput.Length != 12){
//                 throw new Exception("The phone number did not have the correct length\nPlease enter a phone number in \"XXX-XXX-XXXX\" format");
//             }
//             for(int i = 0; i < userInput.Length; i++){
//                 if(userInput[3] != Convert.ToChar("-") || userInput[7] != Convert.ToChar("-")){
//                     throw new Exception("The phone number was in the wrong format\nPlease enter a phone number in \"XXX-XXX-XXXX\" format");
//                 }
//             }
//         }

//         public bool CheckIfEmpty(List<Profile> listOfCust){
//             if(listOfCust.Any() == false){
//                 return true;
//             }
//             else{
//                 return false;
//             }
            
//         }

//         public Profile UpdateProfile(Profile c_prof){
//             try{
//                 return _repo.Update(c_cust);
//             }
//             catch(System.Exception exe){
//                 throw new Exception(exe.Message);
//             }
//         } 
//     }
// }

