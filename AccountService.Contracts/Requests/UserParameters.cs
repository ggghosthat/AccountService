namespace AccountService.Contracts.Requests;

public class UserParameters
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    public string ToQueryString()
    {
        var query = new List<string>();

        if (!string.IsNullOrEmpty(FirstName))
            query.Add($"FirstName={FirstName}");
        if (!string.IsNullOrEmpty(LastName))
            query.Add($"LastName={LastName}");
        if (!string.IsNullOrEmpty(MiddleName))
            query.Add($"MiddleName={MiddleName}");
        if (!string.IsNullOrEmpty(Phone))
            query.Add($"Phone={Phone}");
        if (!string.IsNullOrEmpty(Email))
            query.Add($"Email={Email}");

        return '?' + string.Join("&", query);
    }
}