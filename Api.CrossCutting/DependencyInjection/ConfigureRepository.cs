using Api.Data.Implementations;
using Api.Domain.Repository;
using Data.Context;
using Data.Implementations;
using Data.Repository;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();
            serviceCollection.AddScoped<ITransactionRepository, TransactionImplementation>();

            serviceCollection.AddDbContext<UnitContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION"))
            );
        }
    }
}