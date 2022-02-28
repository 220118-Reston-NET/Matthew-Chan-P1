using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBL;
using ShopModel;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreFrontController : ControllerBase
    {
        private IStoreFrontBL _storeBL;
        private ICustomerBL _custBL;
        public StoreFrontController(IStoreFrontBL s_storeBL, ICustomerBL c_custBL){
            _storeBL = s_storeBL;
            _custBL = c_custBL;
        }

        /// <summary>
        /// Displays every single store's inventory
        /// </summary>
        /// <returns></returns>
        [HttpGet("Inventory/GetAllInventory")]
        public IActionResult GetInventory(){
            try{
                Log.Information("Getting all of the inventory");
                return Ok( _storeBL.GetAllInventory());
            }
            catch(System.Exception exe)
            {
                Log.Information(exe.Message);
                return Conflict(exe.Message);
            }
        }

        /// <summary>
        /// Displays a single store's inventory(selected by the shop id)
        /// </summary>
        /// <param name="sId"></param>
        /// <returns></returns>
        [HttpGet("Inventory")]
        public IActionResult GetInventory([FromQuery] int sId){
            try{
                if(sId == 0){
                    Log.Information("Error: id is empty");
                    return BadRequest(new{Result = "Error, id is empty"});
                }
                Log.Information("Getting inventory from shop " + sId);
                return Ok( _storeBL.GetSpecificInventory(sId));
            }
            catch(System.Exception exe)
            {
                Log.Information(exe.Message);
                return Conflict(exe.Message);
            }
        }

        /// <summary>
        /// Restocks the inventory if the person restocking is a manager
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="prodId"></param>
        /// <param name="storeId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpGet("Restock")]
        public IActionResult CheckManagorialCredentials(string username,string password, int prodId, int storeId, int amount)
        {
            try{
                if(string.IsNullOrWhiteSpace(username)){
                    Log.Information("Error: username is empty");
                    return BadRequest(new{Result = "Error, username is empty"});
                }
                if(string.IsNullOrWhiteSpace(password)){
                    Log.Information("Error: password is empty");
                    return BadRequest(new{Result = "Error, password is empty"});
                }
                Log.Information("Checking Credentials");
                Customer c = _custBL.GetCustomerFromLogin(username,password);
                _custBL.CheckAuthorityClearance(c,1);
                Log.Information("Credentials cleared and restocking/adding items to shop");
                return Ok( _storeBL.AddOrRestock(prodId, storeId, amount)); 
            }
            catch(System.Exception exe)
            {
                Log.Information(exe.Message);
                return Conflict(exe.Message);
            }
        }
    }
}
