using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AKS.Share.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;
        public TopicController(ITopicService topciService)
        {
            _topicService = topciService;
        }
        // GET: api/Topic
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Topic/1234/555
        [HttpGet("{projectId:Guid}/{topicId:Guid}", Name = "Get")]
        public async Task< TopicEdit> Get(Guid projectId, Guid topicId)
        {
            return await _topicService.GetTopicForEdit(projectId, topicId);
        }

        // POST: api/Topic/1234/555
        [HttpPost]
        public async Task Post([FromBody] TopicEdit topic)
        {
            await _topicService.SaveTopic(topic);
        }

        // PUT: api/Topic/1234/555
        [HttpPut("{projectId:Guid}/{topicId:Guid}", Name = "Put")]
        public async Task Put([FromBody] TopicEdit topic)
        {
            await _topicService.SaveTopic(topic);
        }
    }
}
