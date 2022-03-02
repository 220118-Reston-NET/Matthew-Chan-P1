using System.Data.SqlClient;
using ShopModel;

namespace ShopDL
{
    
    
    
    public class SQLOrderRepository : IOrderRepository 
    {
        private readonly string _connectionStrings;
        public SQLOrderRepository(string p_connectionStrings)
        {
            _connectionStrings = p_connectionStrings;
        }


        /* 
        string sqlQuery = @"insert into Product";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {   
                //Opens the connection to the database
                con.Open();

                //SqlCommand class is a class specialized in executing SQL statements
                //Command will how the sqlQuery that will execute on the currently connection we have in the con object
                for(int i = 0; i < o_order.listOfOrders.Count; i++){

                    //should probably add somethign to check if product is already in product table
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    command.Parameters.AddWithValue("@prodName", o_order.lineItems[i].Products.Name);
                    command.Parameters.AddWithValue("@prodPrice", o_order.lineItems[i].Products.Price);
                    command.Parameters.AddWithValue("@prodDesc", o_order.lineItems[i].Products.Desc);
                    command.Parameters.AddWithValue("@prodAgeRestriction", o_order.lineItems[i].Products.Age_Restriction);
                }

                //Executes the SQL statement
                command.ExecuteNonQuery();
            }
        */    

        public Order AddOrder(Order o_order, int custId, int storeId)
        { 
            List<Order> OrderList = new List<Order>{};
            string sqlQuery = @"insert into LineItem
                            values(@prodId, @itemQuantity)";
            int numberOfItems = o_order.LineItems.Count;

            for(int i = 0; i < numberOfItems; i++){
                using (SqlConnection con = new SqlConnection(_connectionStrings))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, con);    
                    command.Parameters.AddWithValue("@prodId", o_order.LineItems[i].Products.prodId); //??????????????????
                    command.Parameters.AddWithValue("@itemQuantity", o_order.LineItems[i].Quantity);
                    command.ExecuteNonQuery();
                }   
            }



            List<int> IId = new List<int>{};
            sqlQuery = @"SELECT top(@numOfOrders) * FROM LineItem li
                    ORDER BY li.lineItemid DESC";
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@numOfOrders", numberOfItems);

                SqlDataReader reader = command.ExecuteReader();
                
                
                while (reader.Read())
                {
                    IId.Add(reader.GetInt32(0));
                }
            }
            
            
            
            sqlQuery = @"insert into Orders
                    values(@custId, @storeId, GETDATE())";

            for(int i = 0; i < numberOfItems; i++){
                using (SqlConnection con = new SqlConnection(_connectionStrings))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    command.Parameters.AddWithValue("@custId", custId);
                    command.Parameters.AddWithValue("@storeId", storeId);
                    command.ExecuteNonQuery();
                }

                
            }

            
            List<int> oId = new List<int>{};
            sqlQuery = @"SELECT top(@numOfOrders) * FROM Orders o ORDER BY o.orderId DESC";
            //probably need a way to read the line items to get the lineitemid
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                //Create command object that has our sqlQuery and con object
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@numOfOrders", numberOfItems);

                SqlDataReader reader = command.ExecuteReader();
                
                
                while (reader.Read())
                {
                    oId.Add(reader.GetInt32(0));
                }
            }



            
            sqlQuery = @"insert into OrderToLineItem
                    values(@orderId, @lineItemId)";

            for(int i = 0; i < numberOfItems; i++){
                using (SqlConnection con = new SqlConnection(_connectionStrings))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                
                    command.Parameters.AddWithValue("@orderId", oId[numberOfItems - i - 1]);
                    command.Parameters.AddWithValue("@lineItemId", IId[numberOfItems - i - 1]);
                    command.ExecuteNonQuery();
                }

                
            }


            // now take away from the inventory

            for(int i = 0; i < numberOfItems; i++){
                UseInventory(o_order.LineItems[i].Products.prodId, storeId, o_order.LineItems[i].Quantity);
            }   
            
            


            return o_order;
        } 




        public Order AddOrder()
        { 
            int custId = -1;
            string sqlQuery = @"SELECT * FROM ShoppingCart sc
                            INNER JOIN Product p ON p.prodId  = sc.prodId";
            List<int> listOfStoreId = new List<int>{};
            Order o_order = new Order();

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                
                List<LineItem> listOfLineItems = new List<LineItem>();
                List<string> listOfStoreFrontLocation = new List<string>();
                int tPrice = 0;

                while (reader.Read())
                {
                    custId = reader.GetInt32(0);
                    listOfLineItems.Add(
                        new LineItem(
                            -1,
                            new Product(
                                reader.GetInt32(4),
                                reader.GetString(5),
                                reader.GetInt32(6),
                                reader.GetString(7),
                                reader.GetInt32(8)
                            ),
                            reader.GetInt32(3)
                        )
                    );
                    listOfStoreFrontLocation.Add(StoreFrontIdToAddress(reader.GetInt32(1)));
                    listOfStoreId.Add(reader.GetInt32(1));
                    tPrice += reader.GetInt32(3) * reader.GetInt32(6);
                } 
                o_order = new Order(
                        -1,
                        listOfLineItems,
                        listOfStoreFrontLocation,
                        tPrice);
            } 
            







            ////////////////////////////////////////////////////////////////////////////////////
            /// 
            ///  
            /// 
            List<Order> OrderList = new List<Order>{};
            sqlQuery = @"insert into LineItem
                            values(@prodId, @itemQuantity)";
            int numberOfItems = o_order.LineItems.Count;

            for(int i = 0; i < numberOfItems; i++){
                using (SqlConnection con = new SqlConnection(_connectionStrings))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, con);    
                    command.Parameters.AddWithValue("@prodId", o_order.LineItems[i].Products.prodId); //??????????????????
                    command.Parameters.AddWithValue("@itemQuantity", o_order.LineItems[i].Quantity);
                    command.ExecuteNonQuery();
                }   
            }



            List<int> IId = new List<int>{};
            sqlQuery = @"SELECT top(@numOfOrders) * FROM LineItem li
                    ORDER BY li.lineItemid DESC";
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@numOfOrders", numberOfItems);

                SqlDataReader reader = command.ExecuteReader();
                
                
                while (reader.Read())
                {
                    IId.Add(reader.GetInt32(0));
                }
            }
            
            
            
            sqlQuery = @"insert into Orders
                    values(@custId, @storeId, GETDATE())";

            for(int i = 0; i < numberOfItems; i++){
                using (SqlConnection con = new SqlConnection(_connectionStrings))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    command.Parameters.AddWithValue("@custId", custId);
                    command.Parameters.AddWithValue("@storeId", listOfStoreId[i]);
                    command.ExecuteNonQuery();
                }

                
            }

            
            List<int> oId = new List<int>{};
            sqlQuery = @"SELECT top(@numOfOrders) * FROM Orders o ORDER BY o.orderId DESC";
            //probably need a way to read the line items to get the lineitemid
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                //Create command object that has our sqlQuery and con object
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@numOfOrders", numberOfItems);

                SqlDataReader reader = command.ExecuteReader();
                
                
                while (reader.Read())
                {
                    oId.Add(reader.GetInt32(0));
                }
            }



            
            sqlQuery = @"insert into OrderToLineItem
                    values(@orderId, @lineItemId)";

            for(int i = 0; i < numberOfItems; i++){
                using (SqlConnection con = new SqlConnection(_connectionStrings))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                
                    command.Parameters.AddWithValue("@orderId", oId[numberOfItems - i - 1]);
                    command.Parameters.AddWithValue("@lineItemId", IId[numberOfItems - i - 1]);
                    command.ExecuteNonQuery();
                }

                
            }


            // now take away from the inventory

            for(int i = 0; i < numberOfItems; i++){
                UseInventory(o_order.LineItems[i].Products.prodId, listOfStoreId[i], o_order.LineItems[i].Quantity);
            }
            //ClearCart();
            return o_order;
        } 
/*
        public Order UpdateOrder(Order o_order)
        { 
            string sqlQuery = @"Update LineItem 
                            SET prodId = @prodIds, itemQuantity = @itemQuantitys
                            Where lineId = @lineIds";
            int numberOfItems = o_order.LineItems.Count;

            for(int i = 0; i < numberOfItems; i++){
                using (SqlConnection con = new SqlConnection(_connectionStrings))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, con);    
                    command.Parameters.AddWithValue("@prodIds", o_order.LineItems[i].Products.prodId); //??????????????????
                    command.Parameters.AddWithValue("@itemQuantitys", o_order.LineItems[i].Quantity);
                    command.ExecuteNonQuery();
                } 
            } 

            return o_order;
        }
*/

  
        public List<Order> GetAllOrder()    
        {   
            List<Order> listOfOrder = new List<Order>();    

            string sqlQuery = @"SELECT o.orderId, li.lineItemId, p.prodId, p.prodName, p.prodPrice, p.prodDesc, p.prodAgeRestriction,  li.itemQuantity, sf.storeAddress, o.creationTime
                            FROM Customer c     
                            INNER JOIN Orders o ON c.custId = o.custId 
                            INNER JOIN OrderToLineItem ol ON o.orderId = ol.orderId 
                            INNER JOIN LineItem li on ol.lineItemId = li.lineItemId 
                            INNER JOIN Product p ON p.prodId  = li.prodId  
                            INNER JOIN StoreFront sf ON o.storeId = sf.storeId";
//                            where c.custId = @custId";
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

                int oId = -1;
                List<LineItem> listOfLineItems = new List<LineItem>();
                List<string> listOfStoreFrontLocation = new List<string>();
                int tPrice = 0;
                DateTime creationTime = DateTime.Now;
                while (reader.Read())
                {
                    if(oId != -1 && oId != reader.GetInt32(0)){
                        listOfOrder.Add(
                            new Order(
                            oId,
                            listOfLineItems,
                            listOfStoreFrontLocation,
                            tPrice,
                            creationTime
                        ));
                        listOfLineItems.Clear();
                        listOfStoreFrontLocation.Clear();
                        tPrice = 0;
                    }
                    oId = reader.GetInt32(0);
                    listOfLineItems.Add(
                        new LineItem(
                            reader.GetInt32(1),
                            new Product(
                                reader.GetInt32(2),
                                reader.GetString(3),
                                reader.GetInt32(4),
                                reader.GetString(5),
                                reader.GetInt32(6)
                            ),
                            reader.GetInt32(7)
                        )
                    );
                    listOfStoreFrontLocation.Add(reader.GetString(8));
                    tPrice += reader.GetInt32(4);
                    creationTime = reader.GetDateTime(9);
                }
            } 

            return listOfOrder;
        }

        public List<Order> GetACustomerOrder( int cId)    
        {   
            List<Order> listOfOrder = new List<Order>();    

            string sqlQuery = @"SELECT o.orderId, li.lineItemId, p.prodId, p.prodName, p.prodPrice, p.prodDesc, p.prodAgeRestriction,  li.itemQuantity, sf.storeAddress, o.creationTime
                            FROM Customer c     
                            INNER JOIN Orders o ON c.custId = o.custId 
                            INNER JOIN OrderToLineItem ol ON o.orderId = ol.orderId 
                            INNER JOIN LineItem li on ol.lineItemId = li.lineItemId 
                            INNER JOIN Product p ON p.prodId  = li.prodId  
                            INNER JOIN StoreFront sf ON o.storeId = sf.storeId
                            where c.custId = @custId
                            ORDER BY cast(o.creationTime as date) ASC, (p.prodPrice * li.itemQuantity) DESC";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                //Opens connection to the database
                con.Open();
                //Create command object that has our sqlQuery and con object
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@custId", cId);
                //SqlDataReader is a class specialized in reading outputs that came from a sql statement
                //Usually this outputs are in a form of a table and keep that in mind
                SqlDataReader reader = command.ExecuteReader();
                //Read() methods checks if you have more rows to go through
                //If there is another row = true, if not = false

                int oId = -1;
                List<LineItem> listOfLineItems = new List<LineItem>();
                List<string> listOfStoreFrontLocation = new List<string>();
                int tPrice = 0;
                DateTime creationTime = DateTime.Now;
                while (reader.Read())
                {
                    oId = reader.GetInt32(0);
                    listOfLineItems.Add(
                        new LineItem(
                            reader.GetInt32(1),
                            new Product(
                                reader.GetInt32(2),
                                reader.GetString(3),
                                reader.GetInt32(4),
                                reader.GetString(5),
                                reader.GetInt32(6)
                            ),
                            reader.GetInt32(7)
                        )
                    );
                    listOfStoreFrontLocation.Add(reader.GetString(8));
                    tPrice += reader.GetInt32(4);
                    creationTime = reader.GetDateTime(9);
                    listOfOrder.Add(
                        new Order(
                        oId,
                        listOfLineItems,
                        listOfStoreFrontLocation,
                        tPrice,
                        creationTime
                    ));
                    


                    
                }
            } 

            return listOfOrder;
        }


        public List<Order> GetAShopOrder( int sId)    
        {   
            List<Order> listOfOrder = new List<Order>();    

            string sqlQuery = @"SELECT o.orderId, li.lineItemId, p.prodId, p.prodName, p.prodPrice, p.prodDesc, p.prodAgeRestriction,  li.itemQuantity, sf.storeAddress
                            FROM Customer c     
                            INNER JOIN Orders o ON c.custId = o.custId 
                            INNER JOIN OrderToLineItem ol ON o.orderId = ol.orderId 
                            INNER JOIN LineItem li on ol.lineItemId = li.lineItemId 
                            INNER JOIN Product p ON p.prodId  = li.prodId  
                            INNER JOIN StoreFront sf ON o.storeId = sf.storeId
                            where sf.storeId = @shopId
                            ORDER BY cast(o.creationTime as date) ASC, (p.prodPrice * li.itemQuantity) DESC";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                //Opens connection to the database
                con.Open();
                //Create command object that has our sqlQuery and con object
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@shopId", sId);
                //SqlDataReader is a class specialized in reading outputs that came from a sql statement
                //Usually this outputs are in a form of a table and keep that in mind
                SqlDataReader reader = command.ExecuteReader();
                //Read() methods checks if you have more rows to go through
                //If there is another row = true, if not = false

                int shopId = -1;
                List<LineItem> listOfLineItems = new List<LineItem>();
                List<string> listOfStoreFrontLocation = new List<string>();
                int tPrice = 0;

                while (reader.Read())
                {
                    shopId = reader.GetInt32(0);
                    listOfLineItems.Add(
                        new LineItem(
                            reader.GetInt32(1),
                            new Product(
                                reader.GetInt32(2),
                                reader.GetString(3),
                                reader.GetInt32(4),
                                reader.GetString(5),
                                reader.GetInt32(6)
                            ),
                            reader.GetInt32(7)
                        )
                    );
                    listOfStoreFrontLocation.Add(reader.GetString(8));
                    tPrice += reader.GetInt32(4);

                    listOfOrder.Add(
                        new Order(
                        shopId,
                        listOfLineItems,
                        listOfStoreFrontLocation,
                        tPrice
                    ));
                    
                    
                }
            } 

            return listOfOrder;
        }

        public Product ProductIdToProduct(int prodId){
            string sqlQuery = @"select * from Product p
                            WHERE p.prodId = @productId";
            Product prod = new Product();
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@productId", prodId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    prod = new Product(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4)); 
                }   
            }
            return prod;
        }

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

        public string StoreFrontIdToAddress(int sId){
            string sqlQuery = @"select sf.storeAddress from StoreFront sf
                            WHERE sf.storeId = @storefrontId";
            string storeFrontAddress = "N/A";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@storefrontId", sId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    storeFrontAddress = reader.GetString(0); 
                }   
            }
            return storeFrontAddress;
            
        }



        public Inventory UseInventory(int p_prodId, int s_storeId, int amount)
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
            SET prodQuantitiy = @currentInv - @amnt
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

        public List<Product> GetAllProducts(){
            List<Product> listOfProducts = new List<Product>();

            string sqlQuery = @"select * from Inventory i
                                Inner Join Product p ON i.prodId = p.prodId";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listOfProducts.Add(new Product(reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8))); 
                }   
            }
            return listOfProducts;
        }

        public List<StoreFront> GetAllStoreFront()
        {
            List<StoreFront> listOfStoreFront = new List<StoreFront>();

            string sqlQuery = @"select * from StoreFront";
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












        public Order AddCart(Order o_order, int custId, int storeId)
        { 
            string sqlQuery = @"insert into ShoppingCart
                            values(@custId, @storeId, @prodId, @quantity)";
            int numberOfItems = o_order.LineItems.Count;

            for(int i = 0; i < numberOfItems; i++){
                using (SqlConnection con = new SqlConnection(_connectionStrings))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, con);    
                    command.Parameters.AddWithValue("@custId", custId);
                    command.Parameters.AddWithValue("@storeId", storeId);
                    command.Parameters.AddWithValue("@prodId", o_order.LineItems[i].Products.prodId); //??????????????????
                    command.Parameters.AddWithValue("@quantity", o_order.LineItems[i].Quantity);
                    command.ExecuteNonQuery();
                }   
            }

            return o_order;
        }

        public Order GetAllCart()    
        {   
            Order ord = new Order();    

            string sqlQuery = @"SELECT * FROM ShoppingCart sc
                            INNER JOIN Product p ON p.prodId  = sc.prodId";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                
                int oId = -1;
                List<LineItem> listOfLineItems = new List<LineItem>();
                List<string> listOfStoreFrontLocation = new List<string>();
                int tPrice = 0;

                while (reader.Read())
                {
                    listOfLineItems.Add(
                        new LineItem(
                            -1,
                            new Product(
                                reader.GetInt32(4),
                                reader.GetString(5),
                                reader.GetInt32(6),
                                reader.GetString(7),
                                reader.GetInt32(8)
                            ),
                            reader.GetInt32(3)
                        )
                    );
                    listOfStoreFrontLocation.Add(StoreFrontIdToAddress(reader.GetInt32(2)));
                    tPrice += reader.GetInt32(3) * reader.GetInt32(6);
                } 
                ord = new Order(
                        oId,
                        listOfLineItems,
                        listOfStoreFrontLocation,
                        tPrice);
            } 
            
            return ord;
        }
        
        
        public void ClearCart()
        {
            string sqlQuery = @"DELETE FROM ShoppingCart";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.ExecuteNonQuery();
            }
        }

    } 
}