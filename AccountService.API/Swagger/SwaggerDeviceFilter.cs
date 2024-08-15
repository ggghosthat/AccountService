using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AccountService.API.Swagger;

public class SwaggerDeviceFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
        {
            operation.Parameters =
            [
                new OpenApiParameter
                {
                    Name = "X-Device",
                    In = ParameterLocation.Header,
                    Description = "device filter for validation",
                    Required = false
                    // Schema = new OpenApiSchema
                    // {
                    //     Type = "string"
                    // }
                },
            ];
        }
    }
}
