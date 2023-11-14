using Inventory.Web.Consumer;
using Inventory.Web.Data;
using Inventory.Web.Interfaces.DomainServices;
using Inventory.Web.Interfaces.Repositories;
using Inventory.Web.Producer;
using Inventory.Web.Service;
using Microsoft.EntityFrameworkCore;

const string policyName = "AllowOrigin";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

//DBContext
builder.Services.AddDbContext<InventoryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"));
});

// build services


// Domain Services
builder.Services.AddScoped<IInventoryService, InventoryService>();

// Background Services

// build kafka producer
builder.Services.AddSingleton<RequestSupplies>();

//kafka consumer
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddHostedService<InventoryUpdateConsumer>();

// Repositories
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(policyName);
app.UseAuthorization();

app.MapControllers();

app.Run();