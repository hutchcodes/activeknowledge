using System;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AKS.Api.Build.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TopicEditController : ControllerBase
    {
        private readonly ITopicService _topicService;
        public TopicEditController(ITopicService topciService)
        {
            _topicService = topciService;
        }

        // GET: api/Topic/1234/555
        [HttpGet("{projectId:Guid}/{topicId:Guid}", Name = "GetTopicEdit")]
        public async Task<TopicEdit> Get(Guid projectId, Guid topicId)
        {
            return await _topicService.GetTopicForEdit(projectId, topicId);
        }

        // POST: api/Topic/1234/555
        [HttpPost]
        public async Task<TopicEdit> Post([FromBody] TopicEdit topic)
        {
            return await _topicService.SaveTopic(topic);
        }

        // PUT: api/Topic/1234/555
        [HttpPut("{projectId:int}/{topicId:int}", Name = "Put")]
        public async Task Put([FromBody] TopicEdit topic)
        {
            await _topicService.SaveTopic(topic);
        }

        // DELETE: api/Topic/1234/555
        [HttpDelete("{projectId:Guid}/{topicId:Guid}")]
        public async Task Delete(Guid projectId, Guid topicId)
        {
            await _topicService.DeleteTopic(projectId, topicId);
        }
    }
}
