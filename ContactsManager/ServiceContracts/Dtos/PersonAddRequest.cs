using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.Dtos;

public class PersonAddRequest
{
  [Required(ErrorMessage = "Person name can't be blank")]
  public string? Name { get; set; }

  [EmailAddress(ErrorMessage = "Email is not valid")]
  [Required(ErrorMessage = "Email can't be blank")]
  public string? Email { get; set; }
  public DateTime? DateOfBirth { get; set; }
  public GenderOptions? Gender { get; set; }
  public string? Address { get; set; }
  public bool ReceiveNewsLetters { get; set; }

  public Guid? CountryId { get; set; }

  public Person ToPerson()
  {
    return new Person()
    {
      Name = Name,
      Email = Email,
      DateOfBirth = DateOfBirth,
      Gender = Gender.ToString(),
      Address = Address,
      ReceiveNewsLetters = ReceiveNewsLetters,
      CountryId = CountryId
    };
  }
}