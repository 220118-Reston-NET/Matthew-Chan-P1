using ShopDL;
using ShopModel;

// verification for entering name, phone, and email
namespace ShopBL{
    public class CustomerBL: ICustomerBL {
        // Dependency Injection Pattern
        // ==================================
        private ICustomerRepository _repo;

        public CustomerBL(ICustomerRepository c_repo){
            _repo = c_repo;
        }
        // ==================================

        
        
        public Customer AddCustomer(Customer c_customer){
            return _repo.AddCustomer(c_customer);
        } 
        
        /*
        public Customer GetCustomerByCustId(int c_custId){
            return _repo.GetCustomerByCustId(c_custId);
        }
        */
        public List<Customer> GetAllCustomers(){
            return _repo.GetAllCustomer();
        }
        public void CheckValidUserName(string username){
            List<Customer> listOfCustomers = _repo.GetAllCustomer();
            foreach(Customer c in listOfCustomers)
            {
                if(c.UserName == username){
                    throw new Exception("Error, invalid username");
                }
            }
        }

        public Customer GetCustomerFromLogin(string username, string password){
            List<Customer> listOfCustomers = _repo.GetAllCustomer();
            foreach(Customer c in listOfCustomers){
                if(c.UserName == username && c.Password == password){
                    return c;
                }
            }
            throw new Exception("Customer not found from username + password.");
        }
        public bool CheckAuthorityClearance(Customer c, int minClearanceLevel){
            if(c.Authority >= minClearanceLevel){
                return true;
            }
            else{
                throw new Exception("This customer does not have the authority to perform this action");
            }
        }

        public List<Customer> SearchCustomerFromCustId(int c_Id){
            List<Customer> listOfCustomers = _repo.GetAllCustomer();
            
            List<Customer> listOfCustWithId = listOfCustomers
                                .Where(cust => cust.custId == c_Id)
                                .ToList();
            if(CheckIfEmpty(listOfCustWithId) == true){
                throw new Exception("There is no Customer with this ID");
            }
            // LINQ library
            return listOfCustWithId;
        }
        public List<Customer> SearchCustomer(string c_name){
            List<Customer> listOfCustomers = _repo.GetAllCustomer();
            // LINQ library
            List<Customer> listOfCustomersName = listOfCustomers
                        .Where(cust => cust.Name.Contains(c_name))
                        .ToList();
            if(CheckIfEmpty(listOfCustomersName) == true){
                throw new Exception("There is no Customer with this ID");
            }
            // LINQ library
            return listOfCustomersName;
        }
        
        public List<Customer> SearchCustomerFromNumber(string c_pnum){
            List<Customer> listOfCustomers = _repo.GetAllCustomer();
            // LINQ library
            return listOfCustomers
                        .Where(cust => cust.PhoneNumber.Contains(c_pnum))
                        .ToList();
        }
        public List<Customer> SearchCustomerFromEMail(string c_email){
        List<Customer> listOfCustomers = _repo.GetAllCustomer();
        // LINQ library
        return listOfCustomers
                    .Where(cust => cust.Email.Contains(c_email))
                    .ToList();
        }

        public void CheckIfValidPhoneNumber(string userInput){
            if(userInput.Length != 12){
                throw new Exception("The phone number did not have the correct length\nPlease enter a phone number in \"XXX-XXX-XXXX\" format");
            }
            for(int i = 0; i < userInput.Length; i++){
                if(userInput[3] != Convert.ToChar("-") || userInput[7] != Convert.ToChar("-")){
                    throw new Exception("The phone number was in the wrong format\nPlease enter a phone number in \"XXX-XXX-XXXX\" format");
                }
            }
        }

        public bool CheckIfEmpty(List<Customer> listOfCust){
            if(listOfCust.Any() == false){
                return true;
            }
            else{
                return false;
            }
            
        }

        public Customer UpdateCustomer(Customer c_cust){
            try{
                return _repo.UpdateCustomer(c_cust);
            }
            catch(System.Exception exe){
                throw new Exception(exe.Message);
            }
        } 
    }
}