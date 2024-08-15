namespace AccountService.API.Routes;

public static class ApiRoutes
{
    public static class User
    {
        public const string Controller = "/api/v1/user";
        
        public const string CreateUser1 = Controller;
        public const string GetUserById = Controller + "/{id}";
        public const string SearchUsers = Controller;
        public const string DeleteUser = Controller + "/delete/{id}";
    }
} 