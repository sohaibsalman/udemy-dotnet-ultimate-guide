namespace Tests.Person
{
  public class DeletePersonTest : PersonTestBase
  {
    [Fact]
    public void DeletePersonTest_NullId()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        _personService.DeletePerson(null);
      });
    }

    [Fact]
    public void DeletePersonTest_InvalidId()
    {
      Guid id = Guid.NewGuid();
      var createdPersons = CreatePersons();

      bool isDeleted = _personService.DeletePerson(id);
      var allPersons = _personService.GetAllPersons();

      Assert.False(isDeleted);
      Assert.Equal(createdPersons.Count, allPersons.Count());
    }

    [Fact]
    public void DeletePersonTest_ValidId()
    {
      var createdPersons = CreatePersons();
      var created = createdPersons.First();

      var existingPerson = _personService.GetPersonById(created.Id);
      Assert.NotNull(existingPerson);

      bool isDeleted = _personService.DeletePerson(created.Id);
      var allPersons = _personService.GetAllPersons();
      var existingPersonAfterDelete = _personService.GetPersonById(created.Id);

      Assert.True(isDeleted);
      Assert.Equal(createdPersons.Count - 1, allPersons.Count());
      Assert.Null(existingPersonAfterDelete);
    }
  }
}
