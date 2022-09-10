using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KP.Common.Model.Automapper;
using KP.Common.Model.Models;
using KP.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace KP.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Core.DomainModels.Product> productRepository;
        private readonly IRepository<Core.DomainModels.Stock> stockRepository;

        public ProductService(IRepository<Core.DomainModels.Product> productRepository, IRepository<Core.DomainModels.Stock> stockRepository)
        {
            this.productRepository = productRepository;
            this.stockRepository = stockRepository;
        }

        public IEnumerable<ProductModel> GetProducts()
        {
            var products = productRepository.Table.Include(p => p.Stock).Select(x => x.ToModel()).ToList();
            return products;
        }

        public ProductModel GetProductById(Guid ProductId)
        {
            var product = productRepository.Table.Include(p => p.Stock).FirstOrDefault(x => x.ProductId == ProductId);
            return product.ToModel();


        }

        public void DeleteProduct(Guid ProductId)
        {
            var databaseEntity = productRepository.Table.FirstOrDefault(s => s.ProductId == ProductId);
            if (databaseEntity == null) return;
            var stockEntity = stockRepository.Table.FirstOrDefault(s => s.ProductId == ProductId);

            if(stockEntity != null)
            stockRepository.Delete(stockEntity);
            productRepository.Delete(databaseEntity);
            
        }

        public ProductModel GetProductByName(string Name)
        {
            var databaseEntity = productRepository.Table.FirstOrDefault(s => s.Name == Name);

            if (databaseEntity == null) return null;
            return databaseEntity.ToModel();


        }

        public ProductModel CreateProduct(ProductModel Product)
        {
            if (Product == null) return null;

            KP.Core.DomainModels.Product productEntity = Product.ToEntity();
            productRepository.Insert(productEntity);
            Product.Stock.ProductId = productEntity.ProductId;
            stockRepository.Insert(Product.Stock.ToEntity());

            ProductModel createdProduct = GetProductById(productEntity.ProductId);
            return createdProduct;
        }

        public ProductModel UpdateProduct(ProductModel Product)
        {
            if (Product == null) return null;
            var databaseEntity = productRepository.TableNoTracking.Include(p=>p.Stock).FirstOrDefault(s => s.ProductId == Product.ProductId);
            if (databaseEntity == null) return null;

            productRepository.Update(Product.ToEntity());
            if(Product.Stock.Quantity != databaseEntity.Stock.Quantity)
            {
                stockRepository.Update(Product.Stock.ToEntity());
            }
            return GetProductById(Product.ProductId);
        }
    }
}
