using ShopModel;

namespace ShopBL
{
    
    public interface ICustomerBL {

        /// <summary>
        /// Adds a customer to the sql database
        /// </summary>
        /// <param name="c_cust"></param>
        /// <returns></returns>
        Customer AddCustomer (Customer c_cust);

        /// <summary>
        /// gets all the information from all the customers in the sql database
        /// </summary>
        /// <returns></returns>
        List<Customer> GetAllCustomers();

        /// <summary>
        /// checks to see if the username exists
        /// </summary>
        /// <param name="username"></param>
        void CheckValidUserName(string username);
        /// <summary>
        /// gets the customer information from the sql databased based on the inputted Id
        /// </summary>
        /// <param name="c_custId"></param>
        /// <returns></returns>
        public List<Customer> SearchCustomerFromCustId(int c_custId);
        /// <summary>
        /// looks for a customer from the sql database based on the inputted name
        /// </summary>
        /// <param name="c_name"></param>
        /// <returns></returns>
        List<Customer> SearchCustomer(string c_name);
        /// <summary>
        /// looks for a customer from their phone number
        /// </summary>
        /// <param name="c_pnum"></param>
        /// <returns></returns>
        List<Customer> SearchCustomerFromNumber(string c_pnum);
        /// <summary>
        /// looks for a customer from their email
        /// </summary>
        /// <param name="c_email"></param>
        /// <returns></returns>
        List<Customer> SearchCustomerFromEMail(string c_email);
        /// <summary>
        /// checks to see if the list of customers is empty
        /// </summary>
        /// <param name="listOfCust"></param>
        /// <returns></returns>
        bool CheckIfEmpty(List<Customer> listOfCust); 
        
        /// <summary>
        /// checks to see if the phone number inputted is valid
        /// </summary>
        /// <param name="userInput"></param>
        void CheckIfValidPhoneNumber(string userInput);

        
        /// <summary>
        /// Updates the customer information
        /// </summary>
        /// <param name="c_cust"></param>
        /// <returns></returns>
        public Customer UpdateCustomer(Customer c_cust);

        /// <summary>
        /// Gets the customer infromation from using the username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Customer GetCustomerFromLogin(string username, string password);
        /// <summary>
        /// checks to see if the person has authority for whatever action is being implimeneted
        /// </summary>
        /// <param name="c"></param>
        /// <param name="minClearanceLevel"></param>
        /// <returns></returns>
        public bool CheckAuthorityClearance(Customer c, int minClearanceLevel);

    } 

    
    
/*    public interface IProductBL {
        Product AddProduct (Product p_prod);

        List<Product> SearchProduct(string p_name);
        List<Product> GetAllCustomer();
    }
*/
    public interface IStoreFrontBL{
        /// <summary>
        /// adds a store to the sql database
        /// </summary>
        /// <param name="s_store"></param>
        /// <returns></returns>
        StoreFront AddStoreFront (StoreFront s_store);
        /// <summary>
        /// looks for a store in the sql database given a name
        /// </summary>
        /// <param name="s_store"></param>
        /// <returns></returns>
        List<StoreFront> SearchStoreFrontName(string s_store);
        /// <summary>
        /// returns all of the stores in the sql database
        /// </summary>
        /// <returns></returns>
        List<StoreFront> GetAllStoreFronts();
        /// <summary>
        /// returns the stores that have the specified product
        /// </summary>
        /// <param name="s_product"></param>
        /// <returns></returns>
        List<StoreFront> SearchStoreFrontProducts(string s_product);
        /// <summary>
        /// displays the name of all the stores
        /// </summary>
        void DisplayAllStoreFronts();

        /// <summary>
        /// returns a list of all the products and its information
        /// </summary>
        /// <returns></returns>
        List<Product> GetAllProducts();
        /// <summary>
        /// returns a list of all the inventories and the information it contains
        /// </summary>
        /// <returns></returns>
        List<Inventory> GetAllInventory();

        /// <summary>
        /// returns an inventory based on the shopId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Inventory GetSpecificInventory(int sId);

        /// <summary>
        /// adds or restocks an item to an invnetory
        /// </summary>
        /// <param name="p_prodId"></param>
        /// <param name="s_storeId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Inventory AddOrRestock(int p_prodId, int s_storeId, int amount);

        //List<Product> GetProductsFromShopId(int id);

        /// <summary>
        /// checks to see if the store Id is valid
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        bool CheckValidStoreId(int storeId);

        /// <summary>
        /// returns a list of the stores that contain a product
        /// </summary>
        /// <param name="prodId"></param>
        /// <returns></returns>
        List<StoreFront> checkStoresForAProduct(int prodId);

        /// <summary>
        /// displays the products in an inventory
        /// </summary>
        /// <param name="inv"></param>
        void printProductsInInventory(Inventory inv);

        /// <summary>
        /// restocks the inventory
        /// </summary>
        /// <param name="p_prodId"></param>
        /// <param name="s_storeId"></param>
        /// <param name="amount"></param>
        void RestockInventory(int p_prodId, int s_storeId, int amount);

        /// <summary>
        /// adds an item to the sql database for a store(pls do not add an existing item)
        /// </summary>
        /// <param name="p_prodId"></param>
        /// <param name="s_storeId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Inventory AddItemToInventory(int p_prodId, int s_storeId, int amount);

        /// <summary>
        /// checks to see if the list of customers is empty(thorw error if true)
        /// </summary>
        /// <param name="listOfCust"></param>
        /// <returns></returns>
        bool CheckIfEmpty(List<StoreFront> listOfCust);

        /// <summary>
        /// displays all of the products
        /// </summary>
        void DisplayAllProducts();
        
        /// <summary>
        /// checks too see if the product exists in sql database
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        bool CheckValidProduct(int pId);

        /// <summary>
        /// checks to see if the product is in the store
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="pId"></param>
        /// <returns></returns>
        bool CheckValidProductInStore(int storeId, int pId);
    }

    /* public interface IInventoryBL{
        List<StoreFront> SearchInventoryFromStoreId(int s_Id);
        List<Inventory> GetAllInventories();
        List<Products> GetProductsFromShopId(int ShopId);

        bool CheckIfEmpty(List<StoreFront> listOfCust);
    } */

    public interface IProductBL{
        Product AddProduct(Product prod);
        List<Product> GetAllProducts();
    }

    public interface IOrderBL{

        /// <summary>
        /// adds an order to the sql orders
        /// </summary>
        /// <param name="ord"></param>
        /// <param name="custID"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        Order AddOrder(Order ord, int custID, int storeID);

        /// <summary>
        /// adds the add the shopping cart order into the list of orders
        /// </summary>
        /// <returns></returns>
        Order AddOrder();

        /// <summary>
        /// converts the product id to a product class
        /// </summary>
        /// <param name="prodId"></param>
        /// <returns></returns>
        Product ProductIdToProduct(int prodId);

        /// <summary>
        /// gets all of the orders made
        /// </summary>
        /// <returns></returns>
        List<Order> GetAllOrder();

        /// <summary>
        /// returns all of the orders the customer made
        /// </summary>
        /// <param name="cId"></param>
        /// <returns></returns>
        List<Order> GetACustomerOrder( int cId);

        /// <summary>
        /// gets all the orders in a shop
        /// </summary>
        /// <param name="sId"></param>
        /// <returns></returns>
        List<Order> GetAShopOrder( int sId);    

        /// <summary>
        /// checks to see if you can add the new product into the order/shopping cart based on the existing orders and shop inventory
        /// </summary>
        /// <param name="prodId"></param>
        /// <param name="quantity"></param>
        /// <param name="orders"></param>
        /// <param name="sId"></param>
        /// <returns></returns>
        bool checkOrder(int prodId, int quantity, Order orders, int sId);
        
        /// <summary>
        /// checks to see if the store has the product
        /// </summary>
        /// <param name="sId"></param>
        /// <param name="pId"></param>
        /// <returns></returns>
        bool CheckValidProduct(int sId, int pId);

        string DisplayAllOrdersInReadableFormat(List<Order> o);

        ////////////////
        
        
        /// <summary>
        /// converts the storefront Id to the address
        /// </summary>
        /// <param name="sId"></param>
        /// <returns></returns>
        string ConvertSFIdToSFAddress(int sId);

        /// <summary>
        /// Returns an inventory based in the inputted store Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Inventory GetSpecificInventory(int id);

        /// <summary>
        /// checks to see if the customer Id is valid
        /// </summary>
        /// <param name="cId"></param>
        /// <returns></returns>
        
        bool CheckValidCId(int cId);
        /// <summary>
        /// displays the products in the inventory
        /// </summary>
        /// <param name="inv"></param>
        void printProductsInInventory(Inventory inv);
        /// <summary>
        /// displays the products in the inventory
        /// </summary>
        /// <param name="inv"></param>
        /// <param name="ord"></param>
        void printProductsInInventory(Inventory inv, Order ord);

        /// <summary>
        /// checks to see if the customer is old enough to buy product
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="prodId"></param>
        /// <returns></returns>
        bool CheckValidAge(int custId, int prodId);

        /// <summary>
        /// checks to see if the store id is valid
        /// </summary>
        /// <param name="sId"></param>
        /// <returns></returns>
        bool CheckValidStoreId(int sId);

        /// <summary>
        /// displays all the names of the store fornts
        /// </summary>
        void DisplayAllStoreFronts();

        /// <summary>
        /// adds an order to the shopping cart
        /// </summary>
        /// <param name="o_order"></param>
        /// <param name="custId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        Order AddCart(Order o_order, int custId, int storeId);
        
        /// <summary>
        /// gets the stored orders in the cart
        /// </summary>
        /// <returns></returns>
        Order GetAllCart();

        /// <summary>
        /// clears the shopping cart
        /// </summary>
        void ClearCart();

    }
    
}
