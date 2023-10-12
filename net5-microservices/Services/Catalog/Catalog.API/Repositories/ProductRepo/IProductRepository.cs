using Catalog.API.Entities;
using Catalog.API.Repositories.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.ProductRepo
{
    public interface IProductRepository : IBaseRepository<Products>
    {
        Task<IEnumerable<Products>> GetProducts();

        Task<Products> GetProduct(string id);

        Task<Products> GetProductByName(string name);

        Task<IEnumerable<Products>> GetProductByCategory(string categoryName);

        Task CreateProduct(Products product);

        Task<bool> UpdateProduct(Products product);

        Task<bool> DeleteProduct(string id);
    }
}
