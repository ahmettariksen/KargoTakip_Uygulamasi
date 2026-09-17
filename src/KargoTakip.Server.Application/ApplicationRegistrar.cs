using KargoTakip.Server.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace KargoTakip.Server.Application;
public static class ApplicationRegistrar
{
    // Dependecy Injection metodumuz.
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssemblies(typeof(ApplicationRegistrar).Assembly);
            conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(ApplicationRegistrar).Assembly);

        return services;
    }
}
