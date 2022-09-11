using KP.Core.Data;
using KP.Services.Product;
using KP.Services.User;

namespace KP.Web.Api
{
    public class DependencyRegistration
    {
        /// <summary>
        /// Register services
        /// </summary>
        /// <param name="builder"></param>
        public void Register(IServiceCollection builder)
        {
            //Per request lifetime

            //Singleton services

            //Transient services

            builder.AddTransient(typeof(IRepository<>), typeof(EFCoreRepository<>));
            builder.AddTransient<IProductService, ProductService>();
            builder.AddTransient<IUserService, UserService>();
        }
    }
}
