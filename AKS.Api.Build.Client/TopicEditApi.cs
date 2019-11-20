using AKS.Common.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.Api.Build.Client
{
    public class TopicEditApi
    {
        readonly string _aksBuildApiBaseUrl;

        public TopicEditApi(IConfiguration configuration)
        {
            _aksBuildApiBaseUrl = configuration.GetValue<string>("AppSettings:AKSBuildApiBaseUrl");
        }
        
        public async Task<TopicEdit> GetTopic(Guid projectId, Guid topicId)
        {
            var client = new RestClient(_aksBuildApiBaseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("topicedit/{projectId}/{topicId}", Method.GET);
            request.AddUrlSegment("projectId", projectId); 
            request.AddUrlSegment("topicId", topicId); 

            // easily add HTTP Headers
            request.AddHeader("header", "value");

            var response = await client.ExecuteTaskAsync<TopicEdit>(request);
            var topic = response.Data;
            return topic;
        }

        public async Task<TopicEdit> UpdateTopic(TopicEdit topicedit)
        {
            var client = new RestClient(_aksBuildApiBaseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("topicedit", Method.POST);
            request.AddJsonBody(topicedit);


            // easily add HTTP Headers
            request.AddHeader("header", "value");

            var response = await client.ExecuteTaskAsync<TopicEdit>(request);
            topicedit = response.Data;
            return topicedit;
        }

        public async Task DeleteTopic(Guid projectId, Guid topicId)
        {
            var client = new RestClient(_aksBuildApiBaseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("topicedit/{projectId}/{topicId}", Method.DELETE);
            request.AddUrlSegment("projectId", projectId);
            request.AddUrlSegment("topicId", topicId);

            // easily add HTTP Headers
            request.AddHeader("header", "value");

            var response = await client.ExecuteTaskAsync<TopicEdit>(request);

            return;
        }
    }
}
