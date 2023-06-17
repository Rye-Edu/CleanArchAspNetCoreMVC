using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Northwind_App.Interfaces.Common;
using Northwind_App.Interfaces.IRepositories;
using Northwind_App.Interfaces.IServices.FileUploader;
using Northwind_App.ServicesHandler.CommonServices.Filters;
using Northwind_Infrastructure.Repositories;
using Northwind_Infrastructure.Services.Uploader;
using Northwind_Infrastructure.Data;

namespace Northwind_Infrastructure.InfraServiceRegistration
{

    public static class InfrastructureServicesRegistration
        {
            // public static c
            public static IServiceCollection AddInfraServiceRegistration(this IServiceCollection services, ConfigurationManager configuration)
            {


             //   services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
              //  services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
              //  services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
                services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(configuration.GetConnectionString("Northwind")));
                services.AddTransient(typeof(IAsyncBaseRepository<>), typeof(AsyncBaseRepository<>));
                services.AddTransient(typeof(IPaging<>),typeof( Paging<>));
                // builder.Services.AddScoped(typeof(IProductRepository<Product>), typeof(ProductRepository));
                services.AddTransient<IProductRepository, ProductRepository>();
                services.AddTransient<ICategoryRepository, CategoryRepository>();
                services.AddTransient<ISupplierRepository, SupplierRepository>();
                services.AddTransient<IUploaderService, Uploader_Service>();
                services.AddTransient<IPurchaseRequestRepository, PurchaseRequestRepository>();
                services.AddTransient<IStorePurchaseRepository, StorePurchaseRepository>();
             //   services.AddScoped<ISupplierRepository, SupplierRepository>();

                return services;
            }
        }
    
}
