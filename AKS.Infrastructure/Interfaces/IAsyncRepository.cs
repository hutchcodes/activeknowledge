using AKS.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AKS.Infrastructure.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(ISpecification<T> spec);
        Task<List<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task UpdateAsync<TFrom>(TFrom model) where TFrom : class;
        Task DeleteAsync(T entity);
        Task DeleteAsync<TFrom>(TFrom entity) where TFrom : class;
    }
}
