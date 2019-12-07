using System.Linq;
using AutoMapper;
using Dal.Interfaces;
using Models.Entities.Common;
using Models.Entities.Projects;
using static Models.Utilities.FileRepresentationUtility;

namespace Logic.Profiles
{
    public class ProjectMilestoneProfile : Profile
    {
        public ProjectMilestoneProfile(IS3Service s3Service)
        {
            CreateMap<ProjectMilestone, ProjectMilestone>()
                .ForMember(x => x.ProjectPhotos, opt => opt.Ignore())
                .AfterMap((source, destination) =>
                {
                    destination.ProjectPhotos = source.ProjectPhotosKeys
                        .Select(x => ResolveFileRepresentation<ProjectPhoto>(s3Service.GetUri(x).Result))
                        .ToList();
                });
        }
    }
}