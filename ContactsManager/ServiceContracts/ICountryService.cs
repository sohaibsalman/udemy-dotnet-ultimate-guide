using ServiceContracts.Dtos;

namespace ServiceContracts
{
  public interface ICountryService
  {
    CountryResponse AddCountry(CountryAddRequest request);
    IEnumerable<CountryResponse> GetAllCountries();
    CountryResponse? GetCountryById(Guid? id);
  }
}
