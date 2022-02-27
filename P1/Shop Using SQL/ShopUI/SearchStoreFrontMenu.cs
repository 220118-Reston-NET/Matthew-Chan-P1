
using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class SearchStoreFrontMenu : IMenu
    {
        private IStoreFrontBL _storeBL;
        public SearchStoreFrontMenu(IStoreFrontBL p_storeBL)
        {
            _storeBL = p_storeBL;
        }

        public void Display()
        {
            Console.WriteLine("Please select an option to filter the shop database");
            //Console.WriteLine("[2] By Store Name");
            Console.WriteLine("[1] By Product");
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
                    List<Product> listOfStoreFrontProducts = _storeBL.GetAllProducts();
                    Console.WriteLine("Here are the list of products: ");
                    //Logic to grab user input
                    for(int i = 0; i < listOfStoreFrontProducts.Count; i++){
                        Console.WriteLine(listOfStoreFrontProducts[i].prodId + " " + listOfStoreFrontProducts[i].Name);
                    }
                    try{
                        Console.WriteLine("Please enter the product Id.");
                        int productId = Convert.ToInt32(Console.ReadLine());
                    
                        //Logic to display the result
                        List<StoreFront> listOfStoreFromNameFromProduct = _storeBL.checkStoresForAProduct(productId);
                        
                        if(_storeBL.CheckIfEmpty(listOfStoreFromNameFromProduct) == true){
                            Console.WriteLine("We could not find a store with that product. Please try another method");
                            Console.WriteLine("Please press Enter to continue");
                            Console.ReadLine();
                            return "SearchStoreFront";
                        }
                        foreach (var item in listOfStoreFromNameFromProduct)
                        {
                            Console.WriteLine("================");
                            Console.WriteLine(item);
                        } 
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();

                        return "MainMenu";
                    }
                    catch(FormatException){
                        Console.WriteLine("Please input a valid ID.");
                        Console.WriteLine("Please press enter to continue.");
                        Console.ReadLine();
                        return "SearchStoreFront";
                    }
                     
                case "2":
                    //Logic to grab user input
                    Console.WriteLine("Please enter a name");
                    string name = Console.ReadLine();

                    //Logic to display the result
                    List<StoreFront> listOfStoreFromName = _storeBL.SearchStoreFrontName(name);
                    
                    if(_storeBL.CheckIfEmpty(listOfStoreFromName) == true){
                        Console.WriteLine("We could not find a store from that name. Please try another method");
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                        return "SearchStoreFront";
                    }

                    Console.WriteLine("The store " + listOfStoreFromName[0].Name + " was found.");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();

                    return "MainMenu";
                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return "SearchStoreFront";

            }
        }
    }
}

