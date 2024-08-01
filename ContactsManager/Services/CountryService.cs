using Entities;
using ServiceContracts;
using ServiceContracts.Dtos;

namespace Services
{
  public class CountryService : ICountryService
  {
    private readonly List<Country> _countries = new List<Country>();

    public CountryResponse AddCountry(CountryAddRequest request)
    {
      if(request == null)
        throw new ArgumentNullException(nameof(request));

      if(string.IsNullOrEmpty(request.Name)) 
        throw new ArgumentNullException(nameof(request.Name));

      if (_countries.Where(country => country.Name.Equals(request.Name)).Count() > 0)
        throw new ArgumentException();

      Country country = new Country()
      {
        Id = Guid.NewGuid(),
        Name = request.Name,
      };

      _countries.Add(country);

      return country.ToCountryResponse();
    }

    public IEnumerable<CountryResponse> GetAllCountries()
    {
      return _countries.Select(country => country.ToCountryResponse());
    }

    public CountryResponse? GetCountryById(Guid? id)
    {
      if (id is null) return null;

      return _countries.FirstOrDefault(country => country.Id == id)?.ToCountryResponse();
    }
  }
}
