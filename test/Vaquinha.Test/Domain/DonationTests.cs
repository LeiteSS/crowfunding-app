using Xunit;
using FluentAssertions;
using static Vaquinha.App.Fixtures.DonationFixture;
using Vaquinha.App.Fixtures;

namespace Vaquinha.Test.Domain
{
    [Collection(nameof(DonationFixtureCollection))] 
    public class DonationTests : IClassFixture<DonationFixture>, 
                              IClassFixture<AddressFixture>, 
                              IClassFixture<CreditCardFixture>
    {
        private readonly DonationFixture _donationFixture;
        private readonly AddressFixture _addressFixture;
        private readonly CreditCardFixture _creditCardFixture;

        public DonationTests(DonationFixture donationFixture, AddressFixture addressFixture, CreditCardFixture creditCardFixture)
        {
            _donationFixture = donationFixture;
            _addressFixture = addressFixture;
            _creditCardFixture = creditCardFixture;
        }

        [Fact]
        [Trait("Donation", "Donation_CorrectlyFilled_DonationValid")]
        public void Donation_CorrectlyFilled_DonationValid()
        {           
            // Arrange
            var donation = _donationFixture.DonationValid();
            donation.AddBillingAddress(_addressFixture.AddressValid());
            donation.AddFormOfPayment(_creditCardFixture.CreditCardValid());

            // Act
            var valid = donation.Valid();

            // Assert
            valid.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            donation.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        [Trait("Donation", "Donation_PersonalDataInvalid_DonationInvalid")]
        public void Donation_PersonalDataInvalid_DonationInvalid()
        {
            // Arrange
            const bool EMAIL_INVALID = true;
            var donation = _donationFixture.DonationValid(EMAIL_INVALID);
            donation.AddBillingAddress(_addressFixture.AddressValid());
            donation.AddFormOfPayment(_creditCardFixture.CreditCardValid());

            // Act
            var valid = donation.Valid();

            // Assert
            valid.Should().BeFalse(because: "o campo email está inválido");
            donation.ErrorMessages.Should().Contain("O campo Email é inválido.");
            donation.ErrorMessages.Should().HaveCount(1, because: "somente o campo email está inválido.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(-5)]
        [InlineData(-10.20)]
        [InlineData(-55.4)]
        [InlineData(-0.1)]
        [Trait("Donation", "Donation_ValuesLessOrEqualThanZero_DonationInvalid")]
        public void Donation_ValuesLessOrEqualThanZero_DonationInvalid(double donationValue)
        {
            // Arrange            
            var donation = _donationFixture.DonationValid(false, donationValue);
            donation.AddBillingAddress(_addressFixture.AddressValid());
            donation.AddFormOfPayment(_creditCardFixture.CreditCardValid());

            // Act
            var valid = donation.Valid();

            // Assert
            valid.Should().BeFalse(because: "o campo Valor está inválido");
            donation.ErrorMessages.Should().Contain("Valor mínimo de doação é de R$ 5,00");
            donation.ErrorMessages.Should().HaveCount(1, because: "somente o campo Valor está inválido.");
        }

        [Theory]
        [InlineData(25000)]
        [InlineData(5500.50)]
        [InlineData(5000.1)]
        [InlineData(4505)]
        [InlineData(4500.1)]
        [Trait("Donation", "Donation_ValuesMoreThanTheLimit_DonationInvalid")]
        public void Donation_ValuesMoreThanTheLimit_DonationInvalid(double donationValue)
        {
            // Arrange
            const bool MAX_VALUE_EXCEDEED = true;
            var donation = _donationFixture.DonationValid(false, donationValue);
            donation.AddBillingAddress(_addressFixture.AddressValid());
            donation.AddFormOfPayment(_creditCardFixture.CreditCardValid());

            // Act
            var valid = donation.Valid();

            // Assert
            valid.Should().BeFalse(because: "o campo Valor está inválido");
            donation.ErrorMessages.Should().Contain("Valor máximo para a doação é de R$4.500,00");
            donation.ErrorMessages.Should().HaveCount(1, because: "somente o campo Valor está inválido.");
        }

        [Fact]
        [Trait("Doacao", "Doacao_MensagemApoioMaxlenghtExecido_DoacaoInvalida")]
        public void Doacao_MensagemApoioMaxlenghtExecido_DoacaoInvalida()
        {
            // Arrange
            const bool MESSAGE_MAX_LENGTH_EXCEDEED = true;
            var donation = _donationFixture.DonationValid(false, null, MESSAGE_MAX_LENGTH_EXCEDEED);
            donation.AddBillingAddress(_addressFixture.AddressValid());
            donation.AddFormOfPayment(_creditCardFixture.CreditCardValid());

            // Act
            var valid = donation.Valid();

            // Assert
            valid.Should().BeFalse(because: "O campo Mensagem de Apoio possui mais caracteres do que o permitido");
            donation.ErrorMessages.Should().HaveCount(1, because: "somente o campo Mensagem deApoio está inválido.");
            donation.ErrorMessages.Should().Contain("O campo Mensagem de Apoio deve possuir no máximo 500 caracteres.");
        }

        [Fact]
        [Trait("Donation", "Donation_DataNotInformed_DonationInvalid")]
        public void Donation_DataNotInformed_DonationInvalid()
        {
            // Arrange
            var donation = _donationFixture.DonationInvalid(false);
            donation.AddBillingAddress(_addressFixture.AddressValid());
            donation.AddFormOfPayment(_creditCardFixture.CreditCardValid());

            // Act
            var valid = donation.Valid();

            // Assert
            valid.Should().BeFalse(because: "os campos da doação nao foram informados");

            donation.ErrorMessages.Should().HaveCount(3, because: "Os 3 campos obrigatórios da doação não foram preenchidos");

            donation.ErrorMessages.Should().Contain("Valor mínimo de doação é de R$ 5,00", because: "valor mínimo de doação nao foi atingido.");
            donation.ErrorMessages.Should().Contain("O campo Nome é obrigatório.", because: "o campo Nome não foi informado.");
            donation.ErrorMessages.Should().Contain("O campo Email é obrigatório.", because: "o campo Email não foi informado.");            
        }

        [Fact]
        [Trait("Donation", "Donation_DataNotInformedAnonymousDonation_DonationInvalid")]
        public void Donation_DataNotInformedAnonymousDonation_DonationInvalid()
        {
            // Arrange
            var donation = _donationFixture.DonationInvalid(true);
            donation.AddBillingAddress(_addressFixture.AddressValid());
            donation.AddFormOfPayment(_creditCardFixture.CreditCardValid());

            // Act
            var valid = donation.Valid();

            // Assert
            valid.Should().BeFalse(because: "os campos da doação nao foram informados");

            donation.ErrorMessages.Should().HaveCount(2, because: "Os 2 campos obrigatórios da doação não foram preenchidos");

            donation.ErrorMessages.Should().Contain("Valor mínimo de doação é de R$ 5,00", because: "valor mínimo de doação nao foi atingido.");            
            donation.ErrorMessages.Should().Contain("O campo Email é obrigatório.", because: "o campo Email não foi informado.");            
        }
    }
}