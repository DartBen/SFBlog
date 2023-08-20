using AutoMapper;
using BlogApp.BLL.RequestModels;
using BlogApp.DLL.Models;

namespace BlogAppAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRequest, User>();
            CreateMap<User, UserRequest>();
            CreateMap<TagRequest, Tag>();
            CreateMap<Tag, TagRequest>();
        }
    }
}
