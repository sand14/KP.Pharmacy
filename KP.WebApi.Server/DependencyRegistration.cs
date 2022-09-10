using KP.Core.Data;
using KP.Services.Product;

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
        }
    }
}
