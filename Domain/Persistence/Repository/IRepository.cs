using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Persistence.Repository
{
    public interface IRepository<T>
    {
        public Task<T> Get(Expression<Func<User, bool>> predicate);
        public Task<List<T>> GetAll();
        public Task<List<T>> GetAll(Expression<Func<User, bool>> predicate);
        public Task Create(T entity);
        public void Remove(T entity);
        public void Update(T entity);
        public Task Save();
    }
}
