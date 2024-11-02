using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Subscriptions.Persistence;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property("_maxGyms")
            .HasColumnName("MaxGyms");
        builder.Property("AdminId").HasColumnName("AdminId");
        builder.Property(x => x.SubscriptionType)
            .HasConversion(s => s.ToString(),
                s => (SubscriptionType)Enum.Parse(typeof(SubscriptionType), s));
        
        builder.Property<List<Guid>>("_gymIds")
            .HasColumnName("GymIds")
            .HasListOfIdsConverter();
    }
}