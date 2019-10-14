using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using project_Andrey_Rudenko.Data;
using project_Andrey_Rudenko.Model;
using project_Andrey_Rudenko.Model.Enum;
using project_Andrey_Rudenko.Model.Interfaces;

namespace project_Andrey_Rudenko
{
    class Program
    {
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            //services.AddTransient<ITestService, TestService>();
            // IMPORTANT! Register our application entry point
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestProjectAndreyRudenko;Trusted_Connection=True;MultipleActiveResultSets=true");
            });
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddTransient<Market>();
            return services;
        }

        static void Main(string[] args)
        {
            // Create service collection and configure our services
            var services = ConfigureServices();
            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            // Kick off our actual code
            serviceProvider.GetService<Market>().AddItem(new Item(10, "Burger", "Good Food", ItemCategory.Food, 12));
            serviceProvider.GetService<Market>().AddItem(new Item(112, "Cola", "Good Drink", ItemCategory.Drink, 100));
            serviceProvider.GetService<Market>().Start();


        }
    }
}
