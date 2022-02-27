/*

using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class AddOrderMenu : IMenu
    {
        //static non-access modifier is needed to keep this variable consistent to all objects we create out of our AddOrder
        private static Order _newOrder = new Order();
        private ICustomerBL _custBL;
        //Dependency Injection
        //==========================
        private IOrderBL _OrderBL;
        public AddOrderMenu(IOrderBL o_orderBL, ICustomerBL c_custBL)
        {
            _OrderBL = o_orderBL;
            _custBL = c_custBL;
        }
        //==========================
        
        

        public void Display()
        {
            Console.WriteLine("Please enter your customer id and name");
            int custId = Convert.ToInt32(Console.ReadLine());
            string custName = Console.ReadLine();


            Console.WriteLine("Enter Customer information");
            
            Console.WriteLine("[1] Save");
            Console.WriteLine("[0] Go Back");
        }

        public string UserChoice()
        {
            /* string userInput = Console.ReadLine();
                
            } star/////
            return "SearchInventory";
        }
    }
}

*/