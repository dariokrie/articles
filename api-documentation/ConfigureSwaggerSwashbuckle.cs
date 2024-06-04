using System.Reflection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dr.TicketShop.ApiHost.Extensions;

/// <summary>
/// Configure the Swagger generator.
/// </summary>
public static class ConfigureSwaggerSwashbuckle
{
    public static void AddSwaggerSwashbuckleConfigured(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerSwashbuckleOptions>();

        services.AddSwaggerGen(options =>
        {
            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath, true);
        });

        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
    }
}
