using ServiceContracts;
using Services;

namespace Tests.Person;

public class PersonTestBase
{
  protected readonly IPersonService _personService;

  protected PersonTestBase()
  {
    _personService = new PersonService(new CountryService());
  }
}
