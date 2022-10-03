using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mappings
{
    public class UsersMap : IEntityTypeConfiguration<UserEntity>
    {
        private string _tableName { get; set; }

        public UsersMap(string TableName)
        {
            _tableName = TableName;
        }

        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable(_tableName);

            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.StudentId)
                .HasMaxLength(10);

            builder
                .HasIndex(u => u.StudentId)
                .IsUnique();
        }
    }
}