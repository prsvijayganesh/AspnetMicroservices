using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool exists = productCollection.Find(p => true).Any();
            if(!exists)
            {
                productCollection.InsertManyAsync(GetPreConfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreConfiguredProducts()
        {
            return new List<Product>()
            {
                new Product(){Id="12323234343",Name="Poco M3",Category="Phone",Price=12000},
                new Product(){Id="45678910sdf",Name="Samsung M12",Category="Phone",Price=14000}
            };
            
        }
    }
}
