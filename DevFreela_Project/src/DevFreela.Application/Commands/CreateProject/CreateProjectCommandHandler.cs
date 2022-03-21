using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly DevFreelaDbContext _context;
        public CreateProjectCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.Title, request.Description, request.IdClient, request.IdFreelancer, request.TotalCost);
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return project.Id;
        }
    }
}
