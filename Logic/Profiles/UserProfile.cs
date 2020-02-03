using AutoMapper;
using Dal.Interfaces;
using Models.Entities.Users;

namespace Logic.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile(IS3Service s3Service)
        {
            CreateMap<User, User>()
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .AfterMap((source, destination) =>
                {
                    /*source.ProfilePhoto =
                        ResolveFileRepresentation<ProfilePhoto>(s3Service.GetUri(source.ProfilePhoto).Result);*/
                });
        }
    }
}