

// using CRUDShopDL;

// using ShopModel;

// namespace ShopBL
// {
    
//     public interface IProfileBL {

//         /// <summary>
//         /// Adds a profomer to the sql database
//         /// </summary>
//         /// <param name="c_prof"></param>
//         /// <returns></returns>
//         Profile AddProfile (Profile c_prof);

//         /// <summary>
//         /// gets all the information from all the profomers in the sql database
//         /// </summary>
//         /// <returns></returns>
//         List<Profile> GetAllProfiles();

//         /// <summary>
//         /// checks to see if the username exists
//         /// </summary>
//         /// <param name="username"></param>
//         void CheckValidUserName(string username);
//         /// <summary>
//         /// gets the profomer information from the sql databased based on the inputted Id
//         /// </summary>
//         /// <param name="c_profId"></param>
//         /// <returns></returns>
//         public List<Profile> SearchProfileFromCustId(int c_profId);
//         /// <summary>
//         /// looks for a profomer from the sql database based on the inputted name
//         /// </summary>
//         /// <param name="c_name"></param>
//         /// <returns></returns>
//         List<Profile> SearchProfile(string c_name);
//         /// <summary>
//         /// looks for a profomer from their phone number
//         /// </summary>
//         /// <param name="c_pnum"></param>
//         /// <returns></returns>
//         List<Profile> SearchProfileFromNumber(string c_pnum);
//         /// <summary>
//         /// looks for a profomer from their email
//         /// </summary>
//         /// <param name="c_email"></param>
//         /// <returns></returns>
//         List<Profile> SearchProfileFromEMail(string c_email);
//         /// <summary>
//         /// checks to see if the list of profomers is empty
//         /// </summary>
//         /// <param name="listOfCust"></param>
//         /// <returns></returns>
//         bool CheckIfEmpty(List<Profile> listOfCust); 
        
//         /// <summary>
//         /// checks to see if the phone number inputted is valid
//         /// </summary>
//         /// <param name="userInput"></param>
//         void CheckIfValidPhoneNumber(string userInput);

        
//         /// <summary>
//         /// Updates the profomer information
//         /// </summary>
//         /// <param name="c_prof"></param>
//         /// <returns></returns>
//         public Profile UpdateProfile(Profile c_prof);

//         /// <summary>
//         /// Gets the profomer infromation from using the username and password
//         /// </summary>
//         /// <param name="username"></param>
//         /// <param name="password"></param>
//         /// <returns></returns>
//         public Profile GetProfileFromLogin(string username, string password);
//         /// <summary>
//         /// checks to see if the person has authority for whatever action is being implimeneted
//         /// </summary>
//         /// <param name="c"></param>
//         /// <param name="minClearanceLevel"></param>
//         /// <returns></returns>
//         public bool CheckAuthorityClearance(Profile c, int minClearanceLevel);

//     } 

    
    

// }
