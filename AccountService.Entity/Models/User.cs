using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountService.Entity;

public class User
{
    [Column("UserId")] 
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "First name is required.")]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [MaxLength(50)]
    public string LastName { get; set; }
    
    [MaxLength(50)]
    public string MiddleName { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [RegularExpression(@"^\d{4} \d{6}$", ErrorMessage = "Passport number should be in format of ХХХХ ХХХХХХ.")]
    public string PassportNumber { get; set; }

    [MaxLength(100)]
    public string PlaceOfBirth { get; set; }

    [RegularExpression(@"^7\d{10}$", ErrorMessage = "Phone should be in format of 7XXXXXXXXXX.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    [MaxLength(200)]
    public string RegistrationAddress { get; set; }

    [MaxLength(200)]
    public string ResidenceAddress { get; set; }
}