using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Threading.Tasks;
using Vaquinha.App.Interfaces.Service;
using Vaquinha.App.Models;

namespace Vaquinha.App.Controllers
{
    public class DonationsController : BaseController
    {
        private readonly IDonationService _donationService;
        private readonly IDomainNotificationService _domainNotificationService;

        public DonationsController(IDonationService donationService,
                                 IDomainNotificationService domainNotificationService,
                                 IToastNotification toastNotification) : base(domainNotificationService, toastNotification)
        {
            _donationService = donationService;
            _domainNotificationService = domainNotificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(nameof(Index), await _donationService.RestoreDonorsAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DonationViewModel model)
        {
            _donationService.AccomplishDonationAsync(model);

            if (HasDomainErrors())
            {
                AddDomainError();
                return View(model);
            }

            AddNotificationOperationAccomplishWithSuccess("Doação realizada com sucesso!<p>Obrigado por apoiar nossa causa :)</p>");
            return RedirectToAction("Index", "Home");
        }

        private bool HasDomainErrors()
        {
            return _domainNotificationService.HasErrors;
        }
    }
}