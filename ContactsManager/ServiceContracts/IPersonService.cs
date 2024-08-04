using ServiceContracts.Dtos;

namespace ServiceContracts;

public interface IPersonService
{
  PersonResponse AddPerson(PersonAddRequest? person);
  IEnumerable<PersonResponse> GetAllPersons();
  PersonResponse? GetPersonById(Guid? id);
  IEnumerable<PersonResponse> GetFilteredPersons(string? searchBy, string? searchString);
}
