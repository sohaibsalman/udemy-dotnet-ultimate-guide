using ServiceContracts.Dtos;
using ServiceContracts.Enums;

namespace Tests.Person;

public class AddPersonTests : PersonTestBase
{
  [Fact]
  public void AddPerson_PersonIsNull()
  {
    PersonAddRequest? person = null;

    Assert.Throws<ArgumentNullException>(() =>
    {
      _personService.AddPerson(person);
    });
  }

  [Fact]
  public void AddPerson_PersonNameIsNull()
  {
    PersonAddRequest? person = new PersonAddRequest()
    {
      Name = null
    };

    Assert.Throws<ArgumentException>(() =>
    {
      _personService.AddPerson(person);
    });
  }

  [Fact]
  public void AddPerson_ValidPerson()
  {
    PersonAddRequest? personToAdd = new PersonAddRequest()
    {
      Name = "Sohaib Salman", Address = "Lahore", CountryId = Guid.NewGuid(),
      DateOfBirth = DateTime.Parse("1999-01-12"), Gender = GenderOptions.Male,
      ReceiveNewsLetters = true, Email = "sohaib@test.com"
    };

    PersonResponse personResponse = _personService.AddPerson(personToAdd);
    List<PersonResponse> allPersons = _personService.GetAllPersons().ToList();

    Assert.True(personResponse.Id != Guid.Empty);
    Assert.Contains(personResponse, allPersons);
  }
}
