namespace Vaquinha.App.Models
{
    public class DonationViewModel
    {
        public decimal Value { get; set; }
        public PersonViewModel PersonalData { get; set; }
        public AddressViewModel BillingAddress { get; set; }
        public CreditCardViewModel FormOfPayment { get; set; }
    }
}