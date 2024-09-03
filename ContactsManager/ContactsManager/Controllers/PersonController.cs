using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.Dtos;
using ServiceContracts.Enums;

namespace ContactsManager.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [Route("/")]
        [Route("persons")]
        public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(PersonResponse.Name), SortOptions sortOrder = SortOptions.Ascending)
        {
            ViewBag.SearchBy = searchBy;
            ViewBag.SearchString = searchString;
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;

            ViewBag.SearchByFields = new Dictionary<string, string>()
            {
                { nameof(PersonResponse.Name), "Person Name" },
                { nameof(PersonResponse.Email), "Email" },
                { nameof(PersonResponse.DateOfBirth), "Date of Birth" },
                { nameof(PersonResponse.Address), "Address" },
                { nameof(PersonResponse.Gender), "Gender" },
                { nameof(PersonResponse.Country), "Country" },
                { nameof(PersonResponse.ReceiveNewsLetters), "Receive Newsletters" },
            };

            var persons = _personService.GetFilteredPersons(searchBy, searchString);

            persons = _personService.GetSortedPersons(persons, sortBy, sortOrder);
            return View(persons);
        }
    }
}
