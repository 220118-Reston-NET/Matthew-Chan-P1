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

        // GET: api/Customer
        [HttpGet("GetAll")]
        public IActionResult GetAllCustomers()
        {
            try{
                return Ok(_custBL.GetAllCustomers()); 
            }
            catch(SqlException)
            {
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
        // GET: api/Customer/5
        [HttpGet()]
        public IActionResult GetCustomerByName([FromQuery] string custName)
        {
            try{
                if(string.IsNullOrWhiteSpace(custName)){
                    return BadRequest(new{Result = "Error, Name is empty"}); // form not completed
                }
                return Ok( _custBL.SearchCustomer(custName) ); 
            }
            catch(SqlException)
            {
                return NotFound();
            }
            
        }

        // POST: api/Customer
        [HttpPost("AddNewCustomer")]
        public IActionResult Post([FromBody] Customer cust)
        {
            try{
                return Created( "Successfully added", _custBL.AddCustomer(cust) );
            }
            catch(System.Exception exe){
                return Conflict(exe.Message); // 400 ex
            }
        }

        // PUT: api/Customer/5
        [HttpPut("Update")]
        public IActionResult Put([FromQuery] int id,[FromBody] Customer c_cust)
        {
            c_cust.custId = id;
            try{
                return Ok( _custBL.UpdateCustomer(c_cust) ); // please implement
            }
            catch(System.Exception exe){
                return Conflict(exe.Message); // 400 ex
            }
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("CheckCredentials")]
        public IActionResult CheckManagorialCredentials(string username,string password)
        {
            try{
                return Ok( _custBL.CheckAuthorityClearance(_custBL.GetCustomerFromLogin(username,password),1) ); 
            }
            catch(SqlException)
            {
                return NotFound();
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
