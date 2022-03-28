using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using Xunit;

namespace DevFreela.Tests.Core.Entities
{
    public class ProjectTests
    {
        [Fact]
        public void TetIfProjectStartWorks()
        {
            var project = new Project("Teste", "Descicao", 1, 2, 1000);

            Assert.Equal(ProjectStatusEnum.Created, project.Status);
            Assert.Null(project.StartedAt);
            Assert.NotNull(project.Title);
            Assert.NotEmpty(project.Title);
            Assert.NotNull(project.Description);
            Assert.NotEmpty(project.Description);

            project.Start();

            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
        }
    }
}
