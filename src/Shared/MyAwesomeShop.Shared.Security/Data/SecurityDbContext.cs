using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using MyAwesomeShop.Shared.Security.Data.EntityTypeConfigurations;
using MyAwesomeShop.Shared.Security.Domain;

namespace MyAwesomeShop.Shared.Security.Data;

public class SecurityDbContext : IdentityDbContext<User, Role, Guid>
{
    public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("security");
        builder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}
