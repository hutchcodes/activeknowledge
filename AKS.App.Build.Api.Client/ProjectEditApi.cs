using AKS.Common.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.App.Build.Api.Client
{
    public class ProjectEditApi
    {
        readonly string _aksBuildApiBaseUrl;

        public ProjectEditApi(IConfiguration configuration)
        {
            _aksBuildApiBaseUrl = configuration.GetValue<string>("AppSettings:AKSBuildApiBaseUrl");
        }        

        public async Task<ProjectEdit> GetProject(Guid projectId)
        {
            var client = new RestClient(_aksBuildApiBaseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("project/{projectId}", Method.GET);
            request.AddUrlSegment("projectId", projectId);

            // easily add HTTP Headers
            request.AddHeader("header", "value");

            var response = await client.ExecuteTaskAsync<ProjectEdit>(request);
            var project = response.Data;
            return project;
        }

        public async Task<ProjectEdit> UpdateProject(ProjectEdit projectEdit)
        {
            var client = new RestClient(_aksBuildApiBaseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("project", Method.POST);
            request.AddJsonBody(projectEdit);

            // easily add HTTP Headers
            request.AddHeader("header", "value");

            var response = await client.ExecuteTaskAsync<ProjectEdit>(request);
            var project = response.Data;
            return project;
        }
    }
}
