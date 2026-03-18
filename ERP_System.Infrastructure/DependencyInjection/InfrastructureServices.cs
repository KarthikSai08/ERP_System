using ERP_System.Domain.Interfaces;
using ERP_System.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace ERP_System.Infrastructure.DependencyInjection
{
    public static class InfrastructureServices
    {

        public static IServiceCollection AddInfrastucture(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlServer(config.GetConnectionString("DefaultConnection")));


            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
