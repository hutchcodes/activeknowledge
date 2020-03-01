using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKS.Infrastructure.Entities;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.Specifications;
using AKS.Common.Models;
using AKS.Common.Extensions;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AKS.Infrastructure.Services
{
    public class CategoryTopicService : ICategoryTopicService
    {
        private readonly ILogger<CategoryTopicService> _logger;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<CategoryTopic> _categoryTopicRepo;

        public CategoryTopicService(IMapper mapper, ILoggerFactory loggerFactory, IAsyncRepository<CategoryTopic> categoryTopcicRepo)
        {
            _logger = loggerFactory.CreateLogger<CategoryTopicService>();
            _mapper = mapper;
            _categoryTopicRepo = categoryTopcicRepo;
            _logger.LogDebug($"New instance of {GetType().Name} was created");
        }

       

        public async Task SaveCategoryTopicsAsync(List<CategoryTopicList> topics)
        {
            try
            {
                foreach (var t in topics)
                {
                    await _categoryTopicRepo.UpdateAsync(t);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteCategoryTopicAsync(Guid projectId, Guid categoryId, Guid topicId)
        {
            var topicSpec = new CategoryTopicSpecification(projectId, categoryId, topicId);
            var topic = await _categoryTopicRepo.ListAsync(topicSpec);

            foreach (var t in topic)
            {
                await _categoryTopicRepo.DeleteAsync(t);
            }            
        }
    }
}
