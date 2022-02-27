using System.Data.SqlClient;
using ShopModel;

namespace ShopDL
{
    public class SQLCustomerRepository : ICustomerRepository
    {
        private readonly string _connectionStrings;
        public SQLCustomerRepository(string p_connectionStrings)
        {
            _connectionStrings = p_connectionStrings;
        }

        public Customer AddCustomer(Customer c_cust)
        {
            string sqlQuery = @"insert into Customer 
                            values(@custName, @custAge, @custAddress, @custEmail,@custPhoneNumber)";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@custName", c_cust.Name);
                command.Parameters.AddWithValue("@custAge", c_cust.Age);
                command.Parameters.AddWithValue("@custAddress", c_cust.Address);
                command.Parameters.AddWithValue("@custEmail", c_cust.Email);
                command.Parameters.AddWithValue("@custPhoneNumber", c_cust.PhoneNumber);

                command.ExecuteNonQuery();
            }

            int cId = -1;
            
            sqlQuery = @"SELECT top(1) * FROM Customer c
                    ORDER BY c.custId DESC";
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                //Create command object that has our sqlQuery and con object
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                
                
                while (reader.Read())
                {
                    cId = reader.GetInt32(0);
                }
            }
            
            
            
            sqlQuery = @"insert into LoginAuthorityInfo 
                    values(@username, @password, @authority, @custId)";


            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                
                command.Parameters.AddWithValue("@username", c_cust.UserName );
                command.Parameters.AddWithValue("@password", c_cust.Password );
                command.Parameters.AddWithValue("@authority", c_cust.Authority );
                command.Parameters.AddWithValue("@custId", cId );
                command.ExecuteNonQuery();
            }

                
            


            return c_cust;
        }   

        

    
        public List<Customer> GetAllCustomer()
        {
            List<Customer> listOfCustomer = new List<Customer>();

            string sqlQuery = @"select * from Customer c
                            Inner Join LoginAuthorityInfo LAI ON c.custId = LAI.custId
                            Order By c.custId ASC";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                //Opens connection to the database
                con.Open();
                //Create command object that has our sqlQuery and con object
                SqlCommand command = new SqlCommand(sqlQuery, con);
                //SqlDataReader is a class specialized in reading outputs that came from a sql statement
                //Usually this outputs are in a form of a table and keep that in mind
                SqlDataReader reader = command.ExecuteReader();
                //Read() methods checks if you have more rows to go through
                //If there is another row = true, if not = false
                while (reader.Read())
                {
                    listOfCustomer.Add(new Customer(){
                        //Zero-based column index
                        custId      = reader.GetInt32(0),
                        Name        = reader.GetString(1), 
                        Age         = reader.GetInt32(2),
                        Address     = reader.GetString(3),
                        Email       = reader.GetString(4),
                        PhoneNumber = reader.GetString(5),
                        UserName    = reader.GetString(6),
                        Password    = reader.GetString(7),
                        Authority   = reader.GetInt32(8)
                    });
                }
            }

            return listOfCustomer;
        }


        /*public async Task<List<Customer>> GetAllCustomerAsync()
        {
            List<Customer> listOfCustomer = new List<Customer>();

            string sqlQuery = @"select * from Customer";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                await con.OpenAsync();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    listOfCustomer.Add(new Customer(){
                        //Zero-based column index
                        custId = reader.GetInt32(0),
                        Name = reader.GetString(1), 
                        Age = reader.GetInt32(2),
                        Address = reader.GetString(3),
                        Email = reader.GetString(4),
                        PhoneNumber = reader.GetString(5)
                    });
                }
            }

            return listOfCustomer;
        }
*/



        public Customer UpdateCustomer(Customer c_cust){
            string sqlQuery = @"UPDATE Customer
                        SET custName = @custNames, custAge = @custAges, custAddress = @custAddresss, custEmail = @custEmails, custPhoneNumber = @custPhoneNumbers
                        WHERE custId = @custIds"; 
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                
                SqlCommand command = new SqlCommand(sqlQuery, con);

                command.Parameters.AddWithValue("@custIds", c_cust.custId);
                command.Parameters.AddWithValue("@custNames", c_cust.Name);
                command.Parameters.AddWithValue("@custAges", c_cust.Age);
                command.Parameters.AddWithValue("@custAddresss", c_cust.Address);
                command.Parameters.AddWithValue("@custEmails", c_cust.Email);
                command.Parameters.AddWithValue("@custPhoneNumbers", c_cust.PhoneNumber);

                command.ExecuteNonQuery();
                
            }
            return c_cust; 
        }

        

    }
}