
using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class SearchInventoryMenu : IMenu
    {
        private IStoreFrontBL _storeBL;
        public SearchInventoryMenu(IStoreFrontBL p_storeBL)
        {
            _storeBL = p_storeBL;
        }

        public void Display()
        {
            Console.WriteLine("Here are all the shops:");
            _storeBL.DisplayAllStoreFronts();
            Console.WriteLine("Please select the shop(by name)");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            List<StoreFront> listOfStoreFromName = _storeBL.SearchStoreFrontName(userInput);
                    
            if(_storeBL.CheckIfEmpty(listOfStoreFromName) == true){
                Console.WriteLine("We could not find a store from that name. Please try another method");
                Console.WriteLine("Please press Enter to continue");
                Console.ReadLine();
                return "SearchInventory";
            }

            
            Console.WriteLine("The store " + listOfStoreFromName[0].Name + " was found.");
            
            Inventory storeInventory = _storeBL.GetSpecificInventory(listOfStoreFromName[0].storeId);

            _storeBL.printProductsInInventory(storeInventory);            
            
            if(storeInventory.Products.Any() == false ){
                Console.WriteLine("The store is empty.");
            }

            Console.WriteLine("Done");
            Console.ReadLine();
            
            
            return "MainMenu";
            
        }
    }
}

