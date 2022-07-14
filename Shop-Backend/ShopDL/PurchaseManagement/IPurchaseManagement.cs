using System.Data.SqlClient;
using Shop.Models;


namespace Shop.DatabaseManagement.Interfaces
{
    public interface IPurchaseManagementDL
    {
        Task<ShoppingCartDto>AddToCart(ShoppingCartDto shoppingCart);
        Task<List<ShoppingCartDto>>GetUserCart(string userId);
        Task<ShoppingCartDto>GetShoppingCartByCartId(string shoppingCartId);
        Task<ShoppingCartDto>UpdateShoppingCartQuantity(string shoppingCartId, int quantity);
        Task<ShoppingCartDto>RemoveFromCart(string shoppingCartId);

        Task<OrderDto> OrderFromCart(string userId, string shopId);
        Task<List<OrderDto>> GetUserOrders(string userId);
        Task<List<OrderDto>> GetShopOrders(string shopId);
    }
} 