
using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class OrderMenu : IMenu
    {
        public static int custId;
        public static int storeId;
        public static Order Orders = new Order();

        public int numOfOrders = 0;

        public static int costOfOrder = 0;

        //Dependency Injection
        //==========================
        private IOrderBL _orderBL;
        public OrderMenu(IOrderBL o_orderBL)
        {
            _orderBL = o_orderBL;
        }
        //==========================

        public void Display()
        { 
            Console.WriteLine("Would you like to get or make an order?");
            Console.WriteLine("[4] Select a Customer ID to order: " + custId);
            Console.WriteLine("[3] Select a Store ID to order from: " + storeId);
            Console.WriteLine("[2] Order a Product.");
            Console.WriteLine("[5] Display Orders");
            Console.WriteLine("[6] Get Orders from the customer");
            Console.WriteLine("[7] Get Orders from the Store");
            Console.WriteLine("[8] Reset Orders");
            Console.WriteLine("[1] Finalize Orders: ");
            Console.WriteLine("[0] Go Back");

            Console.WriteLine("Current cost of order: " + costOfOrder);
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "0":
                    return "MainMenu";
                case "1":
                    try
                    {
                        //how to get store ID????
                        //for(int i = 0; i < Orders.LineItems.Count; i++)
                        //{
                        _orderBL.AddOrder(Orders, custId, storeId);
                        //}
                        numOfOrders = 0;
                        costOfOrder = 0;
                        Orders = new Order();
                        Console.WriteLine("Orders reset");
                    }
                    catch (System.Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                    }
                    
                    return "MainMenu";                
                case "2":
                    Console.WriteLine("How many items would you like to order!");
                    int amount = Convert.ToInt32(Console.ReadLine());
                    for(int i = 0; i < amount; i++){
                        // need to have something that is able to retrieve a product from name/id
                        Inventory storeInventory = _orderBL.GetSpecificInventory(storeId); // need to make this
                        int prodId = 0;
                        bool check1 = false;
                        bool check2 = false;
                        do{
                            try{
                                Console.WriteLine("===================================================");
                                if(Orders.LineItems.Any()){
                                    _orderBL.printProductsInInventory(storeInventory, Orders);
                                }
                                else
                                {
                                    _orderBL.printProductsInInventory(storeInventory);
                                }
                                Console.WriteLine("What product would you like to order?");
                                prodId = Convert.ToInt32(Console.ReadLine());
                                if(!_orderBL.CheckValidProduct(storeId,prodId)){
                                    Console.WriteLine("invalid product. please try again");
                                }
                                check1 = _orderBL.CheckValidProduct(storeId,prodId);
                                check2 = _orderBL.CheckValidAge(custId, prodId);
                            }
                            catch(FormatException){
                                Console.WriteLine("Please input a valid ID.");
                                Console.WriteLine("Please press enter to continue.");
                                Console.ReadLine();
                            }
                            catch (System.Exception exc)
                            {
                                Console.WriteLine(exc.Message);
                                Console.WriteLine("Please press Enter to continue");
                                Console.ReadLine();
                            }

                        }
                        while(check1 && check2);

                        
                        Product prod = _orderBL.ProductIdToProduct(prodId);
                        int quantity;
                        // need to have a check to make sure customer is not able to go over
                        while(true)
                        {
                            try{
                                Console.WriteLine("How many of the products would you like?");
                                quantity = Convert.ToInt32(Console.ReadLine());
                                if(_orderBL.checkOrder(prodId,quantity, Orders, storeId) == true){ // checkOrder will make sure product does not go over
                                    if(quantity != 0)
                                    {
                                        Orders.AddItemToOrder(new LineItem(prod, quantity), _orderBL.ConvertSFIdToSFAddress(storeId)); // makde COnvertSFIdToSFAddress
                                        numOfOrders++;
                                        costOfOrder += prod.Price * quantity;
                                    }
                                    else{
                                        Console.WriteLine("Item was not added because quantity was 0");
                                    }
                                    break;
                                }
                                else{
                                    Console.WriteLine("order exceeds inventory. please try again");
                                }
                            }
                            catch(FormatException){
                                Console.WriteLine("Please input a valid quantity");
                                Console.WriteLine("Please press enter to continue.");
                                Console.ReadLine();
                                return "PlaceOrder";
                            }
                        }
                    }
                    
                    return "PlaceOrder";
                case "3":   
                    try {
                        _orderBL.DisplayAllStoreFronts();
                        Console.WriteLine("Please enter the Store ID!");
                        int tempStoreId = Convert.ToInt32(Console.ReadLine());
                        _orderBL.CheckValidStoreId(tempStoreId);
                        storeId = tempStoreId;
                    }
                    catch(FormatException){
                        Console.WriteLine("Error: Invalid Id");
                        Console.WriteLine("Please press enter to continue.");
                        Console.ReadLine();
                    }
                    catch(System.Exception exe){
                        Console.WriteLine(exe.Message);
                        Console.WriteLine("Please press enter to conintue.");
                        Console.ReadLine();
                    }
                    return "PlaceOrder";
                case "4":
                    try{
                        Console.WriteLine("Please enter the Customer ID!");
                        int tempCustId = Convert.ToInt32(Console.ReadLine());
                        _orderBL.CheckValidCId(tempCustId);
                        custId = tempCustId;
                    }
                    catch(FormatException){
                        Console.WriteLine("Error: Invalid Id");
                        Console.WriteLine("Please press enter to continue.");
                        Console.ReadLine();
                    }
                    catch(System.Exception exe){
                        Console.WriteLine(exe.Message);
                        Console.WriteLine("Please press enter to conintue.");
                        Console.ReadLine();
                    }
                    return "PlaceOrder";
                case "5":
                    if(!Orders.LineItems.Any()){
                        Console.WriteLine("No orders");
                        Console.ReadLine();
                        return "PlaceOrder";
                    }
                    Console.WriteLine("Here are the orders: ");
                    Console.WriteLine(Orders);
                    Console.WriteLine("END\n Press enter to Continue");
                    Console.ReadLine();
                    return "PlaceOrder";
                case "6":
                    // check to see if customer order is null
                    
                    List<Order> custOrder = _orderBL.GetACustomerOrder( custId);
                    if(!custOrder.Any()){
                        Console.WriteLine("No orders");
                        Console.ReadLine();
                        return "PlaceOrder";
                    }
                    
                    Console.WriteLine("These are the orders from the customer");
                    

                    foreach(Order o in custOrder){
                        Console.WriteLine("=============================");
                        //Console.WriteLine("Order number: " + o.orderNumber);
                        for(int i = 0; i < o.LineItems.Count; i ++){
                            Console.WriteLine(o.LineItems[i].Products.Name + ": " + o.LineItems[i].Quantity );
                        }
                        break;
                    }
                    Console.WriteLine("END\n Press enter to Continue");
                    Console.ReadLine();
                    return "PlaceOrder";
                case "7": 
                    List<Order> shopOrder = _orderBL.GetAShopOrder( storeId);
                    if(!shopOrder.Any()){
                        Console.WriteLine("No orders");
                        Console.ReadLine();
                        return "PlaceOrder";
                    }
                    Console.WriteLine("These are the orders from the customer");
                    foreach(Order o in shopOrder){
                        Console.WriteLine("=============================");
                        for(int i = 0; i < o.LineItems.Count; i ++){
                            Console.WriteLine(o.LineItems[i].Products.Name + ": " + o.LineItems[i].Quantity );
                            
                        }
                        break;
                    }
                    Console.WriteLine("END\n Press enter to Continue");
                    Console.ReadLine();
                    return "PlaceOrder";
                case "8":
                    numOfOrders = 0;
                    costOfOrder = 0;
                    Orders = new Order();
                    Console.WriteLine("Orders reset");
                    return "PlaceOrder";
                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return "PlaceOrder";
            }
        }
    }
}
