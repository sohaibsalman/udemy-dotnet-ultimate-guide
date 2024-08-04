using ServiceContracts.Dtos;
using ServiceContracts.Enums;

namespace Tests.Person
{
  public class GetPersonTests : PersonTestBase
  {
    [Fact]
    public void GetPersonById_NullId()
    {
      Guid? id = null;
      PersonResponse? response = _personService.GetPersonById(id);
      Assert.Null(response);
    }

    [Fact]
    public void GetPersonById_InvalidId()
    {
      Guid id = Guid.NewGuid();
      PersonAddRequest person = new PersonAddRequest()
      {
        Name = "name",
        Email = "sohaib@test.com",
      };

      _personService.AddPerson(person);
      PersonResponse? response = _personService.GetPersonById(id);
      Assert.Null(response);
    }

    [Fact]
    public void GetPersonById_ValidPerson()
    {
      PersonAddRequest person = new PersonAddRequest()
      {
        Name = "name",
        Email = "sohaib@test.com",
      };

      PersonResponse addPersonResponse = _personService.AddPerson(person);
      PersonResponse? getPersonResponse = _personService.GetPersonById(addPersonResponse.Id);
      Assert.NotNull(getPersonResponse);
      Assert.Equal(addPersonResponse, getPersonResponse);
    }

    [Fact]
    public void GetFilteredPersons_EmptySearchString()
    {
      List<PersonResponse> createdPersons = CreatePersons();
      var filteredPersons = _personService.GetFilteredPersons(nameof(Entities.Person.Name), "");

      foreach (var person in createdPersons)
      {
        Assert.Contains(person, filteredPersons);
      }
    }

    [Fact]
    public void GetFilteredPersons_SearchByName()
    {
      string searchString = "ai";
      string searchBy = nameof(Entities.Person.Name);

      List<PersonResponse> createdPersons = CreatePersons().Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
      var filteredPersons = _personService.GetFilteredPersons(searchBy, searchString);

      Assert.Equal(createdPersons.Count, filteredPersons.Count());

      foreach (var person in createdPersons)
      {
        Assert.Contains(person, filteredPersons);
      }
    }

    [Fact]
    public void GetFilteredPersons_SearchByEmail()
    {
      string searchString = "test";
      string searchBy = nameof(Entities.Person.Email);

      List<PersonResponse> createdPersons = CreatePersons().Where(x => !string.IsNullOrEmpty(x.Email) && x.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
      var filteredPersons = _personService.GetFilteredPersons(searchBy, searchString);
      
      Assert.Equal(createdPersons.Count, filteredPersons.Count());

      foreach (var person in createdPersons)
      {
        Assert.Contains(person, filteredPersons);
      }
    }

    [Fact]
    public void GetFilteredPersons_SearchByGender()
    {
      string searchString = GenderOptions.Male.ToString();
      string searchBy = nameof(Entities.Person.Gender);

      List<PersonResponse> createdPersons = CreatePersons().Where(x => x.Gender == searchString).ToList();
      var filteredPersons = _personService.GetFilteredPersons(searchBy, searchString);

      Assert.Equal(createdPersons.Count, filteredPersons.Count());

      foreach (var person in createdPersons)
      {
        Assert.Contains(person, filteredPersons);
      }
    }

    [Fact]
    public void GetFilteredPersons_SearchByCountry()
    {
      string searchString = "Pakistan";
      string searchBy = nameof(Entities.Person.CountryId);

      List<PersonResponse> createdPersons = CreatePersons().Where(x => x.Country == "Pakistan").ToList();
      var filteredPersons = _personService.GetFilteredPersons(searchBy, searchString);

      Assert.Equal(createdPersons.Count, filteredPersons.Count());

      foreach (var person in createdPersons)
      {
        Assert.Contains(person, filteredPersons);
      }
    }
  }
}
