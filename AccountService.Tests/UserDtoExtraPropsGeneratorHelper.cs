using AccountService.Entity;
using Bogus;

namespace AccountService.Tests;

public static class UserDtoGeneratorHelper
{
    public static UserDto GenerateUserDto()
    {
        var faker = GetUserDtoFaker();
        return faker.Generate();
    }

    public static IEnumerable<UserDto> GenerateUserDtoEnumerable(int count)
    {
        var faker = GetUserDtoFaker();
        
        for (int i = 0; i < count; i++)
            yield return faker.Generate();
    }

    private static Faker<UserDto> GetUserDtoFaker()
    {
        return new Faker<UserDto>()
            .RuleFor(e => e.FirstName, f => f.Name.FirstName())
            .RuleFor(e => e.LastName, f => f.Name.LastName())
            .RuleFor(e => e.MiddleName, f => f.Name.FirstName())
            .RuleFor(e => e.DateOfBirth, f => f.Date.Past(30))
            .RuleFor(e => e.PassportNumber, _ => GeneratePassportNumber())
            .RuleFor(e => e.PlaceOfBirth, f => f.Address.FullAddress())
            .RuleFor(e => e.Phone, _ => GeneratePhoneNumber())
            .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FirstName, e.LastName))
            .RuleFor(e => e.RegistrationAddress, f => f.Address.FullAddress())
            .RuleFor(e => e.ResidenceAddress, f => f.Address.FullAddress());
    }

    private static long GenerateRandomLong(int length)
    {
        var random = new Random();
        
        int firstDigit = random.Next(1, 10);
        
        string otherDigits = "";
        for (int i = 0; i < length; i++)
            otherDigits += random.Next(0, 10).ToString();

        string finalString = firstDigit.ToString() + otherDigits;

        return long.Parse(finalString);
    }

    private static string GeneratePassportNumber()
    {
        var random = new Random();
        string randomNumberString = GenerateRandomLong(9).ToString();
        return $"{randomNumberString.Substring(0, 4)} {randomNumberString.Substring(4)}";
    }

    private static string GeneratePhoneNumber()
    {
        var random = new Random();
        string randomNumberString = GenerateRandomLong(10).ToString();
        int length = randomNumberString.Length - 1;
        return $"7{randomNumberString.Substring(0, length)}";
    }
}