using KP.Core.Data;
using KP.Core.DomainModels;
using KP.Services.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace KP.Web.Api
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IRepository<User> usersRepository;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IRepository<User> usersRepository) : base(options, logger, encoder, clock)
        {
            this.usersRepository = usersRepository;
        }

        private UserService GetService()
        {
            return new(usersRepository);
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("No Header found");
            }

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var bytes = Convert.FromBase64String(authHeader.Parameter);
            string credentials = Encoding.UTF8.GetString(bytes);
            if (!string.IsNullOrEmpty(credentials))
            {
                string[] array = credentials.Split(":");
                string username = array[0];
                string password = array[1];

                var user = usersRepository.TableNoTracking.FirstOrDefault(x => x.Username == username);
                if (user == null)
                {
                    return AuthenticateResult.Fail("UnAuthorized");
                }

                UserService service = GetService();
                if (!service.PasswordVerify(user.Username, password))
                {
                    return AuthenticateResult.Fail("UnAuthorized");
                }
                var claim = new[] { new Claim(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claim, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);

            }
            else
            {
                return AuthenticateResult.Fail("UnAuthorized");
            }
        }
    }
}
