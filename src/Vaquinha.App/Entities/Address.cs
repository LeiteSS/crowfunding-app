using FluentValidation;
using System;
using System.Collections.Generic;
using Vaquinha.App.Base;

namespace Vaquinha.App.Entities
{
    public class Address : Entity
    {
        public string ZipCode { get; private set; }
        public string AddressText { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Phone { get; private set; }

        public Address() { }

        public Address (Guid id, string zipCode, string addressText, string complement, string city, string state, string phone, string number)
        {
            Id = id;
            ZipCode = zipCode;
            AddressText = addressText;
            Complement = complement;
            City = city;
            State = state;
            Phone = phone;
            Number = number;
        }

        public ICollection<Donation> Donations { get; set; }

        public override bool Valid()
        {
            ValidationResult = new AddressValidation().Validate( this);
            return ValidationResult.IsValid;
        }
    }

    public class AddressValidation : AbstractValidator<Address>
    {
        private const int MAX_LENGHT_ADDRESS = 250;
        private const int MAX_LENGHT_COMPLEMENT = 250;
        private const int MAX_LENGHT_CITY = 150;
        private const int MAX_LENGHT_NUMBER = 6;

        public AddressValidation()
        {
            RuleFor(o => o.ZipCode)
                .NotEmpty().WithMessage("O campo CEP deve ser preenchido")
                .Must(ValidateZipCode).WithMessage("Campo CEP inválido");

            RuleFor(o => o.AddressText)
                .NotEmpty().WithMessage("O campo Endereço deve ser preenchido")
                .MaximumLength(MAX_LENGHT_ADDRESS).WithMessage($"O campo Endereço deve possuir no máximo {MAX_LENGHT_ADDRESS} caracteres");

            RuleFor(o => o.Number)
                .NotEmpty().WithMessage("O campo Número deve ser preenchido")
                .MaximumLength(MAX_LENGHT_NUMBER).WithMessage($"O campo Número deve possuir no máximo {MAX_LENGHT_NUMBER} caracteres");

            RuleFor(o => o.City)
                .NotEmpty().WithMessage("O campo Cidade deve ser preenchido")
                .MaximumLength(MAX_LENGHT_CITY).WithMessage($"O campo Cidade deve possuir no máximo {MAX_LENGHT_CITY} caracteres");

            RuleFor(o => o.State)                
                .Length(2).WithMessage("Campo Estado inválido");

            RuleFor(o => o.Phone)
                .NotEmpty().WithMessage("O campo Telefone deve ser preenchido")
                .Must(ValidadePhone).WithMessage("Campo Telefone inválido");

            RuleFor(o => o.Complement)
                .MaximumLength(MAX_LENGHT_COMPLEMENT).WithMessage($"O campo Complemento deve possuir no máximo {MAX_LENGHT_COMPLEMENT} caracteres");
        }

        private bool ValidateZipCode(string zipCode)
        {
            if (string.IsNullOrEmpty(zipCode)) return true;

            return zipCode.Replace(".", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).Length == 8;
        }

        private bool ValidadePhone(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return true;

            var tamanho = phone.Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty)
                .Replace("-", string.Empty).Length;

            return tamanho >= 10 && tamanho <= 11;
        }
    }
}