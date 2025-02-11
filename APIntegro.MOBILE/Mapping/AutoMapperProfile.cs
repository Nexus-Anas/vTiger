using APIntegro.Domain.Entities;
using APIntegro.MOBILE.ViewModels.Contact;
using APIntegro.MOBILE.ViewModels.Project;
using APIntegro.MOBILE.ViewModels.ProjectTask;
using APIntegro.MOBILE.ViewModels.Organization;
using AutoMapper;

namespace APIntegro.MOBILE.Mapping;

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
        CreateMap<Domain.Entities.Contact, ContactPostVM>();
        CreateMap<Domain.Entities.Contact, ContactPutVM>();

        //Organization Mapping
        CreateMap<Organization, OrganizationPostVM>();
        CreateMap<Organization, OrganizationPutVM>();
    }
}