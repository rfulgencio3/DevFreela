using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;
        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get(string query)
        {
            var projects = _service.GetAll(query);
            return StatusCode(200, projects);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _service.GetById(id);
            if (project == null) return StatusCode(404);

            return StatusCode(200, project);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectInputModel createProject)
        {
            if (createProject.Title.Length > 50) return StatusCode(400);

            var id = _service.Create(createProject);

            return StatusCode(201, (nameof(GetById), new { id = id }, createProject));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectInputModel updateProject)
        {
            if (updateProject.Description.Length > 200) return StatusCode(400);

            _service.Update(updateProject);

            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = GetById(id);
            if (project == null) return StatusCode(404);

            _service.Delete(id);

            return StatusCode(204);
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, [FromBody] CreateCommentInputModel createComment)
        {
            _service.CreateComment(createComment);
            return StatusCode(204);
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            _service.Start(id);
            return StatusCode(204);
        }

        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            _service.Finish(id);
            return StatusCode(204);
        }
    }
}
