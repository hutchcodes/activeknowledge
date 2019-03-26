using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;

namespace Resurgam.Admin.Api.Controllers
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
        [HttpGet("{projectId:int}/{topicId:int}", Name = "Get")]
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
        [HttpPut("{projectId:int}/{topicId:int}", Name = "Put")]
        public async Task Put([FromBody] TopicEditViewModel topic)
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
