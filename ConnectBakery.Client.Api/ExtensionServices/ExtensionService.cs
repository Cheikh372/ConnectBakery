using ConnectBakery.Application.Services;
using ConnectBakery.Application.Shared.Interfaces;
using ConnectBakery.DAL.Mapping;
using ConnectBakery.DAL;
using Microsoft.EntityFrameworkCore;

namespace ConectBakery.Client.Api.ExtensionServices
{
    public static class ExtensionService
    {
        //public static IServiceCollection AddConnectBakeryDbContext(this IServiceCollection service, IConfiguration configuration)
        //{
        //    service.AddDbContext<ConnectBakeryDbContext>(options =>
        //    {
        //        options.UseNpgsql(configuration.GetConnectionString("ConnectBakeryContext"));
        //    });

        //    //Service autoMapper
        //    service.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        //    // Repository
        //    service.AddAutoMapper(typeof(ServiceMapping));

        //    //AppContext.SetSwitch("Npgsql.EnableLagacyTimesTampBehavior", true);

        //    return service;
        //}


        public static IServiceCollection AddService(this IServiceCollection service)
        {
            // Services
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<IClientService, ClientService>();
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<IOrderItemService, OrderItemService>();
            service.AddScoped<IEmployeService, EmployeService>();
            service.AddScoped<IStockService, StockService>();
            service.AddScoped<IStatistiqueService, StatistiqueService>();
            service.AddScoped<IUserService, UserService>();

            return service;
        }


    }

}
