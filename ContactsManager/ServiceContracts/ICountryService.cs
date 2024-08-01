using ServiceContracts.Dtos;

namespace ServiceContracts
{
  public interface ICountryService
  {
    CountryResponse AddCountry(CountryAddRequest request);
  }
}
