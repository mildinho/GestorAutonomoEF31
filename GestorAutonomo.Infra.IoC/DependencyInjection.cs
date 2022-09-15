using GestorAutonomo.Domain.Interfaces;
using GestorAutonomo.Infra.Data.Context;
using GestorAutonomo.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GestorAutonomo.Infra.IoC
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfraStructure(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddDbContext<DBContexto>(options =>
                    options.UseMySql(configuration.GetConnectionString("GestorAutonomoContext"), 
                    builder => builder.MigrationsAssembly("GestorAutonomo")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
