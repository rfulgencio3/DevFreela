using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public DevFreelaDbContext()
        {
            Projects = new List<Project>
            {
                new Project("Meu projeto ASP.NET Core", "Minha descrição de projeto 1", 1, 1, 10000),
                new Project("Meu projeto ASP.NET Core", "Minha descrição de projeto 2", 1, 1, 20000),
                new Project("Meu projeto ASP.NET Core", "Minha descrição de projeto 3", 1, 1, 30000),
            };
        }
        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ProjectComment> ProjectComments { get; set; }
    }
}
