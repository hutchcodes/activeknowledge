using AKS.Common.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.App.Build.Api.Client
{
    public class TopicViewApi
    {
        readonly string _aksBuildApiBaseUrl;

        public TopicViewApi(IConfiguration configuration)
        {
            _aksBuildApiBaseUrl = configuration.GetValue<string>("AppSettings:AKSBuildApiBaseUrl");
        }
        public async Task<TopicView> GetTopic(Guid projectId, Guid topicId)
        {
            var client = new RestClient(_aksBuildApiBaseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("topicview/{projectId}/{topicId}", Method.GET);
            request.AddUrlSegment("projectId", projectId); 
            request.AddUrlSegment("topicId", topicId); 

            // easily add HTTP Headers
            request.AddHeader("header", "value");

            var response = await client.ExecuteTaskAsync<TopicView>(request);
            var topic = response.Data;
            return topic;
        }

        public async Task<List<TopicList>> GetTopicListByProjectId(Guid projectId)
        {
            var client = new RestClient(_aksBuildApiBaseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("topicview/list/{projectId}", Method.GET);
            request.AddUrlSegment("projectId", projectId);

            // easily add HTTP Headers
            request.AddHeader("header", "value");

            var response = await client.ExecuteTaskAsync<List<TopicList>>(request);
            var topicList = response.Data;
            return topicList;
        }
    }
}
