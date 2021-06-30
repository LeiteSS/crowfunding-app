using System;
using FluentValidation;
using Vaquinha.App.Base;

namespace Vaquinha.App.Entities
{
    public class Donation : Entity
    {
        public double Value { get; private set; }

        public Guid PersonalDataId { get; private set; }
        public Guid BillingAddressId { get; private set; }

        public DateTime DateAndTime { get; private set; }

        public Person PersonalData { get; private set; }
        public Address BillingAddress { get; private set; }
        public CreditCard FormOfPayment { get; private set; }

        public Donation() { }
        public Donation(double value, Guid personalDataId, Guid billingAddressId, DateTime dateAndTime, Person personalData, Address billingAddress, CreditCard formOfPayment)
        {
            this.Value = value;
            this.PersonalDataId = personalDataId;
            this.BillingAddressId = billingAddressId;
            this.DateAndTime = dateAndTime;
            this.PersonalData = personalData;
            this.BillingAddress = billingAddress;
            this.FormOfPayment = formOfPayment;

        }

        public void UpdateDateAndTime()
        {
            DateAndTime = DateTime.Now;
        }

        public void AddPerson(Person person)
        {
            PersonalData = person;
        }

        public void AddBillingAddress(Address address) 
        {
            BillingAddress = address;
        }
        public void AddFormOfPayment(CreditCard formOfPayment) {
            FormOfPayment = formOfPayment;
        }
        public override bool Valid()
        {
            ValidationResult = new DonationValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class DonationValidation : AbstractValidator<Donation>
    {
        public DonationValidation()
        {
            RuleFor(a => a.Value)
                .GreaterThanOrEqualTo(5).WithMessage("Valor mínimo de doação é de R$ 5,00")
                .LessThanOrEqualTo(4500).WithMessage("Valor máximo para a doação é de R$4.500,00");

            RuleFor(a => a.PersonalData).NotNull().WithMessage("Os Dados Pessoais são obrigatórios").SetValidator(new PersonValidation());
            RuleFor(a => a.BillingAddress).NotNull().WithMessage("O Endereço de Cobrança é obtigatório.").SetValidator(new AddressValidation());
            RuleFor(a => a.FormOfPayment).NotNull().WithMessage("A Forma de Pagamento é obtigatória.").SetValidator(new CreditCardValidation());
        }
    }
}