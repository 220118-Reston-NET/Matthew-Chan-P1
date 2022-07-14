// using System.Data.SqlClient;

// namespace CRUDShopDL{
//     public class ProfileRepo:ICRUDRepository<Profile>
//     {
//         private readonly string _connectionStrings; 
//         public ProfileRepo(string s_connectionString){
//             _connectionStrings = s_connectionString;
//         }
//         public Profile Add(Profile c_prof){
//             string sqlQuery = @"insert into Profile 
//                             values(@profName, @profAge, @profAddress, @profEmail,@profPhoneNumber)";

//             //using block is different from our normal using statement
//             //It is used to automatically close any resource you stated inside of the parenthesis
//             //If an exception occurs, it will still automatically close any resources
//             using (SqlConnection con = new SqlConnection(_connectionStrings))
//             {
//                 //Opens the connection to the database
//                 con.Open();

//                 //SqlCommand class is a class specialized in executing SQL statements
//                 //Command will how the sqlQuery that will execute on the currently connection we have in the con object
//                 SqlCommand command = new SqlCommand(sqlQuery, con);
//                 //command.Parameters.AddWithValue("@profId", c_prof.profId);
//                 command.Parameters.AddWithValue("@profName", c_prof.Name);
//                 command.Parameters.AddWithValue("@profAge", c_prof.Age);
//                 command.Parameters.AddWithValue("@profAddress", c_prof.Address);
//                 command.Parameters.AddWithValue("@profEmail", c_prof.Email);
//                 command.Parameters.AddWithValue("@profPhoneNumber", c_prof.PhoneNumber);

//                 //Executes the SQL statement
//                 command.ExecuteNonQuery();
//             }

//             return c_prof;
//         }
//         public List<Profile> GetAll(){
//             List<Profile> listOfProfile = new List<Profile>();

//             string sqlQuery = @"select * from Profile";
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
        
//         public Profile Update(Profile p_profomer){
//             throw new NotImplementedException();
//         }
//         public Profile Delete(Profile p_profomer){
//             throw new NotImplementedException();
//         }

//     }
// }


