using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace YOUR-NAMESPACE;

/// <summary>
/// Configures the Swagger generation options.
/// </summary>
public class ConfigureSwaggerSwashbuckleOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        var info = new OpenApiInfo()
        {
            Title = "TicketShop API",
            Description = "Manage API endpoints of [Project name]",
            Contact = new OpenApiContact { Name = "Example User", Email = "example@user.com" },
        };

        options.SwaggerDoc("v1", info);
    }
}
