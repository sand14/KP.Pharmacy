using KP.Common.Model.Automapper;

namespace KP.Web.Api
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            new DependencyRegistration().Register(services);
            AutoMapperConfiguration.Init();
            AutoMapperConfiguration.MapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
