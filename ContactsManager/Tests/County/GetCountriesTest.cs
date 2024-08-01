using ServiceContracts.Dtos;

namespace Tests.County
{
  public class GetCountriesTest : CountryTestBase
  {
    [Fact]
    public void GetAllCountries_Empty()
    {
      List<CountryResponse> countries = _countryService.GetAllCountries().ToList();
      Assert.True(!countries.Any());
    }

    [Fact]
    public void GetAllCountries_WithData()
    {
      List<CountryAddRequest> countriesToAdd = new List<CountryAddRequest>
      {
        new() { Name = "Pakistan"},
        new() {Name = "New Zealand"}
      };

      countriesToAdd.ForEach(country =>
      {
        _countryService.AddCountry(country);
      });

      List<CountryResponse> countries = _countryService.GetAllCountries().ToList();

      foreach (var country in countriesToAdd)
      {
        Assert.Contains(country, countriesToAdd);
      }
    }

    [Fact]
    public void GetCountryById_NullId()
    {
      CountryResponse? response = _countryService.GetCountryById(null);
      Assert.Null(response);
    }

    [Fact]
    public void GetCountryById_ValidId()
    {
      CountryAddRequest request = new() { Name = "Pakistan" };
      CountryResponse response = _countryService.AddCountry(request);

      Assert.NotNull(response);
    }
  }
}
