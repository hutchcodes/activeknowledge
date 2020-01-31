using AKS.Api.Build.Client;
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
        private readonly UserApi _userApi;
        private readonly IAppState _appState;

        public CustomAuthenticationProvider(UserApi userApi, IAppState appState)
        {
            _userApi = userApi;
            _appState = appState;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal user;
            var result = await _userApi.GetCurrentUser();
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
