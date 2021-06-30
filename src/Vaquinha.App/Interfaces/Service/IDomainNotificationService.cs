using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Vaquinha.App.Base;
using Vaquinha.App.Entities;

namespace Vaquinha.App.Interfaces.Service
{
    public interface IDomainNotificationService
    {
        bool HasErrors { get; }
        IEnumerable<DomainNotification> RestoreDomainErrors();
        string RestoreDomainErrorsInHtmlFormat();

        void Add<T>(T entity) where T : Entity;

        void AdicionarErroDominio(ValidationResult validationResult);
        void Add(DomainNotification domainNotification);
    }

    public class DomainNotificationService : IDomainNotificationService
    {
        private readonly List<DomainNotification> _notifications;

        public bool HasErrors => _notifications.Any();

        public DomainNotificationService()
        {
            _notifications = new List<DomainNotification>();
        }

        public void Add<T>(T entity) where T : Entity
        {
            var notifications = entity.ValidationResult.Errors.Select(a => new DomainNotification(a.ErrorCode, a.ErrorMessage));
            _notifications.AddRange(notifications);
        }

        public void Add(DomainNotification domainNotification)
        {
            _notifications.Add(domainNotification);
        }

        public void AdicionarErroDominio(ValidationResult validationResult)
        {
            if (validationResult == null) return;

            var notifications = validationResult.Errors.Select(a => new DomainNotification(a.ErrorCode, a.ErrorMessage));
            _notifications.AddRange(notifications);
        }

        public IEnumerable<DomainNotification> RestoreDomainErrors()
        {
            return _notifications;
        }

        public string RestoreDomainErrorsInHtmlFormat()
        {
            var errors = string.Join("", RestoreDomainErrors().Select(a => $"<li>{a.ErrorMessage}</li>").ToArray());
            return $"<ul>{errors}</ul>";
        }

        private static bool PropertiesErrorsExists(string field)
        {
            return !string.IsNullOrEmpty(field);
        }

        private void AddErrorDomain(DomainNotification notification)
        {
            if (FilledField(notification) && !ErrorExists(notification.ErrorMessage))
            {
                _notifications.Add(notification);
            }
        }

        private bool ErrorExists(string field)
        {
            return _notifications.Any(a => a.ErrorMessage == field);
        }

        private static bool FilledField(DomainNotification notification)
        {
            return !string.IsNullOrEmpty(notification.ErrorMessage);
        }
    }
}