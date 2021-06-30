using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vaquinha.App.Interfaces.Service;

namespace Vaquinha.App.Controllers
{
    public class DonorsController : Controller
    {
        private readonly IDonationService _donationService;

        public DonorsController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _donationService.RestoreDonorsAsync());
        }
    }
}