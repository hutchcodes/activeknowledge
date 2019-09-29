using System;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AKS.App.Build.Api.Controllers
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

        // GET: api/Topic/1234/555
        [HttpGet("{projectId:Guid}/{topicId:Guid}", Name = "Get")]
        public async Task<TopicEdit> Get(Guid projectId, Guid topicId)
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
        [HttpPut("{projectId:int}/{topicId:int}", Name = "Put")]
        public async Task Put([FromBody] TopicEdit topic)
        {
            await _topicService.SaveTopic(topic);
        }

        // DELETE: api/Topic/1234/555
        [HttpDelete("{projectId:int}/{topicId:int}")]
        public void Delete(Guid projectId, Guid topicId)
        {
            _topicService.DeleteTopic(projectId, topicId);
        }
    }
}
