using Northwind_App.ServiceRegistration;
using Northwind_Infrastructure.InfraServiceRegistration;

namespace NorthwindMVC_UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //            var connectionString = builder.Configuration.GetConnectionString("UserContextConnection") ?? throw new InvalidOperationException("Connection string 'UserContextConnection' not found.");

            //            builder.Services.AddDbContext<UserContext>(options =>
            //options.UseSqlServer(connectionString));

            //            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //.AddEntityFrameworkStores<UserContext>();
            // Add services to the container.



            builder.Services.AddAppRegistration();
            builder.Services.AddInfraServiceRegistration(builder.Configuration);
            builder.Services.AddControllersWithViews();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.MapControllerRoute(
            //   name: "ProductList",
            //   pattern: "{controller=ProductCatalog}/{action=Products}/list/current-page/{page:int?}");

            //app.MapControllerRoute(
            //    name: "ProductSearch",
            //    pattern: "{controller=ProductCatalog}/{action=Products}/product-search/{productViewModel?}"
            //    );
            //defaults: new { controller = "ProductCatalog", action = "Products" });


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}