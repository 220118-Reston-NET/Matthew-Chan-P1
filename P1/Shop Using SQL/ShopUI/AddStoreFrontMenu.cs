
using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class AddStoreFrontMenu : IMenu
    {
        //static non-access modifier is needed to keep this variable consistent to all objects we create out of our AddPokeMenu
        private static StoreFront _newStore = new StoreFront();

        //Dependency Injection
        //==========================
        private IStoreFrontBL _storeBL;
        public AddStoreFrontMenu(IStoreFrontBL s_storeBL)
        {
            _storeBL = s_storeBL;
        }
        //==========================

        public void Display()
        {
            Console.WriteLine("Enter StoreFront information");
            Console.WriteLine("[4] Name - " + _newStore.Name );
            Console.WriteLine("[3] Address - " + _newStore.Address );
            Console.WriteLine("[2] Products: " + _newStore.Inv);
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
                        _storeBL.AddStoreFront(_newStore);
                    }
                    catch (System.Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                    }
                    return "MainMenu";
                /*case "2":
                    Console.WriteLine("Please enter how many products you would like to add!");
                    int numberOfProducts = Convert.ToInt32(Console.ReadLine());
                    for(int i = 0; i < numberOfProducts; i++){
                        Console.Write("Please enter the product name: ");
                        string prodName = Console.ReadLine();
                        Console.Write("Please enter the price of the product: ");
                        int price = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Please enter the quantity of the product: ");
                        string quantity = Console.ReadLine();
                        _newStore.Products.Add(new Product(prodName,price));
                    }
                    return "AddStoreFront"; */
                case "3":
                    Console.WriteLine("Please enter an address!");
                    _newStore.Address = Console.ReadLine();
                    return "AddStoreFront";
                case "4":
                    Console.WriteLine("Please enter a name!");
                    _newStore.Name = Console.ReadLine();
                    return "AddStoreFront";
                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return "AddStoreFront";
            }
        }
    }
}
