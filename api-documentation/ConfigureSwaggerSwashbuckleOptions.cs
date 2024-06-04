using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dr.TicketShop.ApiHost.Extensions;

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
            Description = "Manage API endpoints of TicketShop",
            Contact = new OpenApiContact { Name = "Dario Krieger", Email = "Dario.Krieger@basenet.ch" },
        };

        options.SwaggerDoc("v1", info);
    }
}
