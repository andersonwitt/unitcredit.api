using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<UnitContext>
    {
        public UnitContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=unitdb;";
            var optionsBuilder = new DbContextOptionsBuilder<UnitContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new UnitContext(optionsBuilder.Options);
        }
    }
}