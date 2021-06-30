using FluentValidation;
using System;
using System.Globalization;
using Vaquinha.App.Base;

namespace Vaquinha.App.Entities
{
    public class CreditCard : Entity
    {
        public string HolderName { get; set; }
        public string CreditCardNumber { get; set; }
        public string Validity { get; set; }
        public string CVV { get; set; }

        private CreditCard() { }

        public CreditCard(string holderName, string number, string validity, string cvv)
        {
            HolderName = holderName;
            CreditCardNumber = number;
            Validity = validity;
            CVV = cvv;
        }



        public override bool Valid()
        {
            ValidationResult = new CreditCardValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CreditCardValidation : AbstractValidator<CreditCard>
    {
        private const int MAX_LENTH_FIELDS = 150;

        public CreditCardValidation()
        {
            RuleFor(o => o.HolderName)
              .NotEmpty().WithMessage("O campo Nome Titular deve ser preenchido")
              .MaximumLength(MAX_LENTH_FIELDS).WithMessage($"O campo Nome Titular deve possuir no máximo {MAX_LENTH_FIELDS} caracteres");

            RuleFor(o => o.CreditCardNumber)
                .NotEmpty().WithMessage("O campo Número de cartão de crédito deve ser preenchido")
                .CreditCard().WithMessage("Campo Número de cartão de crédito inválido");

            RuleFor(o => o.CVV)
                .NotEmpty().WithMessage("O campo CVV deve ser preenchido")
                .Must(ValidateCVV)
                .WithMessage("Campo CVV inválido");

            RuleFor(o => o.Validity)
                .NotEmpty().WithMessage("O campo Validade deve ser preenchido")
                .Must(v => ValidateValidity(v, out _)).WithMessage("Campo Data de Vencimento do cartão de crédito inválido")
                .Must(v => ValidateValidityDate(v)).WithMessage("Cartão de Crédito com data expirada");
        }

        private bool ValidateCVV(string cvv)
        {
            if (string.IsNullOrEmpty(cvv)) return true;

            return cvv.Length >= 3 && cvv.Length <= 4 && int.TryParse(cvv, out _);
        }

        private bool ValidateValidity(string validity, out DateTime? date)
        {
            date = null;

            if (string.IsNullOrEmpty(validity)) return true;

            string[] monthYear = validity.Split("/");

            if (monthYear.Length == 2)
            {
                if (monthYear[0].Length <= 2 && monthYear[1].Length <= 4)
                {
                    int month, year;
                    if (int.TryParse(monthYear[0], out month) && int.TryParse(monthYear[1], out year))
                    {
                        year = CultureInfo.CurrentCulture.Calendar.ToFourDigitYear(year);
                        if (month >= 1 && month <= 12 && year >= 2000 && year <= 2099)
                        {
                            date = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool ValidateValidityDate(string validity)
        {
            if (ValidateValidity(validity, out DateTime? validityDate) && validityDate != null)
            {
                return DateTime.Now.Date <= ((DateTime)validityDate).Date;
            }

            return true;
        }
    }
}