using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace DevFreela.Tests.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        [Fact]
        public async void ThreeProjectsExist_Executed_ReturnTreeProjectsViewModels()
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project("Teste 1", "Descriçaõ do teste 1", 1, 2, 10000),
                new Project("Teste 2", "Descriçaõ do teste 2", 1, 2, 20000),
                new Project("Teste 3", "Descriçaõ do teste 3", 1, 2, 30000)
        };

            var repositoryMock = new Mock<IProjectRepository>();
            repositoryMock.Setup(r => r.GetAllAsync().Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery();
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(repositoryMock.Object);

            // Act
            var projectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(projectViewModelList);
            Assert.NotEmpty(projectViewModelList);
            Assert.Equal(projects.Count, projectViewModelList.Count);

            repositoryMock.Verify(r => r.GetAllAsync().Result, Times.Once);
        }
    }
}
