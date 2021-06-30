using Bogus;
using Vaquinha.App.Entities;
using Vaquinha.App.Models;
using Xunit;

namespace Vaquinha.App.Fixtures
{
    [CollectionDefinition(nameof(CreditCardFixtureCollection))]
    public class CreditCardFixtureCollection : ICollectionFixture<CreditCardFixture>
    {
    }

    public class CreditCardFixture
    {
        public CreditCardViewModel CreditCardModelValid()
        {
            var creditCard = new Faker().Finance;

            var faker = new Faker<CreditCardViewModel>("pt_BR");

            faker.RuleFor(c => c.CVV, (f, c) => creditCard.CreditCardCvv());
            faker.RuleFor(c => c.HolderName, (f, c) => f.Person.FullName);
            faker.RuleFor(c => c.CreditCardNumber, (f, c) => creditCard.CreditCardNumber());
            faker.RuleFor(c => c.Validity, (f, c) => "06/28");

            return faker.Generate();
        }

        public CreditCard CreditCardValid()
        {
            var creditCard = new Faker("pt_BR").Finance;
            var person = new Faker("pt_BR").Person;

            var faker = new Faker<CreditCard>("pt_BR");

            faker.CustomInstantiator(f =>
                 new CreditCard(person.FullName, creditCard.CreditCardNumber(), "06/28", creditCard.CreditCardCvv()));

            return faker.Generate();
        }

        public CreditCard CreditCardEmpty()
        {
            return new CreditCard(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public CreditCard CreditCardNumberValidCvvInvalid()
        {
            var creditCard = new Faker("pt_BR").Finance;
            var person = new Faker("pt_BR").Person;

            var faker = new Faker<CreditCard>("pt_BR");

            faker.CustomInstantiator(f =>
                 new CreditCard(person.FullName, "21125684", "14/25", "312q"));

            return faker.Generate();
        }

        public CreditCard CreditCardValidityExpired()
        {
            var creditCard = new Faker("pt_BR").Finance;
            var person = new Faker("pt_BR").Person;

            var faker = new Faker<CreditCard>("pt_BR");

            faker.CustomInstantiator(f =>
                 new CreditCard(person.FullName, creditCard.CreditCardNumber(), "06/19", creditCard.CreditCardCvv()));

            return faker.Generate();
        }

        public CreditCard CreditCardHolderNameMaxLenght()
        {
            const string MAX_LENGHT_HOLDER_NAME = "AHIUDHASHOIFJOASJPFPOKAPFOKPKQPOFKOPQKWPOFEMMVIMWPOVPOQWPMVPMQOPIPQMJEOIPFMOIQOIFMCOKQMEWVMOPMQEOMVOPMWQOEMVOWMEOMVOIQMOIVMQEHISUAHDUIHASIUHDIHASIUHDUIHIAUSHIDUHAIUSDQWMFMPEQPOGFMPWEMGVWEM";
            var creditCard = new Faker("pt_BR").Finance;

            var faker = new Faker<CreditCard>("pt_BR");

            faker.CustomInstantiator(f =>
                 new CreditCard(MAX_LENGHT_HOLDER_NAME, creditCard.CreditCardNumber(), "06/28", creditCard.CreditCardCvv()));

            return faker.Generate();
        }
    }
}