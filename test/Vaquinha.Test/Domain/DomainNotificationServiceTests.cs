using System.Linq;
using FluentAssertions;
using Vaquinha.App.Entities;
using Vaquinha.App.Fixtures;
using Vaquinha.App.Interfaces.Service;
using Xunit;

namespace Vaquinha.Test.Domain
{
    [Collection(nameof(PersonFixtureCollection))]
    public class DomainNotificationServiceTests: IClassFixture<PersonFixture>
    {
        private readonly PersonFixture _personFixture;
        private readonly IDomainNotificationService _domainNotificationService;

        public DomainNotificationServiceTests(PersonFixture fixture)
        {
            _personFixture = fixture;
            _domainNotificationService = new DomainNotificationService();
        }

        [Trait("DomainNotificationService", "DomainNotificationService_MustNotHaveNotifications")]
        [Fact]
        public void DomainNotificationService_MustNotHaveNotifications()
        {
            // Arrange & Act
            var domainNotification = new DomainNotificationService();

            // Assert
            domainNotification.HasErrors.Should().BeFalse(because:"ainda não foi adicionado nenhuma notificadao de dominino");
        }
        
        [Trait("DomainNotificationService", "DomainNotificationService_AddNotification")]
        [Fact]
        public void DomainNotificationService_AddNotification()
        {
            // Arrange
            var domainNotification = new DomainNotification("RequiredField", "O campo Nome é obrigatório");

            // Act
            _domainNotificationService.Add(domainNotification);

            // Assert            
            _domainNotificationService.HasErrors.Should().BeTrue(because: "foi adicionado a notificacao de codigo RequiredField");

            var notifications = _domainNotificationService.RestoreDomainErrors().Select(a => a.ErrorMessage);
            notifications.Should().Contain("O campo Nome é obrigatório", because: "foi adicionado a notificacao de codigo RequiredField");
        }

        [Trait("DomainNotificationService", "DomainNotificationService_AdicionarEntidade_HasNotificationsTrue")]
        [Fact]
        public void DomainNotificationService_AddEntity()
        {
            // Arrange
            var person = _personFixture.PersonEmpty();
            person.Valid();

            // Act
            _domainNotificationService.Add(person);

            // Assert
            var notifications = _domainNotificationService.RestoreDomainErrors().Select(a => a.ErrorMessage);

            notifications.Should().HaveCount(2, because: "nenhum dos 2 campos obrigatórios foi informado.");
            notifications.Should().Contain("O campo Nome é obrigatório.", because: "o campo Nome não foi informado.");
            notifications.Should().Contain("O campo Email é obrigatório.", because: "o campo Email não foi informado.");

            _domainNotificationService.HasErrors.Should().BeTrue(because: "foi adicionado a entidade pessoa inválida");
        }
    }
}