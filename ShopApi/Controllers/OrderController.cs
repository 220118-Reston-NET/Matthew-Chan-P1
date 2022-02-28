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


        /// <summary>
        /// Gets all the Orders from every single customer and returns it in json format
        /// </summary>
        /// <returns></returns>
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

        
        /// <summary>
        /// From a customer Id, displays the details of a customer's order
        /// </summary>
        /// <param name="orderFromCustId"></param>
        /// <returns></returns>
        [HttpGet("CustOrder")]
        public IActionResult GetOrderByCustId([FromQuery]int orderFromCustId)
        {
            try{
                List<Order> ord = _orderBL.GetACustomerOrder(orderFromCustId);
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
        
        /// <summary>
        /// Displays all the orders placed in a specific shop
        /// </summary>
        /// <param name="GetOrderFromShopId"></param>
        /// <returns></returns>
        [HttpGet("ShopOrder/")]
        public IActionResult GetOrderByShopId([FromQuery] int GetOrderFromShopId)
        {
            try{
                List<Order> ord = _orderBL.GetAShopOrder(GetOrderFromShopId);
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

        /// <summary>
        /// Recieves input and adds an order to the cart(does not commit the order)
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="storeId"></param>
        /// <param name="prodId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
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
                return Created("Sucessfully added an item to the shopping cart", _orderBL.AddCart(ord, custId, storeId ));
            } 
            catch(System.Exception exe){
                return Conflict(exe.Message); 
            }
        }

        /// <summary>
        /// checks to see all the orders in the cart
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// deletes all the orders in the cart
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// commits all the orders from the shopping card into orders
        /// </summary>
        /// <returns></returns>
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
