using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.AspNetCore.Authentication;
using Northwind_App.Mappings;

namespace Northwind_App.ServiceRegistration
{
    public static class AppServiceRegistration
    {
        public static IServiceCollection AddAppRegistration(this IServiceCollection services)
        {

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());

            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
           // services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
