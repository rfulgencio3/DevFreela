using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly OppeningTimeOption _option;
        public ProjectsController(IOptions<OppeningTimeOption> option)
        {
            _option = option.Value;
        }

        [HttpGet]
        public IActionResult Get(string query)
        {
            return StatusCode(200);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return StatusCode(200);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectModel createProject)
        {
            if (createProject.Title.Length > 50) return StatusCode(400);
            return StatusCode(201, (nameof(GetById), new { id = createProject.Id }, createProject));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectModel updateProject)
        {
            if (updateProject.Description.Length > 200) return StatusCode(400);
            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = GetById(id);
            if (project == null) return StatusCode(404);
            return StatusCode(204);
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, [FromBody] CreateCommentModel createComment)
        {
            return StatusCode(204);
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return StatusCode(204);
        }

        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            return StatusCode(204);
        }
    }
}
