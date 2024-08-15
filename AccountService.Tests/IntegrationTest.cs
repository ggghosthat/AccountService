using AccountService.API;
using AccountService.API.Routes;
using AccountService.Entity;
using AccountService.Repository.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;

namespace AccountService.Tests;

public class IntegrationTest
{
    protected readonly HttpClient TestClient;

    protected IntegrationTest()
    {
        var appFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => 
            {
                builder.ConfigureServices(services => 
                { 
                    services.RemoveAll(typeof(RepositoryContext)); 
                    services.AddDbContext<RepositoryContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                });
            });

        TestClient = appFactory.CreateClient();

    }

    protected UserDto GetTestUserDto()
    {
        return UserDtoGeneratorHelper.GenerateUserDto();
    }

    protected IEnumerable<UserDto> GetTestUsersDto(int count)
    {
        return UserDtoGeneratorHelper.GenerateUserDtoEnumerable(count);
    }

    protected async Task<User> CreateUserPostAsync(UserDto request)
    {
        TestClient.DefaultRequestHeaders.Add("X-Device", "web");
        var response = await TestClient.PostAsJsonAsync(ApiRoutes.User.CreateUser1, request);
        return await response.Content.ReadFromJsonAsync<User>();
    }

    protected async Task<IEnumerable<User>> CreateUsersPostAsync(IEnumerable<UserDto> requests)
    {
        List<User> usersResult = new ();
        TestClient.DefaultRequestHeaders.Add("X-Device", "web");
        foreach (var userDto in requests)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.User.CreateUser1, userDto);
            var user = await response.Content.ReadFromJsonAsync<User>();
            usersResult.Add(user);
        }

        return usersResult;
    }
}