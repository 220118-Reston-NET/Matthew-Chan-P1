using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.ShopAPI.AuthenticationService.Interfaces;
using Shop.ShopAPI.Consts;
using Shop.ShopAPI.DataTransferObjects;
using Shop.BuisnessManagement.Interfaces;
using Shop.Models;

/*
{
  "shoppingCartId": "string",
  "userId": "39268b17-fe22-439b-8488-231e62fe69b0",
  "productId": "765e39c4-9a3c-40e9-a08d-d043ebaa504f",
  "storeId": "ff34135c-1968-4fa1-8388-757152d3f427",
  "quantity": 1
}
*/

namespace ShopApi.Controllers
{
    [AllowAnonymous]//[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IPurchaseManagementBL _orderBL;
        //private IProfileBL _profBL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string? given_name;

        public OrderController(IPurchaseManagementBL o_orderBL,
                                    IHttpContextAccessor httpContextAccessor,
                                    UserManager<ApplicationUser> userManager){
            _orderBL = o_orderBL;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;

            /*var token = _httpContextAccessor.HttpContext.Request.Headers["authorization"].Single().Split(" ").Last();
            var tokenHandler = new JwtSecurityTokenHandler();
            given_name = tokenHandler.ReadJwtToken(token).Payload["given_name"].ToString(); */
        }

        //Post: api/StoreFront
        //[Authorize(Roles = "Customer")]
        [HttpPost(RouteConfigs.ShoppingCarts)]
        public async Task<IActionResult> ShoppingCartDto( ShoppingCartDto shoppingCart )
        {
            try
            {
                shoppingCart.ShoppingCartId = Guid.NewGuid().ToString();
                Log.Information("Getting Shopping Cart information");
                Console.WriteLine("Console " + shoppingCart);
                return Ok(await _orderBL.AddToCart(shoppingCart));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.ShoppingCarts + ": " + e.Message);
                return NotFound(e.Message);
            }
        } 



        // GET: api/StoreFront
        //[Authorize(Roles = "Customer")]
        [HttpGet(RouteConfigs.UserShoppingCart)]
        public async Task<IActionResult> GetUserCart(string userId){
            try{ 
                Log.Information("Getting all User Shopping Cart");
                return Ok(await _orderBL.GetUserCart(userId) );
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.UserShoppingCart + ": " + exe.Message);
                return NotFound(exe.Message);
            }
        }

        // Delete: api/StoreFront
        //[Authorize(Roles = "Customer")]
        [HttpDelete(RouteConfigs.ShoppingCart)]
        public async Task<IActionResult> RemoveFromCart(string shoppingCartId){
            try{ 
                Log.Information("Removing User Shopping Cart");
                return Ok(await _orderBL.RemoveFromCart(shoppingCartId) );
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.ShoppingCart + ": " + exe.Message);
                return NotFound(exe.Message);
            }
        }

        //Post: api/StoreFront
        //[Authorize(Roles = "Customer")]
        [HttpPost(RouteConfigs.Order)]
        public async Task<IActionResult> OrderFromCart( string userId, string storeId )
        {
            try
            {
                Console.WriteLine("Test 0");
                Log.Information("Getting Shopping Cart information");
                return Ok(await _orderBL.OrderFromCart(userId, storeId));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.Order + ": " + e.Message);
                return NotFound(e.Message);
            }
        } 

        // GET: api/StoreFront
        //[Authorize(Roles = "Customer")]
        [HttpGet(RouteConfigs.Order)]
        public async Task<IActionResult> GetUserOrders(string userId){
            try{ 
                Log.Information("Getting all User Orders");
                return Ok(await _orderBL.GetUserOrders(userId) );
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Order + ": " + exe.Message);
                return NotFound(exe.Message);
            }
        }

        // GET: api/StoreFront
        //[Authorize(Roles = "Customer")]
        [HttpGet(RouteConfigs.ShopOrder)]
        public async Task<IActionResult> GetShopOrders(string shopId){
            try{ 
                Log.Information("Getting all Orders for a Shop");
                return Ok(await _orderBL.GetShopOrders(shopId) );
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.ShopOrder + ": " + exe.Message);
                return NotFound(exe.Message);
            }
        }

    }
}
