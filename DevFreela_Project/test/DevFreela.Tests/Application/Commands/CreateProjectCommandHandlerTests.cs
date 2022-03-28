using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.Tests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            // Arrange
            var repositoryMock = new Mock<IProjectRepository>();

            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Titulo de Teste",
                Description = "Descrição Teste",
                TotalCost = 50000,
                IdClient = 1,
                IdFreelancer = 2
            };

            var createProjectCommandHandler = new CreateProjectCommandHandler(repositoryMock.Object);

            // Act
            var id = await createProjectCommandHandler.Handle(createProjectCommand, new System.Threading.CancellationToken());

            // Assert
            Assert.True(id >= 0);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Project>()), Times.Once);
        }
    }
}
