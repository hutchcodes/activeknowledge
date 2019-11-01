using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AKS.Api.Build.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicViewController : ControllerBase
    {
        private readonly ITopicService _topicService;
        public TopicViewController(ITopicService topciService)
        {
            _topicService = topciService;
        }

        [HttpGet("list/{projectId:Guid}")]
        public async Task<List<TopicList>> GetTopicsByProjectId(Guid projectId)
        {
            var topicList = await _topicService.GetTopicListForProject(projectId);
            return topicList;
        }

        // GET: api/TopicView/1234/555
        [HttpGet("{projectId:Guid}/{topicId:Guid}")]
        public async Task<TopicView> GetTopicView(Guid projectId, Guid topicId)
        {
            var topic = await _topicService.GetTopicForDisplay(projectId, topicId);            
            return topic;
        }

        [HttpGet("search/{projectId:Guid}")]
        [HttpGet("search/{projectId:Guid}/{search}")]
        public async Task<List<TopicList>> SearchTopics(Guid projectId, Guid? categoryId, string search = "")
        {
            var topicList = await _topicService.SearchTopics(projectId, categoryId, search);
            return topicList;
        }
    }
}
