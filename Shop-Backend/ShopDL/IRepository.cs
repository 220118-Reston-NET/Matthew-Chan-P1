using Shop.Models;

namespace ShopDL{
    public interface IProfileRepository
    {
        Profile AddProfile(Profile c_prof);

        List<Profile> GetAllProfile();

        //async Task<List<Profile>> GetAllProfileAsync();
        //List<Profile> GetProfileByCustId(int c_profId);
        Profile UpdateProfile(Profile c_prof);
    }
    
    public interface IStoreFrontRepository
    {
        StoreFront AddStoreFront(StoreFront s_store);
        List<StoreFront> GetAllStoreFront();



        //Inventory AddInventory(Inventory i_inven);
        List<Inventory> GetAllInventory();
        Inventory GetAnInventory(int id);
        Inventory RestockInventory(int p_prodId, int s_storeId, int amount);
        Inventory AddItemToInventory(int p_prodId, int s_storeId, int amount);
        
        List<Product> GetAllProducts();

            


        
    }
    public interface IOrderRepository{
        Order AddOrder(Order o_order, int profId, int storeId);
        Order AddOrder();
        List<Order> GetAllOrder();
        Product ProductIdToProduct(int prodId);
        List<Order> GetAProfileOrder( int cId);    
        List<Order> GetAShopOrder( int sId);

        string StoreFrontIdToAddress(int prodId);

        

        Inventory GetAnInventory(int s_storeId);

        List<Profile> GetAllProfile();
        List<Product> GetAllProducts();
        List<StoreFront> GetAllStoreFront();


        Order AddCart(Order o_order, int profId, int storeId);
        Order GetAllCart();
        void ClearCart();
    }

    public interface IProductRepository{
        Product AddProduct(Product prod);
        List<Product> GetAllProducts();
        //List<string> GetStoreFrontsThatContainProduct(int prodId);
        Product ProductIdToProduct(int prodId);
    } 

}