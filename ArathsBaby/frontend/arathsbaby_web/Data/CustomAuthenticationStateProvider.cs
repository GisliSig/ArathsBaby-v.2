using ArathsBaby.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace arathsbaby_web.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = new ClaimsPrincipal();
            return Task.FromResult(new AuthenticationState(user));
        }

        public void UserLogin(string Email)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name,Email)
            },"authentication");

            // Shows how you can add a role claim with any logic. This is just for demo purposes, best case scenario would be to add
            // a table like UserRoles and call the service to fetch roles and add them to the ClaimsIdentity
            if (Email == "5")
                identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

            var user = new ClaimsPrincipal(identity);
            
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void UserLogout()
        {
            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
