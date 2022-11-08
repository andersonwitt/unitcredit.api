using Api.Data.Mapping;
using Api.Data.Mappings;
using Api.Domain.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class UnitContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }

        public UnitContext(DbContextOptions<UnitContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(new UsersMap("Users").Configure);
            modelBuilder.Entity<TransactionEntity>(new BaseMap<TransactionEntity>("Transactions").Configure);
        }
    }
}