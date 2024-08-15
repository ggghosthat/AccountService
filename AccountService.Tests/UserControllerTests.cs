using AccountService.API.Routes;
using AccountService.Entity;
using AccountService.Contracts.Requests;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;

namespace AccountService.Tests;

public class UserCotnrollerTests : IntegrationTest 
{
    [Fact]
    public async Task CreateUser_WithUserDto_UserResult()
    {
        //arrange        
        var userDto = GetTestUserDto();

        //act
        var createdUser = await CreateUserPostAsync(userDto);

        //assert
        userDto.Should().BeOfType<UserDto>();
        createdUser.Should().BeOfType<User>();
    }

    [Fact]
    public async Task GetUserById_WithUserId_UserResult()
    {
        //arrange        
        var usersDto = GetTestUsersDto(10);
        var createdUsers = await CreateUsersPostAsync(usersDto);
        var testId = createdUsers.First().Id.ToString();

        //act
        var response = await TestClient.GetAsync(ApiRoutes.User.GetUserById.Replace("{id}", testId.ToString()));

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var returnedUser = await response.Content.ReadFromJsonAsync<User>();
        returnedUser.Should().BeOfType<User>();
    }

    [Fact]
    public async Task SearchUsers_WithSameParameters()
    {
        //arrange        
        var usersDto = GetTestUsersDto(10);
        var createdUsers = await CreateUsersPostAsync(usersDto);
        var searchFirstNameParameter = new UserParameters()
        {
            FirstName = createdUsers.First().FirstName
        };
        string queryString = searchFirstNameParameter.ToQueryString();
        string query = ApiRoutes.User.SearchUsers + queryString;

        //act
        var response = await TestClient.GetAsync(query);

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteUser_WithId()
    {
        //arrange
        var usersDto = GetTestUsersDto(10);
        var createdUsers = await CreateUsersPostAsync(usersDto);
        var testId = createdUsers.First().Id.ToString();

        //act
        var preresponse = await TestClient.GetAsync(ApiRoutes.User.GetUserById.Replace("{id}", testId.ToString()));
        var response = await TestClient.DeleteAsync(ApiRoutes.User.DeleteUser.Replace("{id}", testId.ToString()));
        var postresponse = await TestClient.GetAsync(ApiRoutes.User.DeleteUser.Replace("{id}", testId.ToString()));

        //assert
        preresponse.StatusCode.Should().Be(HttpStatusCode.OK);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        postresponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}