using AutoMapper;
using SocialWebAPI.Db;
using SocialWebAPI.Entities;

namespace SocialWebAPI.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt=> opt.MapFrom(src=>src.Photos.FirstOrDefault(x=>x.IsMain).Url));
            CreateMap<Photo, PhotoDto>();
        }
    }
}
