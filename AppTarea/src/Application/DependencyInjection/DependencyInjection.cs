
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using MediatR;
using System.Reflection;
using FluentValidation;
using Application.Common.Behaviors;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registrar MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Registrar AutoMapper
            services.AddAutoMapper(typeof(Application.MappingProfiles.TableroProfile));

            // Registrar FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Registrar el pipeline de validación para MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Application.Common.Behaviors.ValidationBehavior<,>));

            return services;
        }
    }
}
