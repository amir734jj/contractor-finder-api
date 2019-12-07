using AutoMapper;
using Dal.Interfaces;
using Models.Entities.Common;
using Models.Entities.Users;
using static Logic.Utilities.FileRepresentationUtility;

namespace Logic.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile(IS3Service s3Service)
        {
            CreateMap<User, User>()
                .ForMember(x => x.ProfilePhoto, opt => opt.Ignore())
                .AfterMap((source, destination) =>
                {
                    source.ProfilePhoto =
                        ResolveFileRepresentation<ProfilePhoto>(s3Service.GetUri(source.ProfilePhotoKey).Result);
                });
        }
    }
}