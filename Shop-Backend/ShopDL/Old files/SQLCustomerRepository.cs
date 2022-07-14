// using System.Data.SqlClient;
// using ShopModel;

// namespace ShopDL
// {
//     public class SQLProfileRepository : IProfileRepository
//     {
//         private readonly string _connectionStrings;
//         public SQLProfileRepository(string p_connectionStrings)
//         {
//             _connectionStrings = p_connectionStrings;
//         }

//         public Profile AddProfile(Profile c_prof)
//         {
//             string sqlQuery = @"insert into Profile 
//                             values(@profName, @profAge, @profAddress, @profEmail,@profPhoneNumber)";
//             using (SqlConnection con = new SqlConnection(_connectionStrings))
//             {
//                 con.Open();
//                 SqlCommand command = new SqlCommand(sqlQuery, con);
//                 command.Parameters.AddWithValue("@profName", c_prof.Name);
//                 command.Parameters.AddWithValue("@profAge", c_prof.Age);
//                 command.Parameters.AddWithValue("@profAddress", c_prof.Address);
//                 command.Parameters.AddWithValue("@profEmail", c_prof.Email);
//                 command.Parameters.AddWithValue("@profPhoneNumber", c_prof.PhoneNumber);

//                 command.ExecuteNonQuery();
//             }

//             int cId = -1;
            
//             sqlQuery = @"SELECT top(1) * FROM Profile c
//                     ORDER BY c.profId DESC";
            
//             using (SqlConnection con = new SqlConnection(_connectionStrings))
//             {
//                 con.Open();
//                 //Create command object that has our sqlQuery and con object
//                 SqlCommand command = new SqlCommand(sqlQuery, con);
//                 SqlDataReader reader = command.ExecuteReader();
                
                
//                 while (reader.Read())
//                 {
//                     cId = reader.GetInt32(0);
//                 }
//             }
            
            
            
//             sqlQuery = @"insert into LoginAuthorityInfo 
//                     values(@username, @password, @authority, @profId)";


//             using (SqlConnection con = new SqlConnection(_connectionStrings))
//             {
//                 con.Open();
//                 SqlCommand command = new SqlCommand(sqlQuery, con);
                
//                 command.Parameters.AddWithValue("@username", c_prof.UserName );
//                 command.Parameters.AddWithValue("@password", c_prof.Password );
//                 command.Parameters.AddWithValue("@authority", c_prof.Authority );
//                 command.Parameters.AddWithValue("@profId", cId );
//                 command.ExecuteNonQuery();
//             }

                
            


//             return c_prof;
//         }   

        

    
//         public List<Profile> GetAllProfile()
//         {
//             List<Profile> listOfProfile = new List<Profile>();

//             string sqlQuery = @"select * from Profile c
//                             Inner Join LoginAuthorityInfo LAI ON c.profId = LAI.profId
//                             Order By c.profId ASC";
//             using (SqlConnection con = new SqlConnection(_connectionStrings))
//             {
//                 //Opens connection to the database
//                 con.Open();
//                 //Create command object that has our sqlQuery and con object
//                 SqlCommand command = new SqlCommand(sqlQuery, con);
//                 //SqlDataReader is a class specialized in reading outputs that came from a sql statement
//                 //Usually this outputs are in a form of a table and keep that in mind
//                 SqlDataReader reader = command.ExecuteReader();
//                 //Read() methods checks if you have more rows to go through
//                 //If there is another row = true, if not = false
//                 while (reader.Read())
//                 {
//                     listOfProfile.Add(new Profile(){
//                         //Zero-based column index
//                         profId      = reader.GetInt32(0),
//                         Name        = reader.GetString(1), 
//                         Age         = reader.GetInt32(2),
//                         Address     = reader.GetString(3),
//                         Email       = reader.GetString(4),
//                         PhoneNumber = reader.GetString(5),
//                         UserName    = reader.GetString(6),
//                         Password    = reader.GetString(7),
//                         Authority   = reader.GetInt32(8)
//                     });
//                 }
//             }

//             return listOfProfile;
//         }


//         /*public async Task<List<Profile>> GetAllProfileAsync()
//         {
//             List<Profile> listOfProfile = new List<Profile>();

//             string sqlQuery = @"select * from Profile";
//             using (SqlConnection con = new SqlConnection(_connectionStrings))
//             {
//                 await con.OpenAsync();
//                 SqlCommand command = new SqlCommand(sqlQuery, con);
//                 SqlDataReader reader = await command.ExecuteReaderAsync();

//                 while (reader.Read())
//                 {
//                     listOfProfile.Add(new Profile(){
//                         //Zero-based column index
//                         profId = reader.GetInt32(0),
//                         Name = reader.GetString(1), 
//                         Age = reader.GetInt32(2),
//                         Address = reader.GetString(3),
//                         Email = reader.GetString(4),
//                         PhoneNumber = reader.GetString(5)
//                     });
//                 }
//             }

//             return listOfProfile;
//         }
// */



//         public Profile UpdateProfile(Profile c_prof){
//             string sqlQuery = @"UPDATE Profile
//                         SET profName = @profNames, profAge = @profAges, profAddress = @profAddresss, profEmail = @profEmails, profPhoneNumber = @profPhoneNumbers
//                         WHERE profId = @profIds"; 
            
//             using (SqlConnection con = new SqlConnection(_connectionStrings))
//             {
//                 con.Open();
                
//                 SqlCommand command = new SqlCommand(sqlQuery, con);

//                 command.Parameters.AddWithValue("@profIds", c_prof.profId);
//                 command.Parameters.AddWithValue("@profNames", c_prof.Name);
//                 command.Parameters.AddWithValue("@profAges", c_prof.Age);
//                 command.Parameters.AddWithValue("@profAddresss", c_prof.Address);
//                 command.Parameters.AddWithValue("@profEmails", c_prof.Email);
//                 command.Parameters.AddWithValue("@profPhoneNumbers", c_prof.PhoneNumber);

//                 command.ExecuteNonQuery();
                
//             }
//             return c_prof; 
//         }

        

//     }
// }