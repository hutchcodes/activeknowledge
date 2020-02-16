using AKS.Infrastructure.Entities;
using AKS.Infrastructure.Interfaces;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKS.Infrastructure.Data
{
    public class EfRepository<T> : IAsyncRepository<T> where T: BaseEntity
    {
        protected readonly AKSContext _dbContext;
        private readonly IMapper _mapper;

        public EfRepository(AKSContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<T> GetAsync(ISpecification<T> spec)
        {
            IQueryable<T> secondaryResult = ApplyIncludeFromSpecification(spec);

            // return the result of the query using the specification's criteria expression
            return await secondaryResult
                            .FirstOrDefaultAsync(spec.Criteria);
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> ListAsync(ISpecification<T> spec)
        {
            IQueryable<T> secondaryResult = ApplyIncludeFromSpecification(spec);

            // return the result of the query using the specification's criteria expression
            return await secondaryResult
                            .Where(spec.Criteria)
                            .ToListAsync();
        }

        private IQueryable<T> ApplyIncludeFromSpecification(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));
            return secondaryResult;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync<TFrom>(TFrom model)
        {
            _dbContext.Set<T>().Persist(_mapper).InsertOrUpdate(typeof(TFrom), model);
            await _dbContext.SaveChangesAsync();
        }

    }
}
