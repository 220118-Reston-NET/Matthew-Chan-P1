using Shop.Models;
using Shop.BuisnessManagement.Interfaces;
using Shop.DatabaseManagement.Interfaces;

namespace Shop.BuisnessManagement.Implements
{
    public class PurchaseManagementBL : IPurchaseManagementBL
    {
        private readonly IPurchaseManagementDL _purchaseRepo;
        private readonly IStoreManagementBL _storeBL;
        public PurchaseManagementBL(IPurchaseManagementDL purchaseRepo, IStoreManagementBL storeBL)
        {
            _purchaseRepo = purchaseRepo;
            _storeBL = storeBL;
        }

        public async Task<ShoppingCartDto>AddToCart(ShoppingCartDto shoppingCart)
        {
            // checks if inventory exists
            int inventoryQuantity;
            try
            {
                inventoryQuantity = ((await _storeBL.GetStoreInventoryByStoreId( shoppingCart.StoreId )).SingleOrDefault(s => s.ProductId.Equals( shoppingCart.ProductId ) )).ProductQuantity;
            }
            catch(System.Exception e)
            {
                throw new Exception(e.Message);
            }    
            if(shoppingCart.Quantity <= 0)
            {
                throw new Exception("Erro: Order quantity must be a positive integer");
            }
            if(shoppingCart.Quantity > inventoryQuantity  )//check to see if the quantity you're trying to change to is over the store's capacity
            {
                throw new Exception("Error: Quantity cannot exceed the inventory");
            }
            return await _purchaseRepo.AddToCart(shoppingCart);
        }
        //restrict to admin
        public async Task<List<ShoppingCartDto>>GetUserCart(string userId)
        {
            try
            {
                return await _purchaseRepo.GetUserCart(userId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }


        public async Task<ShoppingCartDto>UpdateShoppingCartQuantity(string shoppingCartId, int newQuantity)
        {
            try
            {
                if(newQuantity < 0)
                {
                    throw new Exception("Error: Invalid Input. Cannot have negative quantity");
                    
                    
                }
                ShoppingCartDto tempShoppingCart = await _purchaseRepo.GetShoppingCartByCartId( shoppingCartId );
                if(newQuantity > ((await _storeBL.GetStoreInventoryByStoreId( tempShoppingCart.StoreId )).FirstOrDefault(s => s.ProductId.Equals( tempShoppingCart.ProductId ) )).ProductQuantity  )//check to see if the quantity you're trying to change to is over the store's capacity
                {
                    throw new Exception("Error: Quantity cannot exceed the inventory");
                }
                return await UpdateShoppingCartQuantity(shoppingCartId, newQuantity);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }

        public async Task<ShoppingCartDto>RemoveFromCart(string shoppingCartId)
        {
            try
            {
                return await _purchaseRepo.RemoveFromCart(shoppingCartId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }

        public async Task<OrderDto> OrderFromCart(string userId, string shopId)
        {
            try
            {
                List<ShoppingCartDto> shoppingCartDtoList = ((await GetUserCart(userId)).Where(s => shopId.Equals(shopId))).ToList();
                int tempQuantity = 0;
                foreach(ShoppingCartDto shoppingCartDto in shoppingCartDtoList)
                {
                    tempQuantity = shoppingCartDto.Quantity;
                    // this check is in case some else's purchase causes this shopping cart's purchases to go over the shop inventory
                    if(tempQuantity > ((await _storeBL.GetStoreInventoryByStoreId( shoppingCartDto.StoreId )).FirstOrDefault(s => s.ProductId.Equals( shoppingCartDto.ProductId ) )).ProductQuantity  )//check to see if the quantity you're trying to change to is over the store's capacity
                    {
                        throw new Exception("Error: Quantity from shopping card ID: " + shoppingCartDto.ShoppingCartId + " is exceeding the inventory");
                    }
                    else
                    {
                        Console.WriteLine( ((await _storeBL.GetStoreInventoryByStoreId( shoppingCartDto.StoreId )).FirstOrDefault(s => s.ProductId.Equals( shoppingCartDto.ProductId ) )).ProductQuantity);
                    }
                }
                return await _purchaseRepo.OrderFromCart(userId, shopId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }

        public async Task<List<OrderDto>> GetUserOrders(string userId)
        {
            try
            {
                return await _purchaseRepo.GetUserOrders(userId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
            
        }
        public async Task<List<OrderDto>> GetShopOrders(string shopId)
        {
            try
            {
                return await _purchaseRepo.GetShopOrders(shopId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
            
        }
    }
}