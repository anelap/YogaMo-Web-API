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
using YogaMo.WebAPI.Services;

namespace YogaMo.WebAPI.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IInstructorService _instructorService;
        private readonly IClientService _clientService;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IInstructorService instructorService,
            IClientService clientService) : base(options, logger, encoder, clock)
        {
            _instructorService = instructorService;
            _clientService = clientService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            string currentUsername = null;
            string currentFirstName = null;
            string currentRole = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];


                var instructor = _instructorService.Authenticate(username, password);
                if(instructor != null)
                {
                    currentUsername = instructor.Username;
                    currentFirstName = instructor.FirstName;
                    currentRole = "Instructor";
                    _instructorService.SetCurrentInstructor(instructor);
                }
                else
                {
                    var client = _clientService.Authenticate(username, password);
                    if (client != null) {
                        currentUsername = client.Username;
                        currentFirstName = client.FirstName;
                        currentRole = "Client";
                        _clientService.SetCurrentClient(client);
                    }
                }
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (currentUsername == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, currentUsername),
                new Claim(ClaimTypes.Name, currentFirstName),
            };

            claims.Add(new Claim(ClaimTypes.Role, currentRole));

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }

}
