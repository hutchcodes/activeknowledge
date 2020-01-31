using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AKS.Api.Build.Client
{
    public class UserApi
    {
        private readonly HttpClient _http;

        public UserApi(HttpClient http)
        {
            _http = http;
        }

        public async Task<AKSUserOld> GetCurrentUser()
        {
            return await _http.GetJsonAsync<AKSUserOld>("user/GetUser");
        }
    }
}
