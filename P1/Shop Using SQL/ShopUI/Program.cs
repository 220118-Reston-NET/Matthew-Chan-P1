//dotnet new console  -o ShopUI
global using Serilog;
using ShopDL;
using ShopUI;
using ShopBL;
using Microsoft.Extensions.Configuration;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("./logs/user.txt") 
    .CreateLogger();


//Reading and obtaining connectionString fom asppsetings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string _connectionString = configuration.GetConnectionString("Reference2DB");

bool repeat = true;
IMenu menu = new MainMenu();

while(repeat){
    Console.Clear();
    menu.Display();
    string ans = menu.UserChoice();

    
    switch(ans){ 
        case "PlaceOrder":
            Log.Information("Placing an Order"); 
            menu = new OrderMenu(new OrderBL(new SQLOrderRepository(_connectionString)));
            break;
        case "Restock":
            Log.Information("Replenishing the Inventory");
            menu = new RestockMenu(new StoreFrontBL(new SQLStoreFrontRepository(_connectionString)));
            break; 
        case "SearchInventory":
            Log.Information("Searching the inventory");
            menu = new SearchInventoryMenu(new StoreFrontBL(new SQLStoreFrontRepository(_connectionString)));
            break;
        case "SearchStoreFront":
            Log.Information("Adding stores");
            menu = new SearchStoreFrontMenu(new StoreFrontBL(new SQLStoreFrontRepository(_connectionString)));
            break;
        case "AddStore":
            Log.Information("Adding stores");
            menu = new AddStoreFrontMenu(new StoreFrontBL(new SQLStoreFrontRepository(_connectionString)));
            break;
        case "SearchCustomer":
            Log.Information("Displaying SearchCustomer Menu to user");
            menu = new SearchCustomerMenu(new CustomerBL(new SQLCustomerRepository(_connectionString)));
            break; 
        case "AddCustomer":
            menu = new AddCustomerMenu(new CustomerBL(new SQLCustomerRepository(_connectionString)));
            break;
        case "MainMenu":
            Log.Information("Displaying MainMenu to user");
            menu = new MainMenu();
            break;
        case "Exit":
            Log.Information("Exiting application");
            Log.CloseAndFlush(); //To close our logger resource
            repeat = false;
            break;
        default:
            Console.WriteLine("Error, this page does not exist");
            Console.WriteLine("Please press enter to continue");
            Console.ReadLine();
            break;
        
    }
      
    
}
