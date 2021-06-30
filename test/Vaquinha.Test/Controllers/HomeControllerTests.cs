using Microsoft.Extensions.Logging;
using Moq;
using Vaquinha.App.Interfaces.Service;

namespace Vaquinha.App.Controllers
{
    public class HomeControllerTests
    {
        private readonly IHomeInfoService _homeInfoService;
        private readonly Mock<ILogger<HomeController>> _logger;

        public HomeControllerTests()
        {

        }
    }
}