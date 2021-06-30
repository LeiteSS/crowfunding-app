using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Vaquinha.App.Fixtures;
using Xunit;

namespace Vaquinha.Test.Domain
{
    [Collection(nameof(CreditCardFixtureCollection))]
    public class CreditCardTests : IClassFixture<CreditCardFixture>
    {
        private readonly CreditCardFixture _fixture;

        public CreditCardTests(CreditCardFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("CreditCard", "CreditCard_CorrectlyFilled_CreditCardValid")]
        public void CartaoCredito_CorretamentePreenchido_CartaoCreditoValido()
        {
            // Arrange
            var creditCard = _fixture.CreditCardValid();

            // Act
            var valid = creditCard.Valid();

            // Assert
            valid.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            creditCard.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        [Trait("CreditCard", "CreditCard_NoFieldFilled_CreditCardInvalid")]
        public void CreditCard_NoFieldFilled_CreditCardInvalid()
        {
            // Arrange
            var creditCard = _fixture.CreditCardEmpty();

            // Act
            var valid = creditCard.Valid();

            // Assert
            valid.Should().BeFalse(because: "deve possuir erros de preenchimento");
            creditCard.ErrorMessages.Should().HaveCount(4, because: "nenhum dos 4 campos obrigatórios foi informado ou estão incorretos.");

            creditCard.ErrorMessages.Should().Contain("O campo Nome Titular deve ser preenchido", because: "o campo Nome Titular é obrigatório e não foi preenchido.");
            creditCard.ErrorMessages.Should().Contain("O campo Número de cartão de crédito deve ser preenchido", because: "o campo Número de cartão de crédito é obrigatório e não foi preenchido.");
            creditCard.ErrorMessages.Should().Contain("O campo CVV deve ser preenchido", because: "o campo CVV é obrigatório e não foi preenchido.");
            creditCard.ErrorMessages.Should().Contain("O campo Validade deve ser preenchido", because: "o campo Data de Vencimento do cartão de crédito é obrigatório e não foi preenchido.");
        }

        [Fact]
        [Trait("CreditCard", "CreditCard_ValidityAndCVVInvalid_CreditCardInvalid")]
        public void CreditCard_ValidityAndCVVInvalid_CreditCardInvalid()
        {
            // Arrange
            var creditCard = _fixture.CreditCardNumberValidCvvInvalid();

            // Act
            var valid = creditCard.Valid();

            // Assert
            valid.Should().BeFalse(because: "deve possuir erros de validação");
            creditCard.ErrorMessages.Should().HaveCount(3, because: "nenhum dos 3 campos obrigatórios foi informado ou estão incorretos.");

            creditCard.ErrorMessages.Should().Contain("Campo Número de cartão de crédito inválido", because: "o campo Nome Titular é obrigatório e não foi preenchido.");
            creditCard.ErrorMessages.Should().Contain("Campo CVV inválido", because: "o campo Nome Titular é obrigatório e não foi preenchido.");
            creditCard.ErrorMessages.Should().Contain("Campo Data de Vencimento do cartão de crédito inválido", because: "o campo Nome Titular é obrigatório e não foi preenchido.");
        }

        [Fact]
        [Trait("CreditCard", "CreditCard_ValidityExpired_CreditCardInvalid")]
        public void CreditCard_ValidityExpired_CreditCardInvalid()
        {
            // Arrange
            var creditCard = _fixture.CreditCardValidityExpired();

            // Act
            var valid = creditCard.Valid();

            // Assert
            valid.Should().BeFalse(because: "deve possuir erros de validação");
            creditCard.ErrorMessages.Should().HaveCount(1, because: "data de vencimento expirada");

            creditCard.ErrorMessages.Should().Contain("Cartão de Crédito com data expirada", because: "o campo Data de Vencimento do cartão de crédito está expirado.");
        }

        [Fact]
        [Trait("CreditCard", "CreditCard_HolderNameMaxLength_CreditCardInvalid")]
        public void CreditCard_HolderNameMaxLength_CreditCardInvalid()
        {
            // Arrange
            var creditCard = _fixture.CreditCardHolderNameMaxLenght();

            // Act
            var valid = creditCard.Valid();

            // Assert
            valid.Should().BeFalse(because: "tamanho máximo de campos atingidos");
            creditCard.ErrorMessages.Should().HaveCount(1, because: "o preenchimento de 1 campo ultrapassou tamanho máximo permitido.");

            creditCard.ErrorMessages.Should().Contain("O campo Nome Titular deve possuir no máximo 150 caracteres", because: "o campo Nome Titular é obrigatório e não foi preenchido.");
        }
    }
}