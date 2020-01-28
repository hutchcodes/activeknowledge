using AKS.App.Core.Data;
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
        private readonly IAppState _appState;

        public CustomAuthenticationProvider(HttpClient httpClient, IAppState appState)
        {
            _httpClient = httpClient;
            _appState = appState;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal user;
            var http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:44301/");
            var result = await http.GetJsonAsync<AKSUserOld>("api/user/GetUser");
            Console.WriteLine($"UserName: {result.UserName}");
            _appState.User = result;
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
