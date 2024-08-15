using System;
using System.ComponentModel.DataAnnotations;

namespace AccountService.Entity;

public class UserDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string PassportNumber { get; set; }

    public string PlaceOfBirth { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string RegistrationAddress { get; set; }

    public string ResidenceAddress { get; set; }
}