using Domain.Managers;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Service.Managers;
using Service.Services;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<ILoginManager, LoginManager>();
        }
    }
}