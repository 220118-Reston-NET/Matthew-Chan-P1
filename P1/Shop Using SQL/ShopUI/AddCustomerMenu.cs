using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class AddCustomerMenu : IMenu
    {
        //static non-access modifier is needed to keep this variable consistent to all objects we create out of our AddPokeMenu
        private static Customer _newCust = new Customer();

        //Dependency Injection
        //==========================
        private ICustomerBL _custBL;
        public AddCustomerMenu(ICustomerBL s_custBL)
        {
            _custBL = s_custBL;
        }
        //==========================

        public void Display()
        {
            Console.WriteLine("Enter Customer information");
            Console.WriteLine("[6] Name - " + _newCust.Name );
            Console.WriteLine("[5] Age - " + _newCust.Age);
            Console.WriteLine("[4] Address - " + _newCust.Address );
            Console.WriteLine("[3] Email - " + _newCust.Email );
            Console.WriteLine("[2] Phone number - " + _newCust.PhoneNumber);
            Console.WriteLine("[1] Save");
            Console.WriteLine("[0] Go Back");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "0":
                    return "MainMenu";
                case "1":
                    //Exception handling to have a better user experience
                    try
                    {
                        _custBL.AddCustomer(_newCust);
                    }
                    catch (System.Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                    }
                    return "MainMenu";
                case "2":
                    try{

                        Console.WriteLine("Please enter a phone number in \"XXX-XXX-XXXX\" format: ");
                        string tempNumber = Console.ReadLine();
                        
                        _custBL.CheckIfValidPhoneNumber(tempNumber);
                        _newCust.PhoneNumber = tempNumber;

                        
                        Console.WriteLine("Phone number added");
                        Console.ReadLine();

                    }
                    catch(FormatException)
                    {
                        Console.WriteLine("Error, wrong format. Returning you back to customer menu");
                        Console.ReadLine();
                    }
                    catch(System.Exception exc){
                        Console.WriteLine(exc.Message);
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                        
                    }
                    return "AddCustomer";
                case "3":
                    Console.WriteLine("Please enter an email!");
                    _newCust.Email = Console.ReadLine();
                    return "AddCustomer";
                case "4":
                    Console.WriteLine("Please enter an address!");
                    _newCust.Address = Console.ReadLine();
                    return "AddCustomer";
                case "5":
                    try{
                        Console.WriteLine("Please enter a Age!");
                        int submittedAge = Convert.ToInt32(Console.ReadLine());
                        if(submittedAge > 0 && submittedAge < 130)
                        {
                            _newCust.Age = submittedAge;
                        }
                        else{
                            Console.WriteLine("Invalid Age. Returning to Customer menu");
                            Console.ReadLine();
                        }
                    }
                    catch{
                        Console.WriteLine("Please input a valid response.\n Returning to customer menu.");
                        Console.ReadLine();
                    }
                    return "AddCustomer";
                case "6":
                    Console.WriteLine("Please enter a name!");
                    _newCust.Name = Console.ReadLine();
                    return "AddCustomer";
                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return "AddCustomer";
            }
        }
    }
}