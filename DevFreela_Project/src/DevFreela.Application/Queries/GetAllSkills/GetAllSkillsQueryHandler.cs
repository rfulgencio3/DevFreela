using Dapper;
using DevFreela.Application.ViewModels;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<SkillDTO>>
    {
        private readonly ISkillRepository _repository;
        public GetAllSkillsQueryHandler(ISkillRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<SkillDTO>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll();

            //with Query
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();
            //    var script = "SELECT Id, Description FROM Skills";
                
            //    var skills = await sqlConnection.QueryAsync<SkillViewModel>(script);
                
            //    return skills.ToList();
            //}

        }
    }
}
