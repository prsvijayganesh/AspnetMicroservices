﻿using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(string id);
        Task<Product> GetproductByName(string name);
        Task<IEnumerable<Product>> GetProductByCategory(string category);
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Product product);
    }
}
