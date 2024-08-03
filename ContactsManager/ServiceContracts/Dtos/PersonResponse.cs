using Entities;

namespace ServiceContracts.Dtos;

public class PersonResponse
{
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public string? Email { get; set; }
  public DateTime? DateOfBirth { get; set; }
  public string? Gender { get; set; }
  public string? Address { get; set; }
  public bool ReceiveNewsLetters { get; set; }
  public string? Country { get; set; }
  public double? Age { get; set; }


  public override bool Equals(object? obj)
  {
    if (obj is null) return false;

    if (obj.GetType() != typeof(PersonResponse)) return false;

    PersonResponse person = (PersonResponse)obj;

    return Id == person.Id && Name == person.Name && Email == person.Email &&
           DateOfBirth == person.DateOfBirth && Gender == person.Gender
           && Address == person.Address && ReceiveNewsLetters == person.ReceiveNewsLetters;
  }

  public override int GetHashCode()
  {
    return base.GetHashCode();
  }
}

public static class PersonExtensions
{
  public static PersonResponse ToPersonResponse(this Person person)
  {
    return new PersonResponse()
    {
      Id = person.Id,
      Name = person.Name,
      Email = person.Email,
      DateOfBirth = person.DateOfBirth,
      Gender = person.Gender,
      Address = person.Address,
      ReceiveNewsLetters = person.ReceiveNewsLetters,
      Age = person.DateOfBirth != null ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null
    };
  }
}

