
using AutoMapper;
using BlogApp.BLL.RequestModels;
using BlogApp.BLL.Validators;
using BlogApp.DLL.Context;
using BlogApp.DLL.Repository.Interfaces;
using BlogApp.DLL.Repository;
using FluentValidation;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // ���� SQLite
            builder.Services.AddDbContext<BlogDB>();
            // ������
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // ����������� ����
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ICommentRepository, CommentRepository>();
            builder.Services.AddTransient<ITagRepository, TagRepository>();
            builder.Services.AddTransient<IArticleRepository, ArticleRepository>();
            builder.Services.AddTransient<IRoleRepository, RoleRepository>();

            // ����������
            builder.Services.AddTransient<IValidator<UserRequest>, UserRequestValidator>();
            builder.Services.AddTransient<IValidator<TagRequest>, TagRequestValidator>();
            builder.Services.AddTransient<IValidator<ArticleRequest>, ArticleRequestValidator>();
            builder.Services.AddTransient<IValidator<CommentRequest>, CommentRequestValidator>();
            builder.Services.AddTransient<IValidator<RoleReqest>, RoleReqestValidator>();

            // ��������������
            builder.Services.AddAuthentication(options => options.DefaultScheme = "Cookies")
                            .AddCookie("Cookies", options =>
                            {
                                options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
                                {
                                    OnRedirectToLogin = redirectContext =>
                                    {
                                        redirectContext.HttpContext.Response.StatusCode = 401;
                                        return Task.CompletedTask;
                                    }
                                };
                            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var basePath = AppContext.BaseDirectory;

                var xmlPath = Path.Combine(basePath, "API.xml");
                options.IncludeXmlComments(xmlPath);
            });

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