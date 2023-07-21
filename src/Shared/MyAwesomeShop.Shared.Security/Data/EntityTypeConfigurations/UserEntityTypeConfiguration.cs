using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MyAwesomeShop.Shared.Security.Domain;

namespace MyAwesomeShop.Shared.Security.Data.EntityTypeConfigurations;
internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(p => p.RefreshToken, b =>
        {
            b.Property(c => c.Value);
            b.Property(c => c.ValidTo);
        });
    }
}
