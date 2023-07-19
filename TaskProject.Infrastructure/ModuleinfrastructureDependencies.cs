using Microsoft.Extensions.DependencyInjection;
using TaskProject.Infrastructure.Abstracts;
using TaskProject.Infrastructure.InfrastructureBases;
using TaskProject.Infrastructure.Repositories;

namespace TaskProject.Infrastructure
{
    public static class ModuleinfrastructureDependencies
    {
        public static IServiceCollection AddinfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<ICategoryRepository, CategoryRepository>()
                .AddTransient<IRefreshTokenRepository, RefreshTokenRepository>()
                .AddTransient(typeof(IGenericeRepositoryAsync<>), typeof(GenericRepositoryAsync<>)); ;
            
            return services;
        }
    }
}