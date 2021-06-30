using FluentAssertions;
using Vaquinha.App.Fixtures;
using Xunit;

namespace Vaquinha.Test.Domain
{
    [Collection(nameof(PersonFixtureCollection))]
    public class PersonTests : IClassFixture<PersonFixture>
    {
        private readonly PersonFixture _fixture;

        public PersonTests(PersonFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("Person", "Person_CorrectlyFilled_PersonValid")]
        public void Person_CorrectlyFilled_PersonValid()
        {
            // Arrange
            var person = _fixture.PersonValid();
            
            // Act
            var valid = person.Valid();

            // Assert
            valid.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            person.ErrorMessages.Should().BeEmpty();            
        }

        [Fact]
        [Trait("Person", "Person_FieldEmpty_PersonInvalid")]
        public void Person_FieldEmpty_PersonInvalid()
        {
            // Arrange
            var person = _fixture.PersonEmpty();

            // Act
            var valid = person.Valid();

            // Assert
            valid.Should().BeFalse(because: "deve possuir erros de validação");

            person.ErrorMessages.Should().HaveCount(2, because: "nenhum dos 2 campos obrigatórios foi informado.");

            person.ErrorMessages.Should().Contain("O campo Nome é obrigatório.", because: "o campo Nome não foi informado.");
            person.ErrorMessages.Should().Contain("O campo Email é obrigatório.", because: "o campo Email não foi informado.");
        }

        [Fact]
        [Trait("Person", "Person_EmailInvalid_PersonInvalid")]
        public void Person_EmailInvalid_PersonInvalid()
        {
            // Arrange
            const bool EMAIL_INVALID = true;
            var person = _fixture.PersonValid(EMAIL_INVALID);

            // Act
            var valido = person.Valid();

            // Assert
            valido.Should().BeFalse(because: "o campo email está inválido");
            person.ErrorMessages.Should().HaveCount(1, because: "somente o campo email está inválido.");

            person.ErrorMessages.Should().Contain("O campo Email é inválido.");
        }

        [Fact]
        [Trait("Person", "Person_FieldMaxLenghtExceeded_PersonInvalid")]
        public void Person_FieldMaxLenghtExceeded_PersonInvalid()
        {
            // Arrange            
            var person = _fixture.PersonMaxLenth();

            // Act
            var valido = person.Valid();

            // Assert
            valido.Should().BeFalse(because: "os campos nome e email possuem mais caracteres do que o permitido.");
            person.ErrorMessages.Should().HaveCount(2, because: "os dados estão inválidos.");

            person.ErrorMessages.Should().Contain("O campo Nome deve possuir no máximo 150 caracteres.");
            person.ErrorMessages.Should().Contain("O campo Email deve possuir no máximo 150 caracteres.");
        }
    }
}