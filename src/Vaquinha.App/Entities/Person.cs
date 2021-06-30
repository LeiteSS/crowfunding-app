using System;
using System.Collections.Generic;
using FluentValidation;
using Vaquinha.App.Base;

namespace Vaquinha.App.Entities
{
    public class Person : Entity
    {
        private string _name;

        public string Name
        {
            get { return Anonymous ? Email : _name; }
            private set { _name = value; }
        }

        public bool Anonymous { get; private set; }
        public string Message { get; private set; }

        public string Email { get; private set; }
        public ICollection<Donation> Donations { get; set; }

        public Person() { }

        public Person(Guid id, string name, bool anonymous, string message, string email)
        {
            Id = id;
            _name = name;
            this.Anonymous = anonymous;
            this.Message = message;
            this.Email = email;
        }

        public override bool Valid()
        {
            ValidationResult = new PersonValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class PersonValidation : AbstractValidator<Person>
    {
        private const int MAX_LENTH_FIELDS = 150;
        private const int MAX_LENTH_MESSAGE = 500;

        public PersonValidation()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("O campo Nome é obrigatório.")
                .When(a => a.Anonymous == false)
                .MaximumLength(MAX_LENTH_FIELDS).WithMessage("O campo Nome deve possuir no máximo 150 caracteres.");

            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("O campo Email é obrigatório.")
                .MaximumLength(MAX_LENTH_FIELDS).WithMessage($"O campo Email deve possuir no máximo {MAX_LENTH_FIELDS} caracteres.");

            RuleFor(a => a.Email).EmailAddress()
                .When(a => !string.IsNullOrEmpty(a.Email))
                .When(a => a?.Email?.Length <= MAX_LENTH_FIELDS)
                .WithMessage("O campo Email é inválido.");

            RuleFor(a => a.Message)                
                .MaximumLength(MAX_LENTH_MESSAGE).WithMessage($"O campo Mensagem de Apoio deve possuir no máximo {MAX_LENTH_MESSAGE} caracteres.");
        }
    }
}