using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using TaskProject.Core.Bases;
using TaskProject.Core.Behaviors;
using TaskProject.Core.Features.Products.Commands.Models;
using TaskProject.Core.Features.Products.Commands.Validarions;
using TaskProject.Service.Abstracts;
using TaskProject.Service.Implementations;

namespace TaskProject.Core
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            //configuration of mediator
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
           // Configuration of AutoMapper
           services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Get Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // 
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.Configure<ApiBehaviorOptions>(x => { x.SuppressModelStateInvalidFilter = true; });


            return services;
        }
    }
}
