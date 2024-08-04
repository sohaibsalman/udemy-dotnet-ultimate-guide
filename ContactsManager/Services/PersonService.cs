using Entities;
using ServiceContracts;
using ServiceContracts.Dtos;
using Services.Helpers;

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

  public PersonResponse? GetPersonById(Guid? id)
  {
    return _persons.Where(x => x.Id == id).FirstOrDefault()?.ToPersonResponse();
  }

  public IEnumerable<PersonResponse> GetFilteredPersons(string? searchBy, string? searchString)
  {
    var allPersons = _persons.Select(x => ConvertPersonToPersonResponse(x));

    var matchedPersons = allPersons;

    if (string.IsNullOrEmpty(searchString) || string.IsNullOrEmpty(searchBy))
      return matchedPersons;

    matchedPersons = searchBy switch
    {
      nameof(Person.Name) => allPersons.Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)),
      nameof(Person.Email) => allPersons.Where(x => !string.IsNullOrEmpty(x.Email) && x.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)),
      nameof(Person.Address) => allPersons.Where(x => !string.IsNullOrEmpty(x.Address) && x.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase)),
      nameof(Person.Gender) => allPersons.Where(x => !string.IsNullOrEmpty(x.Gender) && x.Gender.Equals(searchString, StringComparison.OrdinalIgnoreCase)),
      nameof(Person.ReceiveNewsLetters) => allPersons.Where(x => x.ReceiveNewsLetters),
      nameof(Person.CountryId) => allPersons.Where(x => !string.IsNullOrEmpty(x.Country) && x.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase)),
      nameof(Person.DateOfBirth) => allPersons.Where(x => x.DateOfBirth != null && x.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase)),
      _ => allPersons,
    };

    return matchedPersons;
  }

  private PersonResponse ConvertPersonToPersonResponse(Person person)
  {
    PersonResponse response = person.ToPersonResponse();
    response.Country = _countryService.GetCountryById(person.CountryId)?.Name;

    return response;
  }
}
