using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Vaquinha.App.Interfaces.Service;

namespace Vaquinha.App.Controllers
{
    public class BaseController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IDomainNotificationService _domainNotificationService;

        public BaseController(IDomainNotificationService domainNotificationService,
                              IToastNotification toastNotification)
        {
            _domainNotificationService = domainNotificationService;
            _toastNotification = toastNotification;
        }

        protected void AddNotificationOperationAccomplishWithSuccess(string messageSuccess = null)
        {
            var sucessMessage = messageSuccess ?? "Operação realizada com sucesso!";
            _toastNotification.AddSuccessToastMessage(sucessMessage);
        }

        protected void AddDomainError()
        {
            var errorMessage = _domainNotificationService.HasErrors
                ? _domainNotificationService.RestoreDomainErrorsInHtmlFormat()
                : null;

            if (!string.IsNullOrEmpty(errorMessage))
            {
                _toastNotification.AddErrorToastMessage(errorMessage);
            }
        }
    }
}