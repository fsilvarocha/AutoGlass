
using AutoGlass.Core.Application.Abstractions;
using AutoGlass.Core.Application.Services;
using AutoGlass.Core.Domain.Interfaces;
using AutoGlass.Infra.Data.DataContext;
using AutoGlass.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AutoGlass.Infra.IoC.DataConfig;

public static class DataConfig
{
    public static IServiceCollection AddDataConfig(this IServiceCollection services, IConfiguration configuration)
    {

        services
               .AddDbContextPool<ApplicationDbContext>(opts => opts
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
               .UseSqlServer(configuration
               .GetConnectionString("SQLConnection"), b => b
               .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


        services
            .TryAddScoped(typeof(IProdutoRepository), typeof(ProdutoRepository));

        services
            .TryAddScoped(typeof(IProdutoService), typeof(ProductService));

        return services;

    }
}
