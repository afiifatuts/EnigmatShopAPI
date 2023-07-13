using EnigmaShopApi.Repositories;
using Microsoft.EntityFrameworkCore;
using EnigmaShopApi.Services;
using EnigmaShopApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);
//configure Logging
builder.Host.ConfigureLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    //loggingBuilder.ClearProviders();
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//appdbcontext
//repo 
//persistence
builder.Services.AddDbContext<AppDbContext>
                (opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

/*
lifetime dependency injection ada 3:
singleton -> object dibuat cuma 1 kali dan dipakai selama aplikasi itu hidup 
scoped -> hidup selama ada yang request contohnya database
transient -> hidup selama ada request contoh ketika hit API
*/
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); //-> Kalau datanya generic
builder.Services.AddScoped<IPersistance, DbPersistence>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddTransient<HandleExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<HandleExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// app.Use(async (context, next) =>
// {
//     await context.Response.WriteAsync("text middleware 1");
//     await next();
// });

app.Run();
