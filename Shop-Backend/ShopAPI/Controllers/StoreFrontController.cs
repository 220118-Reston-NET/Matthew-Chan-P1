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

namespace ShopApi.Controllers
{
    [AllowAnonymous]//[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StoreFrontController : ControllerBase
    {
        private IStoreManagementBL _storeBL;
        //private IProfileBL _profBL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string? given_name;
        public StoreFrontController(IStoreManagementBL s_storeBL, /*IProfileBL c_profBL*/
                                    IHttpContextAccessor httpContextAccessor,
                                    UserManager<ApplicationUser> userManager){
            _storeBL = s_storeBL;
            //_profBL = c_profBL;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;

            /*var token = _httpContextAccessor.HttpContext.Request.Headers["authorization"].Single().Split(" ").Last();
            var tokenHandler = new JwtSecurityTokenHandler();
            given_name = tokenHandler.ReadJwtToken(token).Payload["given_name"].ToString(); */
        }

        // Post: api/StoreFront
        //[Authorize(Roles = "Manager")]
        [HttpPost(RouteConfigs.StoreFronts)]
        public async Task<IActionResult> AddStoreFront( StoreFrontDto s_storeFront )
        {
            try
            {
                s_storeFront.StoreId = Guid.NewGuid().ToString();
                Log.Information("Getting profile information");
                var _result = await _storeBL.AddNewStoreFront(s_storeFront);
                return Ok(_result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.StoreFronts + ": " + e.Message);
                return NotFound(e.Message);
            }
        }



        // GET: api/StoreFront
        //[Authorize(Roles = "Customer")]
        [HttpGet(RouteConfigs.StoreFronts)]
        public async Task<IActionResult> GetStoreFronts(){
            try{
                Log.Information("Getting all store fronts");
                return Ok(await _storeBL.GetAllStoreFronts() );
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.StoreFronts + ": " + exe.Message);
                return NotFound(exe.Message);
            }
        }

        // Get: api/StoreFront/{???}
        //[Authorize(Roles = "Customer")]
        [HttpGet(RouteConfigs.StoreFrontName)]
        public async Task<IActionResult> GetStoreFrontByName(string storeFrontName)
        {
            try{
                Log.Information("Getting store's from name");
                return Ok(await _storeBL.GetStoreFrontByName(storeFrontName) );
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.StoreFrontName + ": " + exe.Message);
                return NotFound(exe.Message);
            }
        }

        // Get: api/StoreFront/{???}
        //[Authorize(Roles = "Customer")]
        [HttpGet(RouteConfigs.StoreFront)]
        public async Task<IActionResult> GetStoreFrontById(string storeFrontId)
        {
            try{
                Log.Information("Getting store's from Id");
                return Ok(await _storeBL.GetStoreFrontById(storeFrontId) );
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.StoreFront + ": " + exe.Message);
                return NotFound(exe.Message);
            }
        }

        // Get: api/StoreFront/{???}
        //[Authorize(Roles = "Customer")]
        [HttpGet(RouteConfigs.FindProduct)]
        public async Task<IActionResult> GetStoresWithProduct(string productId)
        {
            try{
                Log.Information("Getting stores with product");
                return Ok(await _storeBL.GetAllStoresWithProduct(productId) );
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.FindProduct + ": " + exe.Message);
                return NotFound(exe.Message);
            }
        }

        // Post: api/StoreFront
        //[Authorize(Roles = "Manager")]
        [HttpPost(RouteConfigs.Products)]
        public async Task<IActionResult> AddProduct( ProductDto productDto )
        {
            try
            {
                productDto.ProductId = Guid.NewGuid().ToString();
                Log.Information("Getting Product information");
                return Ok(await _storeBL.AddNewProduct(productDto));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.Products + ": " + e.Message);
                return NotFound(e.Message);
            }
        }

        // Get: api/StoreFront
        //[Authorize(Roles = "Customer")]
        [HttpGet(RouteConfigs.Products)]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                Log.Information("Getting all products");
                return Ok(await _storeBL.GetAllProducts());
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.Products + ": " + e.Message);
                return NotFound(e.Message);
            }
        }

        // Get: api/StoreFront
        //[Authorize(Roles = "Customer")]
        [HttpGet(RouteConfigs.ProductId)]
        public async Task<IActionResult> GetProductsById(string productId)
        {
            try
            {
                Log.Information("Getting product: " + productId);
                return Ok(await _storeBL.GetProductById(productId));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.ProductId + ": " + e.Message);
                return NotFound(e.Message);
            }
        }

        // Get: api/StoreFront
        //[Authorize(Roles = "Customer")]
        [HttpGet(RouteConfigs.ProductName)]
        public async Task<IActionResult> GetProductsByName(string productName)
        {
            try
            {
                Log.Information("Getting product: " + productName);
                return Ok(await _storeBL.GetProductByName(productName));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.ProductName + ": " + e.Message);
                return NotFound(e.Message);
            }
        } 


        // Post: api/StoreFront
        //[Authorize(Roles = "Manager")]
        [HttpPost(RouteConfigs.Inventories)]
        public async Task<IActionResult> AddInventory(InventoryDto inventoryDto)
        {
            try
            {
                inventoryDto.InventoryId = Guid.NewGuid().ToString();
                Log.Information("Adding an inventory");
                InventoryDto _result = await _storeBL.AddInventory(inventoryDto);
                Console.WriteLine("Test1");
                Console.WriteLine(_result.InventoryId);
                Console.WriteLine("Test2");
                return Ok(_result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Rounte:" + RouteConfigs.Inventories + ": " + e.Message);
                return NotFound(e.Message);
            }
        }

        // Get: api/StoreFront
        //[Authorize(Roles = "Customer")]
        [HttpGet(RouteConfigs.SearchInventory)]
        public async Task<IActionResult> GetStoreInventoryByStoreId(string storeFrontId)
        {
            try
            {
                Log.Information("Adding an inventory");
                return Ok(await _storeBL.GetStoreInventoryByStoreId(storeFrontId) );
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.Inventory + ": " + e.Message);
                return NotFound(e.Message);
            }
        }

        // Get: api/StoreFront
        //[Authorize(Roles = "Manager")]
        [HttpPut(RouteConfigs.Inventory)]
        public async Task<IActionResult> UpdateInventory(string inventoryId, int amount, string updateType)
        {
            try
            {
                Log.Information("Updateing inventory quantity");
                return Ok(await _storeBL.UpdateInventory(inventoryId, amount, updateType) );
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.Inventory + ": " + e.Message);
                return NotFound(e.Message);
            }
        }

        // Get: api/StoreFront
        //[Authorize(Roles = "Manager")]
        [HttpDelete(RouteConfigs.Inventory)]
        public async Task<IActionResult> RemoveInventory(string inventoryId)
        {
            try
            {
                Log.Information("Updating inventory quantity");
                return Ok(await _storeBL.RemoveInventory(inventoryId) );
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.Inventory + ": " + e.Message);
                return NotFound(e.Message);
            }
        }

    }
}
