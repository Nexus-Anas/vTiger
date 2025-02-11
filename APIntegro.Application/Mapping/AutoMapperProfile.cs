using APIntegro.Application.DTOs.Project;
using APIntegro.Application.DTOs.ProjectTask;
using APIntegro.Application.Services.Authentication;
using APIntegro.Domain.Entities;
using AutoMapper;

namespace APIntegro.Application.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //Authentication Mapping
        CreateMap<AuthResult, User>();

        //Project Mapping
        CreateMap<ProjectPostDTO, Project>();
        CreateMap<ProjectPutDTO, Project>();

        //Project Task Mapping
        CreateMap<ProjectTaskPostDTO, ProjectTask>();
        CreateMap<ProjectTaskPutDTO, ProjectTask>();
    }
}