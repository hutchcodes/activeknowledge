﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AKS.Common.Models;
using AutoMapper.EntityFrameworkCore;
using AutoMapper;

namespace AKS.Infrastructure.Data
{
    public class TopicRepository : EfRepository<Infrastructure.Entities.Topic>
    {
        public TopicRepository(AKSContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
        public override async Task UpdateAsync(Infrastructure.Entities.Topic topic)
        {
            _dbContext.Entry(topic).State = EntityState.Modified;

            foreach(var ce in topic.CollectionElements)
            {
                _dbContext.Entry(ce).State = EntityState.Modified;
            }
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
