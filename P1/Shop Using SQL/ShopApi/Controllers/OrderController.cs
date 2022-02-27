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
    public class OrderController : ControllerBase
    {
        private IOrderBL _orderBL;
        public OrderController(IOrderBL o_orderBL){
            _orderBL = o_orderBL;
        }
        // GET: api/Order


        // GET: api/Order/6
        [HttpGet("GetAllOrder")]
        public IActionResult GetAllOrder()
        { 
            try{
                return Ok(_orderBL.GetAllOrder()); 
            }
            catch(SqlException)
            {
                return NotFound();
            }
            
        }

        
        // GET: api/Order/7
        [HttpGet("GetByCust")]
        public IActionResult GetOrderByCustId(int custId)
        {
            try{
                List<Order> ord = _orderBL.GetACustomerOrder(custId);
                string orderDetails = "";
                for(int i = 0; i < ord.Count; i++){
                    orderDetails = ord[i].ToReadableFormat();
                }
                return Ok( orderDetails  ); 
 
            }
            catch(SqlException)
            {
                return NotFound();
            }
            
        }  
        
        [HttpGet("GetByShop/GetOrderFromShopId")]
        public IActionResult GetOrderByShopId(int shopId)
        {
            try{
                List<Order> ord = _orderBL.GetAShopOrder(shopId);
                string orderDetails = "";
                for(int i = 0; i < ord.Count; i++){
                    orderDetails = ord[i].ToReadableFormat();
                }
                return Ok( orderDetails  ); 
            }
            catch(SqlException)
            {
                return NotFound();
            }
            
        } 
/*
        // POST: api/Customer
        [HttpPost("Add")]
        public IActionResult Post([FromBody] Order ord, int custId, int storeId)
        {
            try{
                return Ok( _orderBL.AddOrder(ord, custId, storeId) );
            }
            catch(System.Exception exe){
                return Conflict(exe.Message); // 400 ex
            }
        }
*/

/*
        // POST: api/Customer
        [HttpPost("Add1Order")]
        public IActionResult Post(int custId, int storeId, int prodId, int quantity)
        {
            try{
                Product prod = _orderBL.ProductIdToProduct(prodId);
                List<LineItem> newLineItemList = new List<LineItem>{new LineItem(prod,quantity)};
                List<string> newStoreAddressStringList = new List<string>{_orderBL.ConvertSFIdToSFAddress(storeId)};
                Order ord = new Order(-1, newLineItemList, newStoreAddressStringList, prod.Price * quantity);

                _orderBL.CheckValidProduct(storeId,prodId);
                _orderBL.CheckValidAge(custId, prodId);
                _orderBL.checkOrder(prodId, 0, ord, storeId); // 0 = quantity


                return Ok( ord );
            }
            catch(System.Exception exe){
                return Conflict(exe.Message); // 400 ex
            } 
        }
*/
        [HttpPost("Cart/AddAnOrderToCart")]
        public IActionResult Post(int custId, int storeId, int prodId, int quantity) {
            try {
                Product prod = _orderBL.ProductIdToProduct(prodId);
                List<LineItem> newLineItemList = new List<LineItem>{new LineItem(prod,quantity)};
                List<string> newStoreAddressStringList = new List<string>{_orderBL.ConvertSFIdToSFAddress(storeId)};
                Order ord = new Order(-1, newLineItemList, newStoreAddressStringList, prod.Price * quantity);

                _orderBL.CheckValidProduct(storeId,prodId);
                _orderBL.CheckValidAge(custId, prodId);
                _orderBL.checkOrder(prodId,quantity, _orderBL.GetAllCart(), storeId);
                return Ok( _orderBL.AddCart(ord, custId, storeId ));
            } 
            catch(System.Exception exe){
                return Conflict(exe.Message); 
            }
        }
        [HttpGet("Cart/GetCartOrders")]
        public IActionResult Get() {
            try{
                Order ord = _orderBL.GetAllCart();
                string orderDetails = "";
                orderDetails = ord.ToReadableFormat();
                
                return Ok(ord); 
            }
            catch(SqlException)
            {
                return NotFound();
            }
        }
        [HttpDelete("Cart/DeleteCartOrders")]
        public IActionResult Delete() {
            try{
                _orderBL.ClearCart();
                return Ok("Cart Cleared"); 
            }
            catch(SqlException)
            {
                return NotFound();
            }
        }

/*
        // PUT: api/Customer/5
        [HttpPut]
        public IActionResult Put([FromBody] Order ord)
        {
            try{
                return Ok( _OrderBL.UpdateOrder(ord) );
            }
            catch(System.Exception exe){
                return Conflict(exe.Message); // 400 ex
            }
        }




        try{
                Order ord = _orderBL.GetAllCart();
                string orderDetails = "";
                orderDetails = ord.ToReadableFormat();
                
                return Ok(orderDetails); 
            }
            catch(SqlException)
            {
                return NotFound();
            }
*/


        [HttpPost("CommitOrder")]
        public IActionResult Post()
        {
            try{
                _orderBL.AddOrder();
                return Ok( "Order sent" );
            }
            catch(System.Exception exe){
                return Conflict(exe.Message); // 400 ex
            } 
        }


        // DELETE: api/Order/5
        [HttpDelete("id")]
        public void Delete(int id)
        {
        }
    }
}
