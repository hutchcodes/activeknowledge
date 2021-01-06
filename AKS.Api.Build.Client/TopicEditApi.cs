using AKS.Common;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.Api.Build.Client
{
    public class TopicEditApi
    {
        private readonly HttpClient _http;

        public TopicEditApi(HttpClient http)
        {
            _http = http;
        }
        
        public async Task<TopicEdit> GetTopic(Guid projectId, Guid topicId)
        {
            var topic = await _http.GetJsonAsync<TopicEdit> ($"api/topicedit/{projectId}/{topicId}");

            return topic;
        }

        public async Task<TopicEdit> UpdateTopic(TopicEdit topicEdit)
        {
            topicEdit = await _http.PostJsonAsync<TopicEdit>($"api/topicedit", topicEdit);
            return topicEdit;
        }

        public async Task DeleteTopic(Guid projectId, Guid topicId)
        {
            var response = await _http.DeleteAsync($"api/topicedit/{projectId}/{topicId}");
            return;
        }
    }
}
