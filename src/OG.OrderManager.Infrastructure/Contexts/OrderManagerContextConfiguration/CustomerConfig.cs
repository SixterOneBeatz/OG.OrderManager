using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OG.OrderManager.Domain;

namespace OG.OrderManager.Infrastructure.Contexts.OrderManagerContextConfiguration
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.CreatedDate).HasColumnType("datetime");
            builder.Property(x => x.ModifiedDate).HasColumnType("datetime");
        }
    }
}
