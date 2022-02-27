using ShopModel;

namespace ShopBL
{
    
    public interface ICustomerBL {
        Customer AddCustomer (Customer c_cust);

        List<Customer> GetAllCustomers();
        void CheckValidUserName(string username);
        public List<Customer> SearchCustomerFromCustId(int c_custId);

        List<Customer> SearchCustomer(string c_name);
        List<Customer> SearchCustomerFromNumber(string c_pnum);
        List<Customer> SearchCustomerFromEMail(string c_email);
        
        bool CheckIfEmpty(List<Customer> listOfCust); 
        

        void CheckIfValidPhoneNumber(string userInput);

        

        public Customer UpdateCustomer(Customer c_cust);

        public Customer GetCustomerFromLogin(string username, string password);
        public bool CheckAuthorityClearance(Customer c, int minClearanceLevel);

    } 

    
    
/*    public interface IProductBL {
        Product AddProduct (Product p_prod);

        List<Product> SearchProduct(string p_name);
        List<Product> GetAllCustomer();
    }
*/
    public interface IStoreFrontBL{
        StoreFront AddStoreFront (StoreFront s_store);
        List<StoreFront> SearchStoreFrontName(string s_store);
        List<StoreFront> GetAllStoreFronts();
        List<StoreFront> SearchStoreFrontProducts(string s_product);

        void DisplayAllStoreFronts();


        List<Product> GetAllProducts();
        List<Inventory> GetAllInventory();
        Inventory GetSpecificInventory(int id);

        Inventory AddOrRestock(int p_prodId, int s_storeId, int amount);

        //List<Product> GetProductsFromShopId(int id);
        bool CheckValidStoreId(int storeId);

        List<StoreFront> checkStoresForAProduct(int prodId);

        void printProductsInInventory(Inventory inv);

        void RestockInventory(int p_prodId, int s_storeId, int amount);

        Inventory AddItemToInventory(int p_prodId, int s_storeId, int amount);

        bool CheckIfEmpty(List<StoreFront> listOfCust);


        void DisplayAllProducts();
        

        bool CheckValidProduct(int pId);

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
        Order AddOrder(Order ord, int custID, int storeID);
        Order AddOrder();

        Product ProductIdToProduct(int prodId);

        List<Order> GetAllOrder();
        List<Order> GetACustomerOrder( int cId);
        List<Order> GetAShopOrder( int sId);    

        bool checkOrder(int prodId, int quantity, Order orders, int sId);
        bool CheckValidProduct(int sId, int pId);

        

        ////////////////
        string ConvertSFIdToSFAddress(int sId);

        Inventory GetSpecificInventory(int id);
        
        bool CheckValidCId(int cId);
        void printProductsInInventory(Inventory inv);
        void printProductsInInventory(Inventory inv, Order ord);

        bool CheckValidAge(int custId, int prodId);

        bool CheckValidStoreId(int sId);

        void DisplayAllStoreFronts();

        Order AddCart(Order o_order, int custId, int storeId);
        Order GetAllCart();
        void ClearCart();

    }
    
}
