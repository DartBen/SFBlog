using AutoMapper;
using BlogApp.BLL.RequestModels;
using BlogApp.BLL.Validators;
using BlogApp.DLL.Context;
using BlogApp.DLL.Repository.Interfaces;
using BlogApp.DLL.Repository;
using FluentValidation;
using Microsoft.OpenApi.Models;

namespace BlogApp
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
            builder.Services.AddTransient<IRoleRepository, RoleRepository>();

            // малидаторы
            builder.Services.AddTransient<IValidator<UserRequest>, UserRequestValidator>();
            builder.Services.AddTransient<IValidator<TagRequest>, TagRequestValidator>();
            builder.Services.AddTransient<IValidator<ArticleRequest>, ArticleRequestValidator>();
            builder.Services.AddTransient<IValidator<CommentRequest>, CommentRequestValidator>();
            builder.Services.AddTransient<IValidator<RoleReqest>, RoleReqestValidator>();

            // аутентификация
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
            builder.Services.AddSwaggerGen();

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.MapRazorPages();

            app.Run();
        }
    }
}