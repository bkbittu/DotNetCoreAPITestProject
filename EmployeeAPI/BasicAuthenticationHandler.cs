
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EmployeeAPI
{
    public class BasicAuthenticationHandler : AuthenticationHandler <AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> option,
            ILoggerFactory logger,
            UrlEncoder urlEncoder,
            ISystemClock systemClock): base(option,logger, urlEncoder, systemClock)
        {
            //HandleAuthenticateAsync();
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string username = null;
            try
            {
                var autheHeader = AuthenticationHeaderValue.Parse(Request.Headers["Autherization"]);
                var credential = Encoding.UTF8.GetString(Convert.FromBase64String(autheHeader.Parameter)).Split(':');
                username = credential.FirstOrDefault();
                var password = credential.LastOrDefault();
                if (username != "test@gmail.com" && password != "test@1234")
                    throw  new ArgumentException("Invilad credntial");
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"Authentication Fail: {ex.Message}");
            }
            var claims = new[] {
                new Claim(ClaimTypes.Name, username)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principle = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principle, Scheme.Name);
            return  AuthenticateResult.Success(ticket);

        }
    }
}
