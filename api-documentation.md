# API Documentation

### With Swagger/OpenAPI in ASP.NET

Published on November 16, 2023 by Dario Krieger

Documenting APIs is vital as it provides clear instructions and usage guidelines for developers, enabling quicker onboarding, reducing errors, aiding troubleshooting, ensuring consistency in usage, facilitating updates, and ultimately making the API more accessible and appealing to users. Comprehensive documentation acts as a crucial resource, bridging the gap between API creators and users, fostering better understanding, and improving the overall development experience.

## Prerequisites

1. Web API Project using .NET 6.0 or higher with OpenAPI enabled

## Required Nuget Packages

1. [Swashbuckle.AspNetCore](https://www.nuget.org/packages/Swashbuckle.AspNetCore/)
2. [Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer)

## Provide OpenAPI Documentation in Existing Project

First step is to register some services in the existing project. There are several extension methods which are used in our approach.

1. [ConfigureApiVersioning.cs (optional)](../assets/ConfigureApiVersioning.cs)
2. [ConfigureSwaggerSwashbuckle.cs](../assets/ConfigureSwaggerSwashbuckle.cs)
3. [ConfigureSwaggerSwashbuckleOptions.cs](../assets/ConfigureSwaggerSwashbuckleOptions.cs)

We will now modify the Program.cs class with the extension methods from above.

"AddApiVersioningConfigured" (found in "ConfigureApiVersioning.cs") will support versioning on our documentation (don't implement if you
dont version your api) and "AddSwaggerSwashbuckleConfigured" 
(found in "ConfigureSwaggerSwashbuckle.cs") to configure the Swagger generator.

```csharp
builder.Services.AddApiVersioningConfigured(); // only implement if you version your api
builder.Services.AddSwaggerSwashbuckleConfigured();
```

We will now add the UseSwagger() middleware in our Program.cs class to serve OpenAPI definitions and UseSwaggerUi().

```csharp
if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve the generated OpenAPI definition as JSON files.
    app.UseSwagger();


    // Enable middleware to serve Swagger-UI (HTML, JS, CSS, etc.) by specifying the Swagger JSON endpoint(s).
    var descriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerUI(options =>
    {
        // Build a swagger endpoint for each discovered API version
        foreach (var description in descriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}
```

## Documentation via XML Attributes
To enable XML Documentation in our project, you will need to adjust the project-name.csproj file.
When enabled, the compiler will automatically generate CS1591 warnings for any public members without XML documentation.
We will exclude these warnings.

```csharp
<PropertyGroup>
  <GenerateDocumentationFile>True</GenerateDocumentationFile>
  <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

We are now able to add the XML comments to the controller actions using triple slashes.

```csharp
/// <summary>
/// Get a list with all items.
/// </summary>
[HttpGet]
public IActionResult Get()
{
    // implementation here
}
```

## Documenting API Responeses / Codes
Consumers would need beneficial information about the HTTP status codes and their response body.
Therefore we install the Swashbuckle.AspNetCore.Annotations nuget package.

- [Swashbuckle.AspNetCore.Annotations](https://www.nuget.org/packages/Swashbuckle.AspNetCore.Annotations/)

This is how an implementation of documenting response codes would look like:

```csharp
/// <summary>
/// Updates an existing event.
/// </summary>
/// <param name="eventCode">Identifier of the event to be updated.</param>
/// <param name="updatedEventDetails">Updated details for the event.</param>
/// <response code="200">Event was successfully updated and gets displayed in the response.&lt;/response>
/// <response code="400">Could not update the event.</response>
[HttpPut("{eventCode}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public ActionResult UpdateEvent(string eventCode, [FromBody, Required] EventDetails updatedEventDetails)
{
    var existingEvent = _eventManager.GetEventByCode(eventCode);
    _eventManager.UpdateEvent(existingEvent, updatedEventDetails);

    return Ok(existingEvent);
}
```

&copy; 2024 Dario Krieger