using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace CandyBackend.Api;

public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
{
    [Obsolete("Obsolete")]
    public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder) :
        base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        return Task.FromResult(HandleAuthenticate());
    }

    private AuthenticateResult HandleAuthenticate()
    {
        var headersAuthorization = Request.Headers.Authorization;
        if (headersAuthorization.Count == 0)
        {
            return AuthenticateResult.NoResult();
        }

        var authHeader = AuthenticationHeaderValue.Parse(headersAuthorization.ToString());
        if (authHeader.Parameter == null)
            return AuthenticateResult.NoResult();
        var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
        var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
        var username = credentials[0];
        var password = credentials[1];

        return username switch
        {
            "candymanager" when password == "candymanager" => BuildAuthenticateResult("Candy Manager", "CandyManager"),
            "administrator" when password == "administrator" => BuildAuthenticateResult("Administrator", "Administrator"),
            _ => AuthenticateResult.Fail("Wrong username or password.")
        };
    }

    private AuthenticateResult BuildAuthenticateResult(string name, string role)
    {
        var claims = new List<Claim> { new("Name", name), new("Role", role) };
        var claimsIdentity = new ClaimsIdentity(claims, "Basic", "Name", "Role");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
    }
}

public class BasicAuthenticationOptions : AuthenticationSchemeOptions;