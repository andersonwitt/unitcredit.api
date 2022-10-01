using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Application.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Environment.SetEnvironmentVariable("DEFAULT_URL", "https://localhost:5001");
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
}