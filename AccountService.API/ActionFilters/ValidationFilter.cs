using AccountService.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AccountService.API.ActionFilters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Check if the x-device header is present
        if (!context.HttpContext.Request.Headers.TryGetValue("x-device", out var device))
        {
            context.Result = new BadRequestObjectResult("Missing x-device header");
            return;
        }

        // Retrieve the user object from the action parameters
        if (context.ActionArguments.TryGetValue("user", out var userObj))
        {
            var user = userObj as UserDto; // Assuming you have a User model

            // Perform validation based on the x-device header
            if (device == "mail" && !IsValidForMail(user))
            {
                context.Result = new BadRequestObjectResult("Invalid user input for mobile devices");
                return;
            }
            else if (device == "mobile" && !IsValidForMobile(user))
            {
                context.Result = new BadRequestObjectResult("Invalid user input for desktop devices");
                return;
            }
            else if (device == "web" && !IsValidForWeb(user))
            {
                context.Result = new BadRequestObjectResult("Invalid user input for web client");
                return;
            }
        }

        await next();
    }

    private bool IsValidForMail(UserDto user)
    {
        return !string.IsNullOrEmpty(user.FirstName) && !String.IsNullOrEmpty(user.Email);
    }

    private bool IsValidForMobile(UserDto user)
    {
        return IsValidPhone(user.Phone);
    }

    private bool IsValidForWeb(UserDto user)
    {
        return !string.IsNullOrEmpty(user.LastName) && 
               !string.IsNullOrEmpty(user.FirstName) && 
               !string.IsNullOrEmpty(user.MiddleName) &&
               !string.IsNullOrEmpty(user.PassportNumber) && 
               !string.IsNullOrEmpty(user.PlaceOfBirth) && 
               !string.IsNullOrEmpty(user.Phone) && 
               IsValidPhone(user.Phone);
    }

    private bool IsValidPhone(string phoneNumber)
    {
        if (String.IsNullOrEmpty(phoneNumber) || String.IsNullOrWhiteSpace(phoneNumber))
            return false;

        return phoneNumber.Length == 11 && phoneNumber.StartsWith("7");
    }
}