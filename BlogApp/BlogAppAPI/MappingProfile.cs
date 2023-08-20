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
            CreateMap<CommentRequest, Comment>();
            CreateMap<Comment, Comment>();
            CreateMap<ArticleRequest, Article>();
            CreateMap<Article, ArticleRequest>();
        }
    }
}
