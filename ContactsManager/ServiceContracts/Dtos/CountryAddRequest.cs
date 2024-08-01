using Entities;

namespace ServiceContracts.Dtos
{
  public class CountryAddRequest
  {
    public string? Name { get; set; }

    public Country ToCountry()
    {
      return new Country { Name = Name };
    }
  }
}
