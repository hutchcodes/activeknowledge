using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AKS.Common
{
    public class CustomAuthenticationProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;

        public CustomAuthenticationProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal user;
            var http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:44303/");
            var result = await http.GetJsonAsync<AKSUser>("api/user/GetUser");
            Console.WriteLine($"UserName: {result.UserName}");
            if (result.UserName != "")
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, result.UserName),
                },  "AzureAdB2CAuth");
                user = new ClaimsPrincipal(identity);
            }
            else
            {
                user = new ClaimsPrincipal();
            }
            return await Task.FromResult(new AuthenticationState(user));
        }
    }
}
