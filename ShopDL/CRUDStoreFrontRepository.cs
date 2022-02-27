/*using System.Data.SqlClient;
using ShopModel;

public class StoreFrontRepository:ICRUDRepository<StoreFront>
{
    private readonly string _connectionStrings; 
    public StoreFrontRepository(string s_connectionString){
        _connectionStrings = s_connectionString;
    }



    public StoreFront Add(StoreFront s_store){
        string sqlQuery = @"insert into StoreFront 
                            values(@storeName, @storeAddress)";

        //using block is different from our normal using statement
        //It is used to automatically close any resource you stated inside of the parenthesis
        //If an exception occurs, it will still automatically close any resources
        using (SqlConnection con = new SqlConnection(_connectionStrings))
        {
            //Opens the connection to the database
            con.Open();
            //SqlCommand class is a class specialized in executing SQL statements
            //Command will how the sqlQuery that will execute on the currently connection we have in the con object
            SqlCommand command = new SqlCommand(sqlQuery, con);
            command.Parameters.AddWithValue("@storeName", s_store.Name);
            command.Parameters.AddWithValue("@storeAddress", s_store.Address);
 
            //Executes the SQL statement
            command.ExecuteNonQuery();
        }

        return s_store;
    
    }
    public List<StoreFront> GetAll(){
        List<StoreFront> listOfStoreFront = new List<StoreFront>();

        string sqlQuery = @"select * from StoreFront";
        using (SqlConnection con = new SqlConnection(_connectionStrings))
        {
            con.Open();
            SqlCommand command = new SqlCommand(sqlQuery, con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                listOfStoreFront.Add(new StoreFront(){
                    //Zero-based column index
                    storeId = reader.GetInt32(0),
                    Name = reader.GetString(1), 
                    Address = reader.GetString(2),
                    Inv = GetAnInventory(reader.GetInt32(0))
                });
            }
        } 

        return listOfStoreFront;
    }
    
    public Product Update(Product p_product){
        throw new NotImplementedException();
    }
    public Product Delete(Product p_product){
        throw new NotImplementedException();
    }






    public Inventory GetAnInventory(int s_storeId)
        {
            Inventory inven = new Inventory();

            string sqlQuery = @"select * from Inventory i
                                Inner Join Product p ON i.prodId = p.prodId
                                where i.storeId = @storeId"; 
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@storeId", s_storeId);
                SqlDataReader reader = command.ExecuteReader();
                
                int sId = 0;
                List<Product> productList = new List<Product>{};
                List<int> productQuantity = new List<int>{};

                while (reader.Read())
                {
                    productList.Add(new Product(reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8)));
                    productQuantity.Add(reader.GetInt32(2));
                    sId = reader.GetInt32(3);
                }
                inven = new Inventory(productList,productQuantity, sId);
            } 
            
            return inven;
        }

}




*/