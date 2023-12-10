using ConnectBakery.Application.Services;
using ConnectBakery.Application.Shared.Interfaces;
using ConnectBakery.DAL.Mapping;
using ConnectBakery.DAL;
using Microsoft.EntityFrameworkCore;

namespace ConectBakery.Stock.Api.ExtensionServices
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
            service.AddScoped<IStockService, StockService>();
           

            return service;
        }


    }

}
