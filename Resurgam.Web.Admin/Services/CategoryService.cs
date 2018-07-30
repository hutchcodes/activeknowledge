using Resurgam.AppCore.Entities;
using Resurgam.AppCore.Interfaces;
using Resurgam.AppCore.Specifications;
using Resurgam.Infrastructure.Data;
using Resurgam.Web.Admin.Interfaces;
using Resurgam.Web.Admin.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Web.Admin.Services
{
    public class CategoryService : ITopicService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly IAsyncRepository<Category> _categoryRepo;
        public CategoryService(ILoggerFactory loggerFactory, IAsyncRepository<Category> categoryRepo)
        {
            _logger = loggerFactory.CreateLogger<CategoryService>();
            _categoryRepo = categoryRepo;
        }

        public async Task<TopicDisplayViewModel> GetTopicForDisplayAsync(int projectId, int topicId)
        {
            var spec = new TopicDisplaySpecification(projectId, 1);
            var topic = new Topic();// await _categoryRepo.GetAsync(spec);

            var topicVM = new TopicDisplayViewModel(topic);
            return topicVM;
        }

        public async Task<List<TopicListViewModel>> GetTopicListForProject(int projectId)
        {
            var spec = new TopicListSpecification(projectId);
            var topics = await _categoryRepo.ListAsync(null);

            var topicsVM = new List<TopicListViewModel>();

            //topicsVM.AddRange(topics.ConvertAll(x => new TopicListViewModel(x)));

            return topicsVM;
        }
    }
}
