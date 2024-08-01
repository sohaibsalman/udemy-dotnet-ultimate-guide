using Entities;

namespace ServiceContracts.Dtos
{
  public class CountryResponse
  {
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public override bool Equals(object? obj)
    {
      if(obj == null) return false;

      if(obj.GetType() != typeof(CountryResponse)) return false;

      CountryResponse response = (CountryResponse)obj;
      return response.Id == this.Id && response.Name == this.Name;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }

  public static class CountryExtensions
  {
    public static CountryResponse ToCountryResponse(this Country country)
    {
      return new CountryResponse { Id = country.Id, Name = country.Name };
    }
  }
}
