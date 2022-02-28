using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBL;
using ShopModel;

/*
    created using:
    dotnet aspnet-codegenerator controller -name Customer -api -outDir Controllers -actions

    Auttogeneratie by utilizing aspnet-codegenrator tool
    https://docs.mo

    -To start
    --intsall tool first - dotnet tool install -g dotnet-sapnet-coegenerator
    --add package to api project - dotnet add package Microsoft.VisualStudio.WebCodeGeneration.Design

    -To Create a controller
    dotnet aspnet-codegenerator controller -name Customer -api -outDir Controllers -actions

    "dotnet aspent-codegenerator controller" - creates a controller

    "-name {NameOfController}" - names the controller to hwatever you put

    :-api" maeks the controller restful style api

    "_outDir Controllers - put controller insie controller folder in api prjcet
    "-action" - adds in action(methods) in your controller
*/



namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerBL _custBL;
        public CustomerController(ICustomerBL c_custBL){
            _custBL = c_custBL;
        }


        /// <summary>
        /// Gathers all the customer's information from sql server
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult GetAllCustomers()
        {
            try{
                Log.Information("Getting All Customers information");
                return Ok(_custBL.GetAllCustomers()); 
            }
            catch(SqlException)
            {
                Log.Information("No customers retrieved");
                return NotFound();
            }
            
        }
/*
        [HttpGet("GetAllAsync")]
        public IActionResult GetAllCustomers()
        {
            try{
                return Ok(_custBL.GetAllCustomersA()); 
            }
            catch(SqlException)
            {
                return NotFound();
            }
            
        }

        
*/
        /// <summary>
        /// Gets the information of a specific customer from the sql server
        /// </summary>
        /// <param name="custName"></param>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetCustomerByName([FromQuery] string custName = "")
        {
            try{
                if(string.IsNullOrWhiteSpace(custName)){
                    Log.Information("Error: Name is empty");
                    return BadRequest(new{Result = "Error, Name is empty"}); // form not completed
                }
                Log.Information("Searching for Customer" + custName);
                return Ok( _custBL.SearchCustomer(custName) ); 
            }
            catch(System.Exception exe)
            {
                Log.Information(exe.Message);
                return NotFound(exe.Message);
            }
            
        }

        /// <summary>
        /// Adds a cutomer to the sql server
        /// </summary>
        /// <param name="cust"></param>
        /// <returns></returns>
        [HttpPost("AddNewCustomer")]
        public IActionResult Post([FromBody] Customer cust)
        {
            try{
                Log.Information("Adding a new Customer");
                return Created( "Successfully added", _custBL.AddCustomer(cust) );
            }
            catch(System.Exception exe){
                Log.Information(exe.Message);
                return Conflict(exe.Message); // 400 ex
            }
        }

        /// <summary>
        /// updates customer information given their Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="c_cust"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public IActionResult Put([FromQuery] int id,[FromBody] Customer c_cust)
        {
            if(id == 0){
                Log.Information("Error: Id is empty");
                return BadRequest(new{Result = "Error, Id is empty"}); // form not completed
            }
            c_cust.custId = id;
            try{
                Log.Information("Updating customer information");
                return Ok( _custBL.UpdateCustomer(c_cust) ); 
            }
            catch(System.Exception exe){
                Log.Information(exe.Message);
                return Conflict(exe.Message); // 400 ex
            }
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        /// <summary>
        /// From the username and password of the customer, checks to see if that person is a manager or customer
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("CheckCredentials")]
        public IActionResult CheckManagorialCredentials(string username,string password)
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
                Log.Information("Checking Customer Security Clearance");
                return Ok( _custBL.CheckAuthorityClearance(_custBL.GetCustomerFromLogin(username,password),1) ); 
            }
            catch(System.Exception exe)
            {
                Log.Information(exe.Message);
                return Conflict(exe.Message);
            }
            
        }


        /*
        public IActionResult AddCustomer([FromBoddy] CustomerRegisterForm p_customer){
            try{
                if(p_customer.IsNullOrWhiteSpace(p_customer.UserName)){
                    return BadRequest(new{Result = "Error, username is empty}); // form not completed
                }
                if(p_customer.IsNullOrWhiteSpace(p_customer.Password)){
                    return BadRequest(new{Result = "Error, username is empty}); // form not completed
                }
                if(p_customer.IsNullOrWhiteSpace(p_customer.Name)){
                    return BadRequest(new{Result = "Error, Name is empty}); // form not completed
                }
                if(p_customer.IsNullOrWhiteSpace(p_customer.Age)){
                    return BadRequest(new{Result = "Error, age is empty}); // form not completed
                }
                if(p_customer.IsNullOrWhiteSpace(p_customer.Address)){
                    return BadRequest(new{Result = "Error, address is empty}); // form not completed
                }
                if(p_customer.IsNullOrWhiteSpace(p_customer.Email)){
                    return BadRequest(new{Result = "Error, email is empty}); // form not completed
                }
                if(p_customer.IsNullOrWhiteSpace(p_customer.PhoneNumber)){
                    return BadRequest(new{Result = "Error, phone number is empty}); // form not completed
                }
                //add data to customer table
                Customer _register Customer = new Customer();
                _registerCustomer.Name = p_customer.Name;
                _registerCustomer.Name = p_customer.Age;
                _registerCustomer.Name = p_customer.Address;
                _registerCustomer.Name = p_customer.Email;
                _registerCustomer.Name = p_customer.PhoneNumber;

                _storeBL.AddCustomer(_registerCustomer);
                // log info
                //Log.Information("Added new user successfully " + _registerCustomer);
                
                //add data to userdata table
                UserData _registerUser = newUserData();
                _register.Username = p_customer.UserName;
                _registerUser.Password = p_customer.Password

                _storeBL.AddCust

                return Created("Sucessfully added", p_customer);

            }
            catch(System.Exception ex){
                return Conflict(ex.Message);
            }
        }

        */
    }
}
