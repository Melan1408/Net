using Microsoft.EntityFrameworkCore;
using MovieManagerReferenceData.Service;
using MovieManagerReferenceData.Data.Context;

namespace MovieManagerReferenceData.Api.Modules
{
    public static class CoreModule
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(scan => scan
               .FromAssembliesOf(typeof(IRequestHandler<>))
               .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                   .AsImplementedInterfaces()
                   .WithTransientLifetime()
               .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)))
                   .AsImplementedInterfaces()
                   .WithTransientLifetime());

            services.AddDbContext<MovieManagerReferenceDataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MovieManagerReferenceData"));
            });

            return services;
        }
    }
}
