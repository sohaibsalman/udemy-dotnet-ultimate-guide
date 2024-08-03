using ServiceContracts.Dtos;

namespace ServiceContracts;

public interface IPersonService
{
  PersonResponse AddPerson(PersonAddRequest? person);
  IEnumerable<PersonResponse> GetAllPersons();
}
