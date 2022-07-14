using Microsoft.EntityFrameworkCore;
using Shop.DatabaseManagement.Interfaces;
using Shop.Models;


namespace Shop.DatabaseManagement.Implements
{
    public class PurchaseManagementDL : IPurchaseManagementDL
    {
        private readonly ShopContext _context;

        public PurchaseManagementDL(ShopContext c_context)
        {
            _context = c_context;
        }
        
        public async Task<ShoppingCartDto>AddToCart(ShoppingCartDto shoppingCart)
        {
            await _context.ShoppingCarts.AddAsync(ShoppingCartDtoToShoppingCart(shoppingCart));
            _context.SaveChanges();
            return shoppingCart;
        }
        //restrict to admin
        public async Task<List<ShoppingCartDto>>GetUserCart(string userId)
        {
            List<ShoppingCartDto> _result = await _context.ShoppingCarts
                                                        .Select(s => new ShoppingCartDto()
                                                        {
                                                            ShoppingCartId = s.ShoppingCartId,
                                                            UserId = s.UserId,
                                                            ProductId = s.ProductId,
                                                            StoreId = s.StoreId,
                                                            Quantity = s.Quantity
                                                        })
                                                        .Where(s => s.UserId.Equals(userId) )
                                                        .ToListAsync();
            if(_result != null)
            {
                return _result;
            }
            else
            {
                throw new Exception("Error. User does not have anything in cart");
            }
        }

        public async Task<ShoppingCartDto>GetShoppingCartByCartId(string shoppingCartId)
        {
            ShoppingCartDto? _result = await _context.ShoppingCarts
                                                    .Select(s => new ShoppingCartDto()
                                                    {
                                                        ShoppingCartId = s.ShoppingCartId,
                                                        UserId = s.UserId,
                                                        ProductId = s.ProductId,
                                                        StoreId = s.StoreId,
                                                        Quantity = s.Quantity
                                                    })
                                                    .FirstOrDefaultAsync(s => s.ShoppingCartId.Equals(shoppingCartId));
            if(_result != null)
            {
                return _result;
            }
            else
            {
                throw new Exception("Error. User does not have anything in cart");
            }
        }
        public async Task<ShoppingCartDto>UpdateShoppingCartQuantity(string shoppingCartId, int newQuantity)
        {
            ShoppingCart? shoppingCartToUpdate = await _context.ShoppingCarts.FirstOrDefaultAsync(s => s.ShoppingCartId == shoppingCartId);
            if (shoppingCartToUpdate != null) {
                shoppingCartToUpdate.Quantity = newQuantity;
                await _context.SaveChangesAsync();
                return ShoppingCartToDto(shoppingCartToUpdate);
            }
            else
            {
                throw new Exception("Error. Inventory not found. Inventory Could not be deleted");
            }
        }

        public async Task<ShoppingCartDto>RemoveFromCart(string shoppingCartId)
        {
            ShoppingCart? shoppingCartToRemove = await _context.ShoppingCarts.FirstOrDefaultAsync(s => s.ShoppingCartId == shoppingCartId);
            if (shoppingCartToRemove != null) {
                _context.ShoppingCarts.Remove(shoppingCartToRemove);
                await _context.SaveChangesAsync();
                return ShoppingCartToDto(shoppingCartToRemove);
            }
            else
            {
                throw new Exception("Error. Inventory not found. Inventory Could not be deleted");
            }
            
        }

        public async Task<OrderDto> OrderFromCart(string userId, string shopId)
        {
            List<ShoppingCartDto>? ShoppingCartToRemove = ((await GetUserCart(userId)).Where(s => shopId.Equals(shopId) )).ToList();
            OrderDto tempOrderDto = new OrderDto()
            {
                OrderId = Guid.NewGuid().ToString(),
                UserId = userId,
                StoreId = shopId,
                creationTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
            };
            await _context.Orders.AddAsync( OrderDtoToOrder(tempOrderDto) );
            LineItem tempLineItem = new LineItem();
            foreach(ShoppingCartDto SingleShoppingCart in ShoppingCartToRemove)
            {
                tempLineItem = ShoppingCartDtoToLineItem(SingleShoppingCart);
                await _context.LineItems.AddAsync( tempLineItem );
                await _context.OrderToLineItems.AddAsync( IdsToOrderToLineItem( tempOrderDto.OrderId, tempLineItem.LineItemId ) );
                _context.SaveChanges();
                await DecreaseInventory( SingleShoppingCart.StoreId, SingleShoppingCart.ProductId, SingleShoppingCart.Quantity);
                await RemoveFromCart(SingleShoppingCart.ShoppingCartId);
            }
            _context.SaveChanges();
            return tempOrderDto;
        }

        public async Task<List<OrderDto>> GetUserOrders(string userId)
        {
            return await _context.Orders
                                    .Select(o => new OrderDto()
                                    {
                                        OrderId = o.OrderId,
                                        UserId = o.UserId,
                                        StoreId = o.StoreId,
                                        creationTime = o.creationTime
                                    })
                                    .Where(o => o.UserId == userId)
                                    .ToListAsync() ;
        }
        public async Task<List<OrderDto>> GetShopOrders(string shopId)
        {
            return await _context.Orders
                                    .Select(o => new OrderDto()
                                    {
                                        OrderId = o.OrderId,
                                        UserId = o.UserId,
                                        StoreId = o.StoreId,
                                        creationTime = o.creationTime
                                    })
                                    .Where(o => o.StoreId == shopId)
                                    .ToListAsync() ;
        }

        private ShoppingCartDto ShoppingCartToDto(ShoppingCart s_shoppingCart)
        {
            ShoppingCartDto _shoppingCartDto = new ShoppingCartDto()
            {
                ShoppingCartId = s_shoppingCart.ShoppingCartId,
                UserId = s_shoppingCart.UserId,
                ProductId = s_shoppingCart.ProductId,
                StoreId = s_shoppingCart.StoreId,
                Quantity = s_shoppingCart.Quantity
            };
            return _shoppingCartDto;
        }
        private ShoppingCart ShoppingCartDtoToShoppingCart(ShoppingCartDto s_shoppingCartDto)
        {
            ShoppingCart _shoppingCart = new ShoppingCart()
            {
                ShoppingCartId = s_shoppingCartDto.ShoppingCartId,
                UserId = s_shoppingCartDto.UserId,
                ProductId = s_shoppingCartDto.ProductId,
                StoreId = s_shoppingCartDto.StoreId,
                Quantity = s_shoppingCartDto.Quantity
            };
            return _shoppingCart;
        }
        private LineItem ShoppingCartDtoToLineItem(ShoppingCartDto shoppingCartDto)
        {
            var productId = shoppingCartDto.ProductId;
            LineItem lineItem = new LineItem()
            {
                LineItemId = Guid.NewGuid().ToString(),
                ProductId = productId,
                ItemQuantity = shoppingCartDto.Quantity,
                TotalPrice = shoppingCartDto.Quantity * _context.Products.FirstOrDefault(s => s.ProductId == productId).ProductPrice
            };
            return lineItem;
        }
        private OrderDto ShoppingCartDtoToOrderDto(ShoppingCartDto shoppingCartDto)
        {
            OrderDto orderDto = new OrderDto()
            {
                OrderId = Guid.NewGuid().ToString(),
                UserId = shoppingCartDto.UserId,
                StoreId = shoppingCartDto.StoreId,
                creationTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
            };
            return orderDto;
        }

        private OrderDto OrderToDto(Order o_order)
        {
            OrderDto _shoppingCartDto = new OrderDto()
            {
                OrderId = o_order.OrderId,
                UserId = o_order.UserId,
                StoreId = o_order.StoreId,
                creationTime = o_order.creationTime
            };
            return _shoppingCartDto;
        }
        private Order OrderDtoToOrder(OrderDto o_orderDto)
        {
            Order _shoppingCart = new Order()
            {
                OrderId = o_orderDto.OrderId,
                UserId = o_orderDto.UserId,
                StoreId = o_orderDto.StoreId,
                creationTime = o_orderDto.creationTime
            };
            return _shoppingCart;
        }

        private OrderToLineItem IdsToOrderToLineItem (string o_orderId, string l_lineItemId)
        {
            OrderToLineItem _orderToLineItem = new OrderToLineItem()
            {
                OrderToLineItemId = Guid.NewGuid().ToString(),
                OrderId = o_orderId,
                LineItemId = l_lineItemId
            };
            return _orderToLineItem;
        }

        private async Task<InventoryDto> DecreaseInventory(string storeId, string productId, int decreasedAmount)
        {
            Inventory? inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.StoreId == storeId && i.ProductId == productId);
            if(inventory == null)
                throw new Exception("Inventory DNE");
            inventory.ProductQuantity -= decreasedAmount;
            await _context.SaveChangesAsync();
            InventoryDto InventoryDto = new InventoryDto()
            {
                InventoryId = inventory.InventoryId,
                StoreId = inventory.StoreId,
                ProductId = inventory.ProductId,
                ProductQuantity = inventory.ProductQuantity
            };
            return InventoryDto;
            
        }
        
        /*public Purchase UpdatePurchase(Purchase o_Purchase)
        {
            // Purchase ordToUpdate = _context.Purchases.FirstOrDefault(p => p.PurchaseId == o_Purchase.PurchaseId);
            // if(ordToUpdate != null )
            // {
            //     ordToUpdate.ProfileId
                
            // }
            return o_Purchase;
        } */
    }
}