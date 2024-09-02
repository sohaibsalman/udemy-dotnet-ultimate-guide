using Entities;
using ServiceContracts;
using ServiceContracts.Dtos;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services;

public class PersonService : IPersonService
{
    private readonly ICountryService _countryService;
    private readonly List<Person> _persons;

    public PersonService(ICountryService countryService, bool initialzie = true)
    {
        _persons = new List<Person>();
        _countryService = countryService;

        if (initialzie)
        {
            _persons.AddRange(

                new Person[]
                {
                    new Person() { Id = Guid.Parse("b9702945-e060-4d7d-bf86-a308613b16f5"), Name = "Sohaib Salman", Address = "Lahore",
                        CountryId = Guid.Parse("0efbe8fd-811c-4ec8-a50e-8fdf65f30802"), DateOfBirth = DateTime.Parse("1999-01-12"), Email="sohaib@test.com",
                        Gender = GenderOptions.Male.ToString(), ReceiveNewsLetters = true },
                    new Person() { Id = Guid.Parse("7ad0778f-dbc7-4318-8ccb-2ddf6480ff8d"), Name = "John Wick", Address = "New York",
                        CountryId = Guid.Parse("d37bc149-2e11-4406-9ac4-14cddabc9081"), DateOfBirth = DateTime.Parse("1995-03-21"), Email="john@test.com",
                        Gender = GenderOptions.Male.ToString(), ReceiveNewsLetters = false },
                    new Person() { Id = Guid.Parse("3c46c6f9-ba98-4f1e-aaa4-bf9440bc9b7c"), Name = "Lisa", Address = "Tokyo",
                        CountryId = Guid.Parse("3c46c6f9-ba98-4f1e-aaa4-bf9440bc9b7c"), DateOfBirth = DateTime.Parse("1998-06-15"), Email="lisa@test.com",
                        Gender = GenderOptions.Female.ToString(), ReceiveNewsLetters = true },
                    new Person() { Id = Guid.Parse("48edf824-e9a7-415f-b0af-b8b31b1aa767"), Name = "Nick Fury", Address = "Aukland",
                        CountryId = Guid.Parse("7a0d3d05-e890-4ad0-a1c4-75efb1586d81"), DateOfBirth = DateTime.Parse("1983-01-24"), Email="nick@test.com",
                        Gender = GenderOptions.Male.ToString(), ReceiveNewsLetters = true },
                    new Person() { Id = Guid.Parse("86d4a56e-7f3a-4c9f-8d10-755ebe76b56d"), Name = "Elizabeth", Address = "Toronto",
                        CountryId = Guid.Parse("6a6db927-dc35-4249-af29-0d6edfe352e0"), DateOfBirth = DateTime.Parse("2001-08-30"), Email="elizabeth@test.com",
                        Gender = GenderOptions.Female.ToString(), ReceiveNewsLetters = false },
                });
        }
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
        if (person is null)
            throw new ArgumentNullException(nameof(person));

        ValidationHelper.ModelValidation(person);

        var personInDb = _persons.FirstOrDefault(x => x.Id == person.Id);

        if (personInDb is null)
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
        if (id is null)
            throw new ArgumentNullException(nameof(id));

        var existingPerson = _persons.FirstOrDefault(x => x.Id == id);

        if (existingPerson is null)
            return false;

        _persons.RemoveAll(x => x.Id == id);
        return true;
    }
}
