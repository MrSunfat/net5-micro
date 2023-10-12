using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.Base
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> Add(TEntity obj);
        Task<TEntity> GetById(string id);
        Task<IEnumerable<TEntity>> GetAll();
        //Task<TEntity> Update(string id, TEntity obj);
        Task<bool> Remove(string id);
    }
}
