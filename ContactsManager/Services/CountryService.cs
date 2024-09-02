using Entities;
using ServiceContracts;
using ServiceContracts.Dtos;
using System.Collections.Generic;

namespace Services
{
    public class CountryService : ICountryService
    {
        private readonly List<Country> _countries;

        public CountryService(bool initialize = true)
        {
            _countries = new List<Country>();

            if (initialize)
            {
                _countries.AddRange(
                    new List<Country>()
                    {
                        new Country() { Id = Guid.Parse("0efbe8fd-811c-4ec8-a50e-8fdf65f30802"), Name="Pakistan" },
                        new Country() { Id = Guid.Parse("d37bc149-2e11-4406-9ac4-14cddabc9081"), Name="USA" },
                        new Country() { Id = Guid.Parse("3c46c6f9-ba98-4f1e-aaa4-bf9440bc9b7c"), Name="Japan" },
                        new Country() { Id = Guid.Parse("7a0d3d05-e890-4ad0-a1c4-75efb1586d81"), Name="New Zealand" },
                        new Country() { Id = Guid.Parse("6a6db927-dc35-4249-af29-0d6edfe352e0"), Name="Canada" }
                    });
            }
        }

        public CountryResponse AddCountry(CountryAddRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(request.Name))
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
