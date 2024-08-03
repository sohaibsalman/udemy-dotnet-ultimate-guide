using System.ComponentModel.DataAnnotations;

namespace Services.Helpers
{
  public class ValidationHelper
  {
    internal static void ModelValidation<T>(T model)
    {
      ValidationContext context = new ValidationContext(model);
      List<ValidationResult> validationResult = [];
      bool isValid = Validator.TryValidateObject(model, context, validationResult, true);

      if(!isValid)
      {
        throw new ArgumentException(validationResult.FirstOrDefault()?.ErrorMessage);
      }
    }
  }
}
