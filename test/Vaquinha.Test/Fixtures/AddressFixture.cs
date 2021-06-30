using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Text;
using Vaquinha.App.Models;
using Xunit;

namespace Vaquinha.App.Fixtures
{
    [CollectionDefinition(nameof(AddressFixtureCollection))]
    public class AddressFixtureCollection : ICollectionFixture<AddressFixture>
    {
    }

    public class AddressFixture
    {
        public AddressViewModel AddressModelValid()
        {
            var address = new Faker().Address;

            var faker = new Faker<AddressViewModel>("pt_BR");

            faker.RuleFor(c => c.ZipCode, (f, c) => "14800-700");
            faker.RuleFor(c => c.City, (f, c) => address.City());
            faker.RuleFor(c => c.State, (f, c) => address.StateAbbr());
            faker.RuleFor(c => c.Address, (f, c) => address.StreetAddress());            

            return faker.Generate();
        }

        public Address AddressValid()
        {
            var address = new Faker("pt_BR").Address;
            
            var faker = new Faker<Address>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Address(Guid.NewGuid(), "14800-000", address.StreetAddress(false), string.Empty, address.City(), address.StateAbbr(), "16995811385", "100A"));

            return faker.Generate();
        }

        public Address AddressEmpty()
        {
            return new Address(Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public Address AddressZipCodePhoneStateInvalid()
        {
            var address = new Faker("pt_BR").Address;

            var faker = new Faker<Address>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Address(Guid.NewGuid(), "14800-0000", address.StreetAddress(false), string.Empty, address.City(), address.State(), "169958113859", "2005"));

            return faker.Generate();
        }

        public Address AddressMaxLength()
        {
            const string MAX_LENGTH_ADDRESS = "AHIUDHASHOIFJOASJPFPOKAPFOKPKQPOFKOPQKWPOFEMMVIMWPOVPOQWPMVPMQOPIPQMJEOIPFMOIQOIFMCOKQMEWVMOPMQEOMVOPMWQOEMVOWMEOMVOIQMOIVMQEHISUAHDUIHASIUHDIHASIUHDUIHIAUSHIDUHAIUSDQWMFMPEQPOGFMPWEMGVWEM CQPWEM,CPQWPMCEOWIMVOEWOINMMFWOIEMFOIMOIOWEMFOIEWMFOIWEMFOWEOIMF";

            var address = new Faker("pt_BR").Address;

            var faker = new Faker<Address>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Address(Guid.NewGuid(), "14800-000", MAX_LENGTH_ADDRESS, MAX_LENGTH_ADDRESS, MAX_LENGTH_ADDRESS, address.StateAbbr(), "16995811385", "1234567"));

            return faker.Generate();
        }
    }
}