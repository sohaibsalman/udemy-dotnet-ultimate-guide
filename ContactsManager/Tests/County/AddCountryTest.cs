using ServiceContracts.Dtos;

namespace Tests.County
{
  public class AddCountryTest : CountryTestBase
  {
    [Fact]
    public void AddCountry_NullRequestObject()
    {
      CountryAddRequest request = null;

      Assert.Throws<ArgumentNullException>(() =>
      {
        _countryService.AddCountry(request);
      });
    }

    [Fact]
    public void AddCountry_NullName()
    {
      CountryAddRequest request = new CountryAddRequest
      {
        Name = null
      };

      Assert.Throws<ArgumentNullException>(() =>
      {
        _countryService.AddCountry(request);
      });
    }

    [Fact]
    public void AddCountry_EmptyName()
    {
      CountryAddRequest request = new CountryAddRequest
      {
        Name = string.Empty
      };

      Assert.Throws<ArgumentNullException>(() =>
      {
        _countryService.AddCountry(request);
      });
    }

    [Fact]
    public void AddCountry_DuplicateName()
    {
      CountryAddRequest request = new CountryAddRequest
      {
        Name = "Pakistan"
      };

      CountryAddRequest request2 = new CountryAddRequest
      {
        Name = "Pakistan"
      };

      Assert.Throws<ArgumentException>(() =>
      {
        _countryService.AddCountry(request);
        _countryService.AddCountry(request);
      });
    }

    [Fact]
    public void AddCountry_ProperName()
    {
      CountryAddRequest request = new CountryAddRequest
      {
        Name = "Pakistan"
      };

      CountryResponse response = _countryService.AddCountry(request);

      Assert.True(response.Id != Guid.Empty);

    }
  }
}