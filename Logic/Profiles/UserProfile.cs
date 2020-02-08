using AutoMapper;
using Dal.Interfaces;
using Models.Entities.Users;

namespace Logic.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile(IS3Service s3Service)
        {
            CreateMap<User, User>();
        }
    }
}