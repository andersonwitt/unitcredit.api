using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }
        public void ObjectAsserts<T, E>(T expected, E actual, List<string> propsToIgnore)
        {
            Assert.NotNull(actual);

            foreach (var prop in typeof(T).GetProperties())
            {
                if (propsToIgnore.Contains(prop.Name))
                {
                    continue;
                }

                var actualProp = actual
                    .GetType()
                    .GetProperties()
                    .FirstOrDefault(p => p.Name == prop.Name);

                var value1 = prop.GetValue(expected);
                var value2 = actualProp.GetValue(actual);

                Assert.Equal(value1, value2);
            }
        }
    }
    public class DbTest : IDisposable
    {
        private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", String.Empty)}";
        public ServiceProvider ServiceProvider { get; private set; }

        public DbTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<UnitContext>(o =>
                o.UseNpgsql($"Server=localhost;Port=5432;User Id=postgres;Password=root;Database={dataBaseName};"),
                ServiceLifetime.Transient
            );

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<UnitContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<UnitContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}