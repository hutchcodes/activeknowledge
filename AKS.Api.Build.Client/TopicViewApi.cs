using AKS.Common;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.Api.Build.Client
{
    public class TopicViewApi
    {
        private readonly HttpClient _http;

        public TopicViewApi(HttpClient http)
        {
            _http = http;
        }
        public async Task<TopicView> GetTopic(Guid projectId, Guid topicId)
        {
            var topic = await _http.GetJsonAsync<TopicView>($"topicview/{projectId}/{topicId}");
            return topic;
        }

        public async Task<List<TopicList>> GetTopicListByProjectId(Guid projectId)
        {
            var topicList = await _http.GetJsonAsync<List<TopicList>>($"topicview/list/{projectId}");
            return topicList;
        }

        public async Task<List<TopicList>> SearchProjectTopics(Guid projectId, Guid? categoryId, string search)
        {
            var resource = $"topicview/search/{projectId}/{search}";
            if (categoryId.HasValue)
            {
                resource += $"?categoryId=categoryId";
            }

            var topicList = await _http.GetJsonAsync<List<TopicList>>(resource);
            return topicList;
        }
    }
}
