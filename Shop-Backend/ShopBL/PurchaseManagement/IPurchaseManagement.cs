using Shop.Models;

namespace Shop.BuisnessManagement.Interfaces
{
    public interface IPurchaseManagementBL
    {
        Task<ShoppingCartDto>AddToCart(ShoppingCartDto shoppingCart);
        Task<List<ShoppingCartDto>>GetUserCart(string userId);
        Task<ShoppingCartDto>UpdateShoppingCartQuantity(string shoppingCartId, int newQuantity);
        Task<ShoppingCartDto>RemoveFromCart(string shoppingCartId);

        Task<OrderDto> OrderFromCart(string userId, string shopId);
        Task<List<OrderDto>> GetUserOrders(string userId);
        Task<List<OrderDto>> GetShopOrders(string shopId);
        

    }
}