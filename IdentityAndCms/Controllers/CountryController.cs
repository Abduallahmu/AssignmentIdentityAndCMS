using IdentityAndCms.CMS;
using IdentityAndCms.Interface;
using IdentityAndCms.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace IdentityAndCms.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        public CountryController(ICountryService countryService, ICityService cityService)
        {
            _countryService = countryService;
            _cityService = cityService;
        }

        public IActionResult Index()
        {
            return View(_countryService.AllCountry());
        }

        public IActionResult CreateCountry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCountry(Country country)
        {
            if (ModelState.IsValid)
            {
                country = _countryService.CreateCountry(country.Name);
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        public IActionResult CreateCity(int countryId)
        {
            var vm = new CityVM
            {
                CountryId = countryId
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCity(City city, int countryId)
        {
            if (ModelState.IsValid)
            {
                _cityService.CreateCity(city, countryId);
                return RedirectToAction(nameof(Details), "Country", new { id = countryId });
            }
            return View(city);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = _countryService.FindCountry((int)id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                _countryService.UpdateCountry(country);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                _countryService.DeleteCountry((int)id);

                _cityService.DeleteCity((int)id);

                return RedirectToAction(nameof(Index));
            }
            return Content("");
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = _countryService.FindCountry((int)id);

            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }
    }
}