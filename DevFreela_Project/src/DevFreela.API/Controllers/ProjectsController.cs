﻿using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _service;
    private readonly IMediator _mediator;
    public ProjectsController(
        IProjectService service,
        IMediator mediator
        )
    {
        _service = service;
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> Get(string query)
    {
        var getAllProjectsQuery = new GetAllProjectsQuery();
        
        //var projects = _service.GetAll(query);
        var projects = _mediator.Send(getAllProjectsQuery); 

        return StatusCode(200, projects);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "client, freelancer")]
    public IActionResult GetById(int id)
    {
        var project = _service.GetById(id);
        if (project == null) return StatusCode(404);

        return StatusCode(200, project);
    }

    [HttpPost]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
    {
       
        //var id = _service.Create(createProject);
        var id = await _mediator.Send(command);

        return StatusCode(201, (nameof(GetById), new { id = id }, command));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectInputModel command)
    {
        if (command.Description.Length > 200) return StatusCode(400);

        await _mediator.Send(command);

        return StatusCode(204);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Delete(int id)
    {
        var project = GetById(id);
        if (project == null) return StatusCode(404);

        var command = new DeleteProjectCommand(id);
        await _mediator.Send(command);

        return StatusCode(204);
    }

    [HttpPost("{id}/comments")]
    [Authorize(Roles = "client, freelancer")]
    public IActionResult PostComment(int id, [FromBody] CreateCommentCommand createComment)
    {
        _mediator.Send(createComment);
        return StatusCode(204);
    }

    [HttpPut("{id}/start")]
    [Authorize(Roles = "client")]
    public IActionResult Start(int id)
    {
        _service.Start(id);
        return StatusCode(204);
    }

    [HttpPut("{id}/finish")]
    [Authorize(Roles = "client")]
    public IActionResult Finish(int id)
    {
        _service.Finish(id);
        return StatusCode(204);
    }
}
