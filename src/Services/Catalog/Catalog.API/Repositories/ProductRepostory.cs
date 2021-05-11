using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepostory : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepostory(ICatalogContext context)
        {
            _context = context?? throw new ArgumentNullException();
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            var upateResult = await _context.Products.DeleteOneAsync(filter: g => g.Id == product.Id);
            return upateResult.IsAcknowledged && upateResult.DeletedCount > 0;
        }

        public async Task<IEnumerable< Product>> GetProductByCategory(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _context.Products.Find(p => p.Id==id).FirstOrDefaultAsync();
        }

        public async Task<Product> GetproductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _context.Products.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var upateResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return upateResult.IsAcknowledged && upateResult.ModifiedCount > 0;
        }

        
    }
}
