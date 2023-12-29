using api.Utilities;
using core.Repositories;
using core.Services;
using infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Extensions;

public static class ApplicationServicesExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IComputationService, ComputationService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMapperBase, MapperlyMapper>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IComputationService), typeof(ComputationService));
        services.AddControllers(opts =>
        {
            opts.CacheProfiles.Add("Default120",
            new CacheProfile { Duration = 120, Location = ResponseCacheLocation.Any});
        });

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            options.ReportApiVersions = true;
        }
        );
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
     
    }
}