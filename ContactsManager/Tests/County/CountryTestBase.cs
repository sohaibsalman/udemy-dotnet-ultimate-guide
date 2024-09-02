using ServiceContracts;
using Services;

namespace Tests.County
{
  public class CountryTestBase
  {
    protected readonly ICountryService _countryService;

    public CountryTestBase()
    {
      _countryService = new CountryService(false);
    }
  }
}
