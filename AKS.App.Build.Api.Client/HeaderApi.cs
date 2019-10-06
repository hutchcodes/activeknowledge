using AKS.Common.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.App.Build.Api.Client
{
    public class HeaderApi
    {
        readonly string _aksApiBaseUrl;

        public HeaderApi(IConfiguration configuration) 
        {
            _aksApiBaseUrl = configuration.GetValue<string>("AppSettings:AKSApiBaseUrl");
        }
        public async Task<HeaderNavView> GetHeaderForProject(Guid projectId)
        {
            var client = new RestClient(_aksApiBaseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("header/project/{projectId}", Method.GET);
            request.AddUrlSegment("projectId", projectId); 

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            var response = await client.ExecuteTaskAsync<HeaderNavView>(request);
            var headerNav = response.Data;
            return headerNav;
        }

        public async Task<HeaderNavView> GetHeaderForCustomer(Guid customerId)
        {
            var client = new RestClient(_aksApiBaseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("header/customer/{customerId}", Method.GET);
            request.AddUrlSegment("customerId", customerId);

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            var response = await client.ExecuteTaskAsync<HeaderNavView>(request);
            var headerNav = response.Data;
            return headerNav;
        }
    }
}
