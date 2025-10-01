#region Usings
using Microsoft.AspNetCore.Identity;
#endregion

#region project Usings
using Books.Domain.Layer.Entitys;
using Books.Domain.Layer.Models;
using Books.Domain.Layer.Interfaces;
using Books.Application.Layer.Services;
using Books.Infrastructure.Layer.Persistence;
using Books.Infrastructure.Layer.Respositorys;
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
            services.AddScoped<IModelResult<BookEntity>, ModelResult<BookEntity>>();
            services.AddScoped<BookServices<BookEntity>>();

            services.AddScoped<IRepository<ReviewEntity>, RespositoryReview>();
            services.AddScoped<IModelResult<ReviewEntity>, ModelResult<ReviewEntity>>();
            services.AddScoped<ReviewServices<ReviewEntity>>();

            services.AddScoped<IRepository<CustomUserProfile>, RespositoryUser>();
            services.AddScoped<IModelResult<CustomUserProfile>, ModelResult<CustomUserProfile>>();
            services.AddScoped<UserServices<CustomUserProfile>>();
        }

    }
}
