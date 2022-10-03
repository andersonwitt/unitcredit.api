using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class BaseMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public BaseMap(string TableName)
        {
            _tableName = TableName;
        }
        
        private string _tableName { get; set; }

        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(_tableName);

            builder.HasKey(u => u.Id);
        }
    }
}