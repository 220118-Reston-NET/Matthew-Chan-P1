
using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class RestockMenu : IMenu
    {
        //static non-access modifier is needed to keep this variable consistent to all objects we create out of our AddPokeMenu
        private static int storeId;
        private static int productId;
        private static int amount;

        //Dependency Injection
        //==========================
        private IStoreFrontBL _storeBL;
        public RestockMenu(IStoreFrontBL s_storeBL)
        {
            _storeBL = s_storeBL;
        }
        //==========================

        public void Display()
        {
            Console.WriteLine("Enter StoreFront information");
            Console.WriteLine("[4] Store ID - " + storeId );
            Console.WriteLine("[3] Product ID - " + productId );
            Console.WriteLine("[2] Amount:" + amount);
            Console.WriteLine("[1] Update/Add");
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
                    if(storeId == 0 || productId == 0){
                        Console.WriteLine("Please enter a store and product");
                        Console.ReadLine();
                        return "Restock";
                    }
                    try
                    {
                        _storeBL.CheckValidProductInStore(storeId, productId);
                        _storeBL.RestockInventory(productId, storeId, amount);
                    }
                    catch (System.Exception exc)
                    {
                        _storeBL.AddItemToInventory(productId, storeId, amount);
                    }
                    return "MainMenu";
                case "2":
                    if(storeId == 0 || productId == 0){
                        Console.WriteLine("Please enter a store and product");
                        Console.ReadLine();
                        return "Restock";
                    }
                    try
                    {
                        Console.WriteLine("Please enter the amount of products to be replenished!");
                        amount = Convert.ToInt32(Console.ReadLine());
                        
                    }
                    catch(FormatException){
                        Console.WriteLine("Error: Invalid Id");
                        Console.WriteLine("Please press enter to continue.");
                        Console.ReadLine();
                    }
                    return "Restock";
                case "3":
                    try
                    {
                        _storeBL.DisplayAllProducts();
                        Console.WriteLine("Please enter the Product ID!");
                        int tempProductId = Convert.ToInt32(Console.ReadLine());
                        if(_storeBL.CheckValidProductInStore(storeId,tempProductId)){
                            productId = tempProductId;
                        }
                        return "Restock";
                    }
                    catch(FormatException){
                        Console.WriteLine("Error: Invalid Id");
                        Console.WriteLine("Please press enter to continue.");
                        Console.ReadLine();
                        return "Restock";
                    }
                    catch(Exception exe){
                        Console.WriteLine(exe.Message);
                        Console.WriteLine("Please press enter to continue.");
                        Console.ReadLine();
                        return "Restock";
                    }
                case "4":
                    try{
                        Console.WriteLine("Here are the stores: ");
                        _storeBL.DisplayAllStoreFronts();
                        Console.WriteLine("Please enter the Store ID!");
                        int tempAmount = Convert.ToInt32(Console.ReadLine());
                        if(_storeBL.CheckValidStoreId(tempAmount)){
                            storeId = tempAmount;
                        }
                        return "Restock";
                    }
                    catch(FormatException){
                        Console.WriteLine("Error: Invalid Id");
                        Console.WriteLine("Please press enter to continue.");
                        Console.ReadLine();
                        return "Restock";
                    }
                case "5":
                    if(storeId < 1 || productId < 1){
                        Console.WriteLine("Please change the store ID and product ID");
                        return "Restock";
                    }
                    try
                    {
                        _storeBL.AddItemToInventory(productId, storeId, amount);
                    }
                    catch (System.Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                        return "Restock";
                    }
                    return "MainMenu";
                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return "Restock";
            }
        }
    }
}
