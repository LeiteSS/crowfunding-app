using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using NToastNotify;
using Vaquinha.App.Config;
using Vaquinha.App.Entities;
using Vaquinha.App.Fixtures;
using Vaquinha.App.Interfaces.Payment;
using Vaquinha.App.Interfaces.Repository;
using Vaquinha.App.Interfaces.Service;
using Vaquinha.App.Models;
using Vaquinha.App.Service;
using Xunit;
using static Vaquinha.App.Fixtures.DonationFixture;

namespace Vaquinha.App.Controllers
{
    [Collection(nameof(DonationFixtureCollection))]
    public class DonationControllerTests : IClassFixture<DonationFixture>,
                                        IClassFixture<AddressFixture>,
                                        IClassFixture<CreditCardFixture>
    {
        private readonly Mock<IDonationRepository> _donationRepository = new Mock<IDonationRepository>();        
        private readonly Mock<GlobalAppConfig> _globallAppConfig = new Mock<GlobalAppConfig>();

        private readonly DonationFixture _donationFixture;
        private readonly AddressFixture _addressFixture;
        private readonly CreditCardFixture _creditCardFixture;

        private DonationsController _donationsController;
        private readonly IDonationService _donationService;

        private Mock<IMapper> _mapper;
        private Mock<IPaymentService> _polenService = new Mock<IPaymentService>();
        private Mock<ILogger<DonationsController>> _logger = new Mock<ILogger<DonationsController>>();

        private IDomainNotificationService _domainNotificationService = new DomainNotificationService();

        private Mock<IToastNotification> _toastNotification = new Mock<IToastNotification>();

        private readonly Donation _donationValid;
        private readonly DonationViewModel _donationModelValid;

        public DoacaoControllerTests(DonationFixture donationFixture, AddressFixture addressFixture, CreditCardFixture creditCardFixture)
        {
            _donationFixture = donationFixture;
            _addressFixture = addressFixture;
            _creditCardFixture = creditCardFixture;

            _mapper = new Mock<IMapper>();

            _donationValid = donationFixture.DonationValid();
            _donationValid.AddBillingAddress(addressFixture.AddressValid());
            _donationValid.AddFormOfPayment(creditCardFixture.CreditCardValid());

            _donationModelValid = donationFixture.DonationModelValid();
            _donationModelValid.BillingAddress = addressFixture.AddressModelValid();
            _donationModelValid.FormOfPayment = creditCardFixture.CreditCardModelValid();

            _mapper.Setup(a => a.Map<DonationViewModel, Donation>(_donationModelValid)).Returns(_donationValid);

            _donationService = new DonationService(_mapper.Object, _donationRepository.Object, _domainNotificationService);
        }

        #region HTTPPOST

        [Trait("DonationController", "DonationController_Add_ReturnSuccess")]
        [Fact]
        public void DonationController_Add_ReturnSuccess()
        {
            // Arrange            
            _donationsController = new DonationsController(_donationService, _domainNotificationService, _toastNotification.Object);

            // Act
            var output = _donationsController.Create(_donationModelValid);

            _mapper.Verify(a => a.Map<DonationViewModel, Donation>(_donationModelValid), Times.Once);
            _toastNotification.Verify(a => a.AddSuccessToastMessage(It.IsAny<string>(), It.IsAny<LibraryOptions>()), Times.Once);

            output.Should().BeOfType<RedirectToActionResult>();

            ((RedirectToActionResult)output).ActionName.Should().Be("Index");
            ((RedirectToActionResult)output).ControllerName.Should().Be("Home");
        }

        [Trait("DonationController", "DonationController_AddInvalidData_BadRequest")]
        [Fact]
        public void DonationController_AddInvalidData_BadRequest()
        {
            // Arrange          
            var donation = _donationFixture.DonationInvalid();
            var donationModelInvalid = new DonationViewModel();
            _mapper.Setup(a => a.Map<DonationViewModel, Donation>(donationModelInvalid)).Returns(donation);

            _donationsController = new DonationsController(_donationService, _domainNotificationService, _toastNotification.Object);

            // Act
            var output = _donationsController.Create(donationModelInvalid);

            // Assert                   
            output.Should().BeOfType<ViewResult>();

            _mapper.Verify(a => a.Map<DonationViewModel, Donation>(donationModelInvalid), Times.Once);
            _donationRepository.Verify(a => a.AddAsync(donation), Times.Never);
            _toastNotification.Verify(a => a.AddErrorToastMessage(It.IsAny<string>(), It.IsAny<LibraryOptions>()), Times.Once);

            var viewResult = ((ViewResult)output);

            viewResult.Model.Should().BeOfType<DonationViewModel>();

            ((DonationViewModel)viewResult.Model).Should().Be(donationModelInvalid);
        }

        #endregion
    }
}