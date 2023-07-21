using Microsoft.AspNetCore.Identity;

namespace MyAwesomeShop.Shared.Security.Domain;

public class User : IdentityUser<Guid>
{
    public RefreshToken? RefreshToken { get; set; }
}
