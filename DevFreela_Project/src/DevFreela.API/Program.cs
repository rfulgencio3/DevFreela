using DevFreela.API.Filters;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Services.Implementations;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.Validators;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//IConfiguration Configuration = builder.Configuration;
//builder.Services.Configure<OppeningTimeOption>(Configuration.GetSection("OppeningTime"));

//var connectionString = Configuration.GetConnectionString("DevFreelaCs");

// Add services to the container.
//builder.Services.AddSingleton<DevFreelaDbContext>();
//builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseInMemoryDatabase("DevFreela"));

//Services
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserService, UserService>();

//Repositories
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)))
    .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>())
    .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<CreateProjectCommandValidator>());

builder.Services.AddMediatR(typeof(CreateProjectCommand).Assembly);
builder.Services.AddMediatR(typeof(CreateCommentCommand).Assembly);
builder.Services.AddMediatR(typeof(DeleteProjectCommand).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
