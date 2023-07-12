using EnigmaShopApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>)); //-> Kalau datanya generic
builder.Services.AddTransient<IPersistance, DbPersistence>();


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
