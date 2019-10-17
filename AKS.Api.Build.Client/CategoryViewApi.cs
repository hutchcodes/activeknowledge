using AKS.Common.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.Api.Build.Client
{
    public class CategoryViewApi
    {
        readonly string _aksBuildApiBaseUrl;

        public CategoryViewApi(IConfiguration configuration) 
        {
            _aksBuildApiBaseUrl = configuration.GetValue<string>("AppSettings:AKSBuildApiBaseUrl");
        }
        public async Task<CategoryTreeView> GetCategoryTreeForProject(Guid projectId)
        {
            var client = new RestClient(_aksBuildApiBaseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("categoryview/project/{projectId}", Method.GET);
            request.AddUrlSegment("projectId", projectId); 

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            var response = await client.ExecuteTaskAsync<CategoryTreeView>(request);
            var categoryTree = response.Data;
            return categoryTree;
        }
    }
}
