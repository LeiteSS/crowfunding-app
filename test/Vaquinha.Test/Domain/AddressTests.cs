using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Vaquinha.App.Fixtures;
using Xunit;

namespace Vaquinha.Test.Domain
{
    [Collection(nameof(AddressFixtureCollection))]
    public class AddressTests : IClassFixture<AddressFixture>
    {
        private readonly AddressFixture _fixture;

        public AddressTests(AddressFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("Address", "Address_CorrectlyFilled_AddressValid")]
        public void Address_CorrectlyFilled_AddressValid()
        {
            // Arrange
            var address = _fixture.AddressValid();

            // Act
            var valid = address.Valid();

            // Assert
            valid.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            address.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        [Trait("Address", "Address_NoFieldFilled_AddressInvalid")]
        public void Address_NoFieldFilled_AddressInvalid()
        {
            // Arrange
            var address = _fixture.AddressEmpty();

            // Act
            var valid = address.Valid();

            // Assert
            valid.Should().BeFalse(because: "deve possuir erros de preenchimento");
            address.ErrorMessages.Should().HaveCount(6, because: "nenhum dos 6 campos obrigatórios foi informado ou estão incorretos.");

            address.ErrorMessages.Should().Contain("O campo Cidade deve ser preenchido", because: "o campo Cidade é obrigatório e não foi preenchido.");
            address.ErrorMessages.Should().Contain("O campo Endereço deve ser preenchido", because: "o campo Endereço é obrigatório e não foi preenchido.");
            address.ErrorMessages.Should().Contain("O campo CEP deve ser preenchido", because: "o campo CEP é obrigatório e não foi preenchido.");
            address.ErrorMessages.Should().Contain("Campo Estado inválido", because: "o campo Estado não foi preenchido c omnforme o esperado.");
            address.ErrorMessages.Should().Contain("O campo Telefone deve ser preenchido", because: "o campo Telefone não foi preenchido comnforme o esperado.");
            address.ErrorMessages.Should().Contain("O campo Número deve ser preenchido", because: "o campo Número não foi preenchido comnforme o esperado.");
        }

        [Fact]
        [Trait("Address", "Address_ZipCodePhoneStateInvalid_AddressInvalid")]
        public void Address_ZipCodePhoneStateInvalid_AddressInvalid()
        {
            // Arrange
            var address = _fixture.AddressZipCodePhoneStateInvalid();

            // Act
            var valid = address.Valid();

            // Assert
            valid.Should().BeFalse(because: "deve possuir erros de validação");
            address.ErrorMessages.Should().HaveCount(3, because: "o preenchimento de 3 campos não foi feito conforme o esperado.");
            
            address.ErrorMessages.Should().Contain("Campo CEP inválido", because: "o campo CEP não foi preenchido comnforme o esperado.");
            address.ErrorMessages.Should().Contain("Campo Estado inválido", because: "o campo Estado não foi preenchido comnforme o esperado.");
            address.ErrorMessages.Should().Contain("Campo Telefone inválido", because: "o campo Telefone não foi preenchido comnforme o esperado.");            
        }

        [Fact]
        [Trait("Address", "Address_AddressCityComplementMaxLength_AddressInvalid")]
        public void Address_AddressCityComplementMaxLength_AddressInvalid()
        {
            // Arrange
            var address = _fixture.AddressMaxLength();

            // Act
            var valid = address.Valid();

            // Assert
            valid.Should().BeFalse(because: "tamanho máximo de campos atingidos");
            address.ErrorMessages.Should().HaveCount(4, because: "o preenchimento de 4 campos ultrapassou tamanho máximo permitido.");

            address.ErrorMessages.Should().Contain("O campo Endereço deve possuir no máximo 250 caracteres", because: "o campo Endereço ultrapassou tamanho máximo permitido.");
            address.ErrorMessages.Should().Contain("O campo Cidade deve possuir no máximo 150 caracteres", because: "o campo Cidade ultrapassou tamanho máximo permitido.");
            address.ErrorMessages.Should().Contain("O campo Complemento deve possuir no máximo 250 caracteres", because: "o campo Complemento ultrapassou tamanho máximo permitido.");
            address.ErrorMessages.Should().Contain("O campo Número deve possuir no máximo 6 caracteres", because: "o campo Complemento ultrapassou tamanho máximo permitido.");
        }
    }
}