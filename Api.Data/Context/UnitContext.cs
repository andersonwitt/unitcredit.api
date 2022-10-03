using Api.Data.Mappings;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class UnitContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public UnitContext(DbContextOptions<UnitContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(new UsersMap("Users").Configure);
        }
    }
}