using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace MyAwesomeShop.Shared.Security;

public class SecurityOptions
{
    public string Issuer { get; set; }

    public string Audience { get; set; }

    public string Key { get; set; }

    public TimeSpan Expiry { get; set; }

    public SymmetricSecurityKey GetSigningKey() => new(Encoding.UTF8.GetBytes(Key));
}