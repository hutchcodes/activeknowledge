using AKS.Common.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.Api.Build.Client
{
    public class HeaderApi
    {
        readonly string _aksBuildApiBaseUrl;

        public HeaderApi(IConfiguration configuration) 
        {
            _aksBuildApiBaseUrl = configuration.GetValue<string>("AppSettings:AKSBuildApiBaseUrl");
        }
        public async Task<HeaderNavView> GetHeaderForProject(Guid projectId)
        {
            var client = new RestClient(_aksBuildApiBaseUrl);
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
            var client = new RestClient(_aksBuildApiBaseUrl);
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
