using Entities;
using ServiceContracts;
using ServiceContracts.Dtos;
using ServiceContracts.Enums;
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

  public IEnumerable<PersonResponse> GetSortedPersons(IEnumerable<PersonResponse> allPersons, string sortBy, SortOptions sortOrder)
  {
    if (string.IsNullOrEmpty(sortBy))
      sortBy = nameof(Person.Name);

    return sortBy switch
    {
      nameof(Person.Name) => sortOrder == SortOptions.Ascending ? allPersons.OrderBy(x => x.Name) : allPersons.OrderByDescending(x => x.Name),
      nameof(Person.Email) => sortOrder == SortOptions.Ascending ? allPersons.OrderBy(x => x.Email) : allPersons.OrderByDescending(x => x.Email),
      nameof(Person.Address) => sortOrder == SortOptions.Ascending ? allPersons.OrderBy(x => x.Address) : allPersons.OrderByDescending(x => x.Address),
      nameof(Person.DateOfBirth) => sortOrder == SortOptions.Ascending ? allPersons.OrderBy(x => x.DateOfBirth) : allPersons.OrderByDescending(x => x.DateOfBirth),
      nameof(Person.Gender) => sortOrder == SortOptions.Ascending ? allPersons.OrderBy(x => x.Gender) : allPersons.OrderByDescending(x => x.Gender),
      nameof(Person.CountryId) => sortOrder == SortOptions.Ascending ? allPersons.OrderBy(x => x.Country) : allPersons.OrderByDescending(x => x.Country),
      nameof(Person.ReceiveNewsLetters) => sortOrder == SortOptions.Ascending ? allPersons.OrderBy(x => x.ReceiveNewsLetters) : allPersons.OrderByDescending(x => x.ReceiveNewsLetters),
      _ => allPersons,
    };
  }

  public PersonResponse UpdatePerson(PersonUpdateRequest? person)
  {
    if(person is null)
      throw new ArgumentNullException(nameof(person));

    ValidationHelper.ModelValidation(person);

    var personInDb = _persons.FirstOrDefault(x => x.Id == person.Id);

    if(personInDb is null)
      throw new ArgumentException(nameof(person.Id));

    personInDb.Name = person.Name;
    personInDb.Email = person.Email;
    personInDb.Address = person.Address;
    personInDb.ReceiveNewsLetters = person.ReceiveNewsLetters;
    personInDb.CountryId = person.CountryId;
    personInDb.DateOfBirth = person.DateOfBirth;
    personInDb.Gender = person.Gender.ToString();

    return personInDb.ToPersonResponse();
  }

  public bool DeletePerson(Guid? id)
  {
    if(id is null)
      throw new ArgumentNullException(nameof(id));

    var existingPerson = _persons.FirstOrDefault(x => x.Id == id);

    if (existingPerson is null)
      return false;

    _persons.RemoveAll(x => x.Id == id);
    return true;
  }
}
