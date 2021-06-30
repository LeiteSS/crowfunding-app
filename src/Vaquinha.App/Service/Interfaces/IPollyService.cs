using Polly.Retry;

namespace Vaquinha.App.Service.Interfaces
{
    public interface IPollyService
    {
         AsyncRetryPolicy CreatePoliticWaitAndRetryFor(string method);
    }
}