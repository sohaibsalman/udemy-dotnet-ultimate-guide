using Entities;
using ServiceContracts;
using ServiceContracts.Dtos;
using Services.Helpers;
using System;

namespace Services;

public class PersonService : IPersonService
{
  private readonly ICountryService _countryService;
  private readonly List<Person> _persons = new List<Person>();

  public PersonService(ICountryService countryService)
  {
    _countryService = countryService;
  }

  public PersonResponse AddPerson(PersonAddRequest? person)
  {
    if (person == null)
      throw new ArgumentNullException(nameof(person));

    // Model validations
    ValidationHelper.ModelValidation(person);

    Person personToAdd = person.ToPerson();
    personToAdd.Id = Guid.NewGuid();

    _persons.Add(personToAdd);
    return ConvertPersonToPersonResponse(personToAdd);
  }

  public IEnumerable<PersonResponse> GetAllPersons()
  {
    return _persons.Select(person => person.ToPersonResponse());
  }


  private PersonResponse ConvertPersonToPersonResponse(Person person)
  {
    PersonResponse response = person.ToPersonResponse();
    response.Country = _countryService.GetCountryById(person.CountryId)?.Name;

    return response;
  }
}
