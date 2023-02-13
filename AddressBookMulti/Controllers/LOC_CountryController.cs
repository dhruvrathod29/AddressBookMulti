using Microsoft.AspNetCore.Mvc;

namespace AddressBookMulti.Controllers
{
    public class LOC_CountryController : Controller
    {
        public IActionResult Index()
        {
            return View("LOC_CountryAddEdit");
        }
    }
}
