using AutoMapper;
using BlogApp.BLL.RequestModels;
using BlogApp.BLL.Validators;
using BlogApp.DLL.Context;
using BlogApp.DLL.Repository;
using BlogApp.DLL.Repository.Interfaces;
using FluentValidation;

namespace BlogAppAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // база SQLite
            builder.Services.AddDbContext<BlogDB>();
            // маппер
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // репозитории базы
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ICommentRepository, CommentRepository>();
            builder.Services.AddTransient<ITagRepository, TagRepository>();
            builder.Services.AddTransient<IArticleRepository, ArticleRepository>();

            // малидаторы
            builder.Services.AddTransient<IValidator<UserRequest>, UserRequestValidator>();
            builder.Services.AddTransient<IValidator<TagRequest>,  TagRequestValidator>();
            builder.Services.AddTransient<IValidator<ArticleRequest>, ArticleRequestValidator>();
            builder.Services.AddTransient<IValidator<CommentRequest>, CommentRequestValidator>();

            // аутентификация


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}