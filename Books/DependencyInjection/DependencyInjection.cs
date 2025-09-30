#region Usings
using Microsoft.AspNetCore.Identity;
#endregion

#region project Usings
using Books.InterfaceAdapter.Layer;
using Books.ApplicationBusiness.Layer;
using Books.EnterpriseBusiness.Layer.Models;
using Books.EnterpriseBusiness.Layer.Entitys;
using Books.InterfaceAdapter.Layer.Respositorys;
using Books.ApplicationBusiness.Layer.Interfaces;
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
