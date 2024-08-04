using ServiceContracts;
using ServiceContracts.Dtos;
using ServiceContracts.Enums;
using Services;

namespace Tests.Person;

public class PersonTestBase
{
  protected readonly IPersonService _personService;
  protected readonly ICountryService _countryService;

  protected PersonTestBase()
  {
    _countryService = new CountryService();
    _personService = new PersonService(_countryService);
  }

  protected List<PersonResponse> CreatePersons()
  {
    // Create Countries
    var countriesToAdd = new List<CountryAddRequest>
      {
        new() { Name = "Pakistan" },
        new() { Name = "New Zealand" },
        new() { Name = "America" },
      };

    var createdCountries = new List<CountryResponse>();
    foreach (var country in countriesToAdd)
    {
      createdCountries.Add(_countryService.AddCountry(country));
    }

    var personsToAdd = new List<PersonAddRequest>
      {
        new() { Name = "Sohaib Salman", Email="sohaib@test.com", Address = "Lahore", DateOfBirth = DateTime.Parse("1999-01-12"),
          Gender = GenderOptions.Male, ReceiveNewsLetters = true, CountryId = createdCountries.FirstOrDefault(x => x.Name == "Pakistan")?.Id },
        new() { Name = "Saad Salman", Email="saad@test.com", Address = "Lahore", DateOfBirth = DateTime.Parse("1995-02-08"),
          Gender = GenderOptions.Male, ReceiveNewsLetters = true, CountryId = createdCountries.FirstOrDefault(x => x.Name == "Pakistan")?.Id },
        new() { Name = "Nasir Abdul Rehman", Email="nasir@test.com", Address = "Christ Church", DateOfBirth = DateTime.Parse("2015-09-20"),
          Gender = GenderOptions.Male, ReceiveNewsLetters = true, CountryId = createdCountries.FirstOrDefault(x => x.Name == "New Zealand")?.Id },
        new() { Name = "John Wick", Email="john@test.com", Address = "New York", DateOfBirth = DateTime.Parse("1992-10-20"),
          Gender = GenderOptions.Male, ReceiveNewsLetters = true, CountryId = createdCountries.FirstOrDefault(x => x.Name == "America")?.Id },
        new() { Name = "Nina Williams", Email="nina@test.com", Address = "Texas", DateOfBirth = DateTime.Parse("1996-10-20"),
          Gender = GenderOptions.Female, ReceiveNewsLetters = true, CountryId = createdCountries.FirstOrDefault(x => x.Name == "America")?.Id }
      };

    var createdPersons = new List<PersonResponse>();
    foreach (var person in personsToAdd)
    {
      createdPersons.Add(_personService.AddPerson(person));
    }
    return createdPersons;
  }
}
