
/*

using System.Data.SqlClient;

namespace ShopDL{
    public class InventoryRepository:ICRUDRepository<Inventory>
    {
        private readonly string _connectionStrings; 
        public InventoryRepository(string i_connectionString){
            _connectionStrings = i_connectionString;
        }



        public Inventory Add(Inventory i_inventory){  ///////// ??????????????????
            string sqlQuery = @"insert into inventory 
                                values(@prodId, @prodQuantity, @storeId)";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                    for(int i = 0; i < i_inventory.Products.Count; i++){ // loops though all the incoming inventory?
                        command.Parameters.AddWithValue("@prodId", i_inventory.Products[i]); // i think this should work????
                        command.Parameters.AddWithValue("@prodQuantity", i_inventory.quantity[i]);
                        command.Parameters.AddWithValue("@storeId", i_inventory.storeId);
                    }
                command.ExecuteNonQuery();
            }


            
            return i_inventory;

        }
        public List<Inventory> GetAll(){ 
            List<Inventory> listOfInventory = new List<Inventory>();

            string sqlQuery = @"select * from Inventory 
                                Inner Join Product p ON i.prodId = p.prodId";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                //Opens connection to the database
            con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                
                int sId = -1;
                List<Product> productList = new List<Product>{};
                List<int> productQuantity = new List<int>{};

                int prodQuantity = 0; // the get all product

                while (reader.Read())
                {
                    if(sId != -1 && sId != reader.GetInt32(3)){
                        listOfInventory.Add(new Inventory(productList,productQuantity,sId));
                        productList.Clear();
                        productQuantity.Clear();
                    }
                    productList.Add(new Product(reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8)));
                    productQuantity.Add(reader.GetInt32(2));
                    sId = reader.GetInt32(3);
                }
            }
            return listOfInventory;
        }
        public Inventory Update(Inventory i_inventory){

            string sqlQuery = @"UPDATE Inventory
                SET prodQuantitiy = @newQuant
                WHERE inventory.storeId = @s_sId
                AND inventory.prodId = @p_ProdId"; 
                
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                
                con.Open();
                
                SqlCommand command = new SqlCommand(sqlQuery, con);
                for(int i = 0; i < i_inventory.Products.Count; i++)
                {
                    command.Parameters.AddWithValue("@newQuant", i_inventory.quantity[i]);
                    command.Parameters.AddWithValue("@s_sId", i_inventory.storeId);
                    command.Parameters.AddWithValue("@p_ProdId", i_inventory.Products[i].prodId);
                }
                SqlDataReader reader = command.ExecuteReader();
                    
            } 
            return i_inventory; 
        } 
        public Inventory Delete(Inventory i_resource){
            string sqlQuery = @"delete from Inventory
                            where inventoryId = @Id";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand com = new SqlCommand(sqlQuery, con);
                
                com.Parameters.AddWithValue("@Id",i_resource.)
            }
        }
    }
}




/* 
        public Inventory GetAnInventory(int s_storeId)
        {
            Inventory inven = new Inventory();

            string sqlQuery = @"select * from Inventory i
                                Inner Join Product p ON i.prodId = p.prodId
                                where i.storeId = @storeId"; 
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                //Opens connection to the database
                con.Open();
                //Create command object that has our sqlQuery and con object
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@storeId", s_storeId);
                //SqlDataReader is a class specialized in reading outputs that came from a sql statement
                //Usually this outputs are in a form of a table and keep that in mind
                SqlDataReader reader = command.ExecuteReader();
                //Read() methods checks if you have more rows to go through
                //If there is another row = true, if not = false
                
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

        public Inventory RestockInventory(int p_prodId, int s_storeId, int amount)
        {
            string sqlQuery = @"select prodQuantitiy from Inventory
            WHERE inventory.storeId = @s_sId
            AND inventory.prodId = @p_ProdId";

            int currentInv = 0;
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@s_sId", s_storeId);
                command.Parameters.AddWithValue("@p_ProdId", p_prodId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currentInv = reader.GetInt32(0); 
                }
                
            } 



            sqlQuery = @"UPDATE Inventory
            SET prodQuantitiy = @amnt + @currentInv
            WHERE inventory.storeId = @s_sId
            AND inventory.prodId = @p_ProdId"; 
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                //Opens connection to the database
                con.Open();
                //Create command object that has our sqlQuery and con object
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@amnt", amount);
                command.Parameters.AddWithValue("@s_sId", s_storeId);
                command.Parameters.AddWithValue("@p_ProdId", p_prodId);
                command.Parameters.AddWithValue("@currentInv", currentInv);
                //SqlDataReader is a class specialized in reading outputs that came from a sql statement
                //Usually this outputs are in a form of a table and keep that in mind
                SqlDataReader reader = command.ExecuteReader();
                //Read() methods checks if you have more rows to go through
                //If there is another row = true, if not = false
                
            } 
            Inventory inven = GetAnInventory(s_storeId);
            return inven;
        }

        public Inventory AddItemToInventory(int p_prodId, int s_storeId, int amount)
        {
            string sqlQuery = @"insert into inventory 
                            values(@prodId, @prodQuantity, @storeId)";

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
                //command.Parameters.AddWithValue("@custId", c_cust.custId);
                command.Parameters.AddWithValue("@prodId", p_prodId);
                command.Parameters.AddWithValue("@prodQuantity", amount);
                command.Parameters.AddWithValue("@storeId", s_storeId);

                //Executes the SQL statement
                command.ExecuteNonQuery();
            }






            Inventory inven = new Inventory();
            int IId;
            sqlQuery = @"SELECT i.inventoryId  FROM Inventory i 
                        ORDER BY i.inventoryId  DESC 
                        OFFSET 0 ROWS FETCH FIRST 1 ROW ONLY";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                //Create command object that has our sqlQuery and con object
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                
                
                while (reader.Read())
                {
                    IId = reader.GetInt32(0);
                }
            } 

            return GetAnInventory(s_storeId);


        }

        public List<Product> GetAllProducts(){
            List<Product> listOfProducts = new List<Product>();

            string sqlQuery = @"select * from Product";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listOfProducts.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4))); 
                }   
            }
            return listOfProducts;
        }



        public List<Customer> GetAllCustomer()
        {
            List<Customer> listOfCustomer = new List<Customer>();

            string sqlQuery = @"select * from Customer";
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

    }
} */