using ServiceContracts.Dtos;
using ServiceContracts.Enums;

namespace ServiceContracts;

public interface IPersonService
{
  PersonResponse AddPerson(PersonAddRequest? person);
  IEnumerable<PersonResponse> GetAllPersons();
  PersonResponse? GetPersonById(Guid? id);
  IEnumerable<PersonResponse> GetFilteredPersons(string? searchBy, string? searchString);
  IEnumerable<PersonResponse> GetSortedPersons(IEnumerable<PersonResponse> allPersons, string sortBy, SortOptions sortOrder);
}
