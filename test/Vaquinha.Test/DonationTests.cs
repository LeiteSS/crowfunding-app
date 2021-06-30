using FluentAssertions;
using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
using System;
using Vaquinha.App.Factory;
using Vaquinha.App.Fixtures;
using Xunit;

namespace Vaquinha.App
{
    public class DonationsTests : IDisposable, IClassFixture<DonationFixture>, 
                                               IClassFixture<AddressFixture>, 
                                               IClassFixture<CreditCardFixture>
	{
		private DriverFactory _driverFactory = new DriverFactory();
		private IWebDriver _driver;

		private readonly DonationFixture _donationFixture;
		private readonly AddressFixture _addressFixture;
		private readonly CreditCardFixture _creditCardFixture;

		public DonationsTests(DonationFixture donationFixture, AddressFixture addressFixture, CreditCardFixture creditCardFixture)
        {
            _donationFixture = donationFixture;
            _addressFixture = addressFixture;
            _creditCardFixture = creditCardFixture;
        }
		public void Dispose()
		{
			_driverFactory.Close();
		}

		[Fact]
		public void DonationUI_AccessUserInterfaceHome()
		{
			// Arrange
			_driverFactory.NavigateToUrl("https://localhost:5001/");
			_driver = _driverFactory.GetWebDriver();

			// Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("vaquinha-logo"));

			// Assert
			webElement.Displayed.Should().BeTrue(because:"logo exibido");
		}
		[Fact]
		public void DonationUI_CreateDonation()
		{
			//Arrange
			var donation = _donationFixture.DonationValid();
            donation.AddBillingAddress(_addressFixture.AddressValid());
            donation.AddFormOfPayment(_creditCardFixture.CreditCardValid());
			_driverFactory.NavigateToUrl("https://localhost:5001/");
			_driver = _driverFactory.GetWebDriver();

			//Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			//Assert
			_driver.Url.Should().Contain("/Donations/Create");
		}
}