using Catalog.API.Data.MongoDb;
using Catalog.API.Entities;
using Catalog.API.Repositories.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.ProductRepo
{
    public class ProductRepository : BaseRepository<Products>, IProductRepository
    {
        public ProductRepository(IMongoContext context) : base(context) {
        }

        public async Task CreateProduct(Products product)
        {
            product.Id = ObjectId.GenerateNewId().ToString();
            await DbSet.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Products> builder = Builders<Products>.Filter.Eq(p => p.Id, id);
            var dataResult = await DbSet.DeleteOneAsync(builder);
            return dataResult.IsAcknowledged && dataResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Products>> GetProducts()
        {
            var products = await DbSet
                .Find(p => true)
                .ToListAsync();
            return products;
        }

        public async Task<Products> GetProduct(string id)
        {
            return await DbSet
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Products>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Products> builder = Builders<Products>.Filter.Eq(p => p.Category, categoryName);
            return await DbSet
                .Find(builder)
                .ToListAsync();
        }

        public async Task<Products> GetProductByName(string name)
        {
            return await DbSet
                .Find(p => p.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateProduct(Products product)
        {
            var dataResult = await DbSet
                .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return dataResult.IsAcknowledged && dataResult.ModifiedCount > 0;
        }
    }
}
