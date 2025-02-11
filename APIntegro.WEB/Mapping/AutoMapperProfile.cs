using APIntegro.Domain.Entities;
using APIntegro.WEB.ViewModels.Contact;
using APIntegro.WEB.ViewModels.Project;
using APIntegro.WEB.ViewModels.ProjectTask;
using APIntegro.WEB.ViewModels.Organization;
using AutoMapper;

namespace APIntegro.WEB.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //Project Mapping
        CreateMap<Project, ProjectPostVM>();
        CreateMap<Project, ProjectPutVM>();

        //Project Task Mapping
        CreateMap<ProjectTask, ProjectTaskPostVM>();
        CreateMap<ProjectTask, ProjectTaskPutVM>();

        //Contact Mapping
        CreateMap<Contact, ContactPostVM>();
        CreateMap<Contact, ContactPutVM>();

        //Organization Mapping
        CreateMap<Organization, OrganizationPostVM>();
        CreateMap<Organization, OrganizationPutVM>();
    }
}