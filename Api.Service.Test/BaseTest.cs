using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.CrossCutting.Mappings;
using AutoMapper;

namespace Api.Service.Test
{
    public abstract class BaseTest
    {
        public IMapper Mapper { get; set; }
        public BaseTest()
        {
            //Environment.SetEnvironmentVariable("Example If its necessary", "Value for it");
            Mapper = new AutoMapperFixture().GetMapper();
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
        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new EntityToDTOProfile());
                });
                return config.CreateMapper();
            }
            public void Dispose()
            {

            }
        }
    }
}