using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class SearchCustomerMenu : IMenu
    {
        private ICustomerBL _custBL;
        public SearchCustomerMenu(ICustomerBL c_custBL)
        {
            _custBL = c_custBL;
        }

        public void Display()
        {
            Console.WriteLine("Please select an option to filter the shop database");
            Console.WriteLine("[4] By Name");
            Console.WriteLine("[3] By Email");
            Console.WriteLine("[2] By Phone Number");
            Console.WriteLine("[1] By Unique Customer Number");
            Console.WriteLine("[0] Go back");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    return "MainMenu";
                case "1":
                    try{
                        Console.WriteLine("Enter Customer Id:");
                        int custId = Convert.ToInt32(Console.ReadLine());
                        List<Customer>listOfCustomer = _custBL.SearchCustomerFromCustId(custId);
                        
                        if(_custBL.CheckIfEmpty(listOfCustomer) == true){
                            Console.WriteLine("We could not find a customer from that value. Please try another method");
                            Console.WriteLine("Please press Enter to continue");
                            Console.ReadLine();
                            return "SearchCustomer";
                        }
                        foreach(var item in listOfCustomer){
                            Console.WriteLine("=========");
                            Console.WriteLine(item.Name);
                        }

                        Console.WriteLine("Please press Enter to Continue");
                        Console.ReadLine();
                        return "MainMenu";
                    }
                    catch(FormatException){
                        Console.WriteLine("Please input a valid ID.");
                        Console.WriteLine("Please press enter to continue.");
                        Console.ReadLine();
                        return "SearchCustomer";
                    }
                    catch (System.Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                        return "SearchCustomer";
                    }
                case "2":
                    try{
                        //Logic to grab user input
                        Console.WriteLine("Please enter a phone number in \"XXX-XXX-XXXX\" format: ");
                        string number = Console.ReadLine();

                        _custBL.CheckIfValidPhoneNumber(number);
                        //Logic to display the result
                        List<Customer> listOfCustFromNum = _custBL.SearchCustomerFromNumber(number);
                        foreach (var item in listOfCustFromNum)
                        {
                            Console.WriteLine("================");
                            Console.WriteLine(item);
                        }
                        
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                    }
                    catch(System.Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                        return "SearchCUstomer";
                    }
                    

                    return "MainMenu";
                case "3":
                    //Logic to grab user input
                    Console.WriteLine("Please enter your email: ");
                    string email = Console.ReadLine();
                    //Logic to display the result
                    List<Customer> listOfCustFromEMail = _custBL.SearchCustomerFromEMail(email);
                    if(listOfCustFromEMail.Count > 1){
                        Console.WriteLine("We have multiple customers with that email in our database. please select another identifier.");
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                        return "SearchCustomer";
                    }
                    foreach (var item in listOfCustFromEMail)
                    {
                        Console.WriteLine("================");
                        Console.WriteLine(item);
                    }
                    
                    if(_custBL.CheckIfEmpty(listOfCustFromEMail) == true){
                        Console.WriteLine("We could not find a customer from that email. Please try another method");
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                        return "SearchCustomer";
                    }
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();

                    return "MainMenu";
                case "4":
                    //Logic to grab user input
                    Console.WriteLine("Please enter a name");
                    string name = Console.ReadLine();

                    //Logic to display the result
                    List<Customer> listOfCustFromName = _custBL.SearchCustomer(name);
                    
                    if(_custBL.CheckIfEmpty(listOfCustFromName) == true){
                        Console.WriteLine("We could not find a customer from that Name. Please try another method");
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                        return "SearchCustomer";
                    }
                    if(listOfCustFromName.Count > 1){
                        Console.WriteLine("We have multiple customers with that name in our database. please select another identifier.");
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                        return "SearchCustomer";
                    }
                    foreach (var item in listOfCustFromName)
                    {
                        Console.WriteLine("================");
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();

                    return "MainMenu";
                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return "SearchCustomer";

            }
        }
    }
}
