using ServiceContracts.Dtos;

namespace Tests.Person
{
  public class UpdatePersonTest : PersonTestBase
  {
    [Fact]
    public void UpdatePerson_PersonNull()
    {
      PersonUpdateRequest person = null;

      Assert.Throws<ArgumentNullException>(() =>
      {
        _personService.UpdatePerson(person);
      });
    }

    [Fact]
    public void UpdatePerson_PersonInvalidId() 
    {
      PersonUpdateRequest person = new PersonUpdateRequest() { Id = Guid.NewGuid() };

      Assert.Throws<ArgumentException>(() =>
      {
        _personService.UpdatePerson(person);
      });
    }

    [Fact]
    public void UpdatePerson_PersonEmptyName()
    {
      var createdPerson = CreatePersons().First();
      PersonUpdateRequest person = new PersonUpdateRequest() { Id = createdPerson.Id, Name = "" };

      Assert.Throws<ArgumentException>(() =>
      {
        _personService.UpdatePerson(person);
      });
    }

    [Fact]
    public void UpdatePerson_PersonValidDetails()
    {
      var createdPerson = CreatePersons().First().ToPersonUpdateRequest();
      createdPerson.ReceiveNewsLetters = false;

      var updatedPerson = _personService.UpdatePerson(createdPerson);
      var getPerson = _personService.GetPersonById(updatedPerson.Id);

      Assert.Equal(getPerson, updatedPerson);
    }
  }
}
