using AccountService.API.Extensions;
using AccountService.API.Swagger;
using Microsoft.Extensions.DependencyInjection;

public partial class Program 
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        // Add services to the container.
        builder.Services.ConfigureCors();
        builder.Services.ConfigureSqlContext(configuration);
        builder.Services.ConfigureInMemoryDb(configuration);
        builder.Services.ConfigureRepositoryManager();
        builder.Services.RegisterAutoMapper();
        builder.Services.RegisterMediatR();
        builder.Services.RegisterFilters();

        builder.Services.AddControllers()
                        .AddNewtonsoftJson();
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.DocumentFilter<SwaggerDocument>();
            c.OperationFilter<SwaggerDeviceFilter>();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }
}