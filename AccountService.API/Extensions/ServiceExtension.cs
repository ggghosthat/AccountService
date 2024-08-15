using AccountService.API.ActionFilters;
using AccountService.Contracts.Repository;
using AccountService.Repository.Context;
using AccountService.Repository;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Configuration;

namespace AccountService.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options => 
        {
            options.AddPolicy("CorsPolicy", builder =>
              builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(opts =>
          opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => 
            b.MigrationsAssembly("AccountService.API")));

    public static void ConfigureInMemoryDb(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(options => options.UseInMemoryDatabase("InMemoryDb"));

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void RegisterMediatR(this IServiceCollection services) =>
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

    public static void RegisterAutoMapper(this IServiceCollection services) =>
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies() );

    public static void RegisterFilters(this IServiceCollection services) =>
        services.AddScoped<ValidationFilter>();
}