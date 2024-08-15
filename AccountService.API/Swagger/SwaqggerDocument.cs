using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AccountService.API.Swagger;

public class SwaggerDocument : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Info = new OpenApiInfo
        {
            Title = "Account Service",
            Version = "v1",
            Description = "Acount management service.",
            Contact = new OpenApiContact
            {
                Name = "Ildar",
                Email = "IldarKarachai@outlook.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0"
            }
        }; 
    }
}