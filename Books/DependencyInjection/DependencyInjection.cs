#region Usings
using Microsoft.AspNetCore.Identity;
#endregion

#region project Usings
using Books.Domain.Layer.Entitys;
using Books.Application.Layer.DTOs;
using Books.Domain.Layer.Interfaces;
using Books.Application.Layer.Services;
using Books.Infrastructure.Layer.Persistence;
using Books.Infrastructure.Layer.Respositorys;
using Books.Application.Layer.Querys.Books.GetAllBooks;
#endregion

namespace BooksPresentation.DependencyInjection
{
    public class DependencyInjection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<UserEntity, IdentityRole<int>>().AddEntityFrameworkStores<AppDbConext>();

            services.AddScoped<IRepository<BookEntity>, RespositoryBook>();
            services.AddScoped<IResultDto<BookEntity>, ResultDto<BookEntity>>();
            services.AddScoped<BookServices<BookEntity>>();

            services.AddScoped<IRepository<ReviewEntity>, RespositoryReview>();
            services.AddScoped<IResultDto<ReviewEntity>, ResultDto<ReviewEntity>>();
            services.AddScoped<ReviewServices<ReviewEntity>>();

            services.AddScoped<IRepository<CustomUserProfile>, RespositoryUser>();
            services.AddScoped<IResultDto<CustomUserProfile>, ResultDto<CustomUserProfile>>();
            services.AddScoped<UserServices<CustomUserProfile>>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllBooksQuery>());
        }

    }
}
