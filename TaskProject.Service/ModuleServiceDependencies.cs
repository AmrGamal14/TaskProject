using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using TaskProject.Service.Abstracts;
using TaskProject.Service.Implementations;

namespace TaskProject.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>()
            .AddTransient<ICategoryService, CategoryService>()
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            .AddSingleton<IHostEnvironment, HostingEnvironment>()
            .AddTransient<IAuthenticationService, AuthenticationService>()
            .AddTransient<IAuthorizationService, AuthorizationService>();
            return services;
        }
    }
}