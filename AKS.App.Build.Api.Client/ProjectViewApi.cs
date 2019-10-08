using AKS.Common.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.App.Build.Api.Client
{
    public class ProjectViewApi
    {
        readonly string _aksBuildApiBaseUrl;

        public ProjectViewApi(IConfiguration configuration)
        {
            _aksBuildApiBaseUrl = configuration.GetValue<string>("AppSettings:AKSBuildApiBaseUrl");
        }        

        public async Task<List<ProjectList>> GetProjectListByCustomer(Guid customerId)
        {
            var client = new RestClient(_aksBuildApiBaseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("project/list/{customerId}", Method.GET);
            request.AddUrlSegment("customerId", customerId);

            // easily add HTTP Headers
            request.AddHeader("header", "value");

            var response = await client.ExecuteTaskAsync<List<ProjectList>>(request);
            var projectList = response.Data;
            return projectList;
        }
    }
}
