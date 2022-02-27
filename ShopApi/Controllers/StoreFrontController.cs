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

        [HttpGet("Inventory/GetAllInventory")]
        public IActionResult GetInventory(){
            try{
                return Ok( _storeBL.GetAllInventory());
            }
            catch(System.Exception exe)
            {
                return Conflict(exe.Message);
            }
        }
        [HttpGet("Inventory")]
        public IActionResult GetInventory([FromQuery] int sId){
            try{
                return Ok( _storeBL.GetSpecificInventory(sId));
            }
            catch(System.Exception exe)
            {
                return Conflict(exe.Message);
            }
        }

        [HttpGet("Restock")]
        public IActionResult CheckManagorialCredentials(string username,string password, int prodId, int storeId, int amount)
        {
            try{
                Customer c = _custBL.GetCustomerFromLogin(username,password);
                _custBL.CheckAuthorityClearance(c,1);
                return Ok( _storeBL.AddOrRestock(prodId, storeId, amount)); 
            }
            catch(System.Exception exe)
            {
                return Conflict(exe.Message);
            }
            
        }


    }
}
