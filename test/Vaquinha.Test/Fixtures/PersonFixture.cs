using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using Xunit;

namespace Vaquinha.App.Fixtures
{
    [CollectionDefinition(nameof(PersonFixtureCollection))]
    public class PersonFixtureCollection : ICollectionFixture<PersonFixture>
    {
    }
    public class PersonFixture
    {
        public Person PersonModelValid(bool emailInvalid = false)
        {
            var genre = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<Person>("pt_BR");

            faker.RuleFor(c => c.Message, (f, c) => f.Lorem.Sentence(30));
            faker.RuleFor(c => c.Name, (f, c) => f.Name.FirstName(genre));
            faker.RuleFor(c => c.Email, (f, c) => emailInvalid ? "EMAIL_INVALIDO" : f.Internet.Email(c.Name.ToLower(), c.Name.ToLower()));

            return faker.Generate();
        }

        public IEnumerable<Person> PersonModelValid(int qtd, bool emailInvalid = false)
        {
            var genre = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<Person>("pt_BR");

            faker.RuleFor(c => c.Name, (f, c) => f.Name.FirstName(genre));
            faker.RuleFor(c => c.Email, (f, c) => emailInvalid ? "EMAIL_INVALIDO" : f.Internet.Email(c.Name.ToLower(), c.Name.ToLower()));

            return faker.Generate(qtd);
        }

        public IEnumerable<Person> PersonValid(int qtd, bool emailInvalid = false, bool maxLenghField = false)
        {
            var genre = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<Person>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Person(Guid.NewGuid(), f.Name.FirstName(genre), string.Empty, false, maxLenghField ? f.Lorem.Sentence(300) : f.Lorem.Sentence(30)))
                .RuleFor(c => c.Email, (f, c) => emailInvalid ? "EMAIL_INVALIDO" : f.Internet.Email(c.Name.ToLower(), c.Name.ToLower()));

            return faker.Generate(qtd);
        }

        public Person PersonValid(bool emailInvalido = false, bool maxLenghField = false)
        {
            var genre = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<Person>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Person(Guid.NewGuid(), f.Name.FirstName(genre), string.Empty, false, maxLenghField ? f.Lorem.Sentence(300) : f.Lorem.Sentence(30)))
                .RuleFor(c => c.Email, (f, c) => emailInvalido ? "EMAIL_INVALIDO" : f.Internet.Email(c.Name.ToLower(), c.Name.ToLower()));

            return faker.Generate();
        }

        public Person PersonEmpty()
        {
            return new Person(Guid.Empty, string.Empty, string.Empty, false, string.Empty);
        }

        public Person PersonMaxLenth()
        {
            const string MAX_LENGHT_FIELDS = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            return new Person(Guid.NewGuid(), MAX_LENGHT_FIELDS, MAX_LENGHT_FIELDS, false, "AA");
        }
    }
}