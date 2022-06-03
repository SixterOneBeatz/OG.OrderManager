using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OG.OrderManager.Domain;

namespace OG.OrderManager.Infrastructure.Contexts.OrderManagerContextConfiguration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.Amount).HasColumnType("float");
            builder.Property(x => x.CreatedDate).HasColumnType("datetime");
            builder.Property(x => x.ModifiedDate).HasColumnType("datetime");
        }
    }
}
