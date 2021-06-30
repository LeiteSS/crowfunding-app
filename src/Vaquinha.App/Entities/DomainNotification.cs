namespace Vaquinha.App.Entities
{
    public class DomainNotification
    {
        public string Id { get; private set; }
        public string ErrorMessage { get; private set; }

        public DomainNotification(string errorCode, string errorMessage)
        {
            Id = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}