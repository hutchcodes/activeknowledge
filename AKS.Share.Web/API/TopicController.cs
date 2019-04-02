using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.ViewModels;

namespace Resurgam.Admin.Web.Api
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
        public async Task< TopicEditViewModel> Get(Guid projectId, Guid topicId)
        {
            return await _topicService.GetTopicForEdit(projectId, topicId);
        }

        // POST: api/Topic/1234/555
        [HttpPost]
        public async Task Post([FromBody] TopicEditViewModel topic)
        {
            await _topicService.SaveTopic(topic);
        }

        // PUT: api/Topic/1234/555
        [HttpPut("{projectId:Guid}/{topicId:Guid}", Name = "Put")]
        public async Task Put([FromBody] TopicEditViewModel topic)
        {
            await _topicService.SaveTopic(topic);
        }

        // DELETE: api/Topic/1234/555
        [HttpDelete("{projectId:Guid}/{topicId:Guid}")]
        public void Delete(Guid projectId, Guid topicId)
        {
        }
    }
}
