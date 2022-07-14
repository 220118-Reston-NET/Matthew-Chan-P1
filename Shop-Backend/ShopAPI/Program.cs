global using Serilog;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shop.ShopAPI.AuthenticationService.Implements;
using Shop.ShopAPI.AuthenticationService.Interfaces;
using Shop.ShopAPI.AuthenticationService.Middlewares;
using Shop.BuisnessManagement.Implements;
using Shop.BuisnessManagement.Interfaces;
using Shop.DatabaseManagement.Implements;
using Shop.DatabaseManagement.Interfaces;
using Shop.Models;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("./logs/user.txt") //We configure our logger to save in this file
    .CreateLogger();

//"Reference2DB" : "Server=tcp:pokemondb.database.windows.net,1433;Initial Catalog=PokeDB;Persist Security Info=False;User ID=guiltykingouma;Password=Naruto7878!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

// Add services to the container.
var key = builder.Configuration["Token:Key"];
var connectionString = builder.Configuration.GetConnectionString("Reference2DB");

//builder.Services.AddSingleton<PresenceTracker>();
builder.Services.AddSingleton<IAccessTokenManager, AccessTokenManager>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();

//Identity Role
builder.Services.AddIdentity<ApplicationUser, IdentityRole>( x =>
{
    x.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ShopContext>();

//Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer( x=>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };

    /*
    x.Events = new JwtBearerEvents
    {
        OnMessagesReceived = context =>
        {
            var access Token = context.Request.Query["access_token"];

            var path = context.HttpContext.Request.Path;
            if(!string.IsNullOrempty(accessToken) &&
                path.StartWithSegments("/hubs"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    } */

});

builder.Services.AddCors();
//builder.Services.AddSignalR();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ShopContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IProfileManagementBL, ProfileManagementBL>();
builder.Services.AddScoped<IProfileManagementDL, ProfileManagementDL>();

builder.Services.AddScoped<IStoreManagementBL, StoreManagementBL>();
builder.Services.AddScoped<IStoreManagementDL, StoreManagementDL>();

builder.Services.AddScoped<IPurchaseManagementBL, PurchaseManagementBL>();
builder.Services.AddScoped<IPurchaseManagementDL, PurchaseManagementDL>();
/*
builder.Services.AddScoped<IProfileManagementDL>(repo => new ProfileManagementDL(new ShopContext(connectionString)));
builder.Services.AddScoped<IProfileManagementBL, ProfileManagementBL>(); */

//builder.Services.AddScoped<Irepository, EfRepository>();

// builder.Services.AddScoped<IProfileRepository>(repo => new SQLProfileRepository(builder.Configuration.GetConnectionString("Reference2DB")));
// builder.Services.AddScoped<IProfileBL,ProfileBL>();

// builder.Services.AddScoped<IStoreFrontRepository>(repo => new SQLStoreFrontRepository(builder.Configuration.GetConnectionString("Reference2DB")));
// builder.Services.AddScoped<IStoreFrontBL,StoreFrontBL>();
// builder.Services.AddScoped<IProfileBL,ProfileBL>();

// builder.Services.AddScoped<IProductRepository>(repo => new SQLProductRepository(builder.Configuration.GetConnectionString("Reference2DB")));
// builder.Services.AddScoped<IProductBL,ProductBL>();

// builder.Services.AddScoped<IOrderRepository>(repo => new SQLOrderRepository(builder.Configuration.GetConnectionString("Reference2DB")));
// builder.Services.AddScoped<IOrderBL,OrderBL>();

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Apply middleware
app.UseTokenManagerMiddleware();
//app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
