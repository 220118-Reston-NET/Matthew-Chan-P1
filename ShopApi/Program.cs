global using Serilog;
using ShopBL;
using ShopDL;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("./logs/user.txt") //We configure our logger to save in this file
    .CreateLogger();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomerRepository>(repo => new SQLCustomerRepository(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<ICustomerBL,CustomerBL>();

builder.Services.AddScoped<IStoreFrontRepository>(repo => new SQLStoreFrontRepository(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IStoreFrontBL,StoreFrontBL>();
builder.Services.AddScoped<ICustomerBL,CustomerBL>();

builder.Services.AddScoped<IProductRepository>(repo => new SQLProductRepository(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IProductBL,ProductBL>();

builder.Services.AddScoped<IOrderRepository>(repo => new SQLOrderRepository(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IOrderBL,OrderBL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
