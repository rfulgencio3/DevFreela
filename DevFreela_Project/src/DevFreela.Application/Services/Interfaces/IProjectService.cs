﻿using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {
        //List<ProjectViewModel> GetAll(string query);
        ProjectDetailsViewModel GetById(int id);
        void Start(int id);
        void Finish(int id);
    }
}
