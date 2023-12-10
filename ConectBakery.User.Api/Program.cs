using ConnectBakery.DAL;
using ConnectBakery.Domain.Entities;
using ConnectBakery.Core.Extension;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddConnectBakeryDbContext(builder.Configuration);

// Add service
builder.Services.AddService();

// add identity services
builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<ConnectBakeryDbContext>();

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

app.MapGroup("/api/account").MapIdentityApi<User>();

app.Run();
