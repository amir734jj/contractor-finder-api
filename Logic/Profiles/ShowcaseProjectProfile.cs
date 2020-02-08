using System.Linq;
using AutoMapper;
using Dal.Interfaces;
using Models.Entities.Common;
using Models.Entities.Projects;
using static Logic.Utilities.FileRepresentationUtility;

namespace Logic.Profiles
{
    public class ShowcaseProjectProfile : Profile
    {
        public ShowcaseProjectProfile(IS3Service s3Service)
        {
            CreateMap<ShowcaseProject, ShowcaseProject>();
        }
    }
}