using KP.Common.Model.Automapper;
using KP.Common.Model.Models;
using KP.Core.Data;
using KP.Core.DomainModels;
using KP.Services.Product;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace KP.Tests
{
    public class ProductServiceTests
    {

        private EFCoreRepository<Product> productRepository;
        private EFCoreRepository<Stock> stockRepository;

        private ProductService GetService()
        {
            return new(productRepository, stockRepository);
        }

        private Product CreateProductModel(string name, string description, double price, string producer)
        {
            KP.Core.DomainModels.Product product = new()
            {
                Name = name,
                Description = description,
                Price = price,
                Producer = producer,
            };

            KP.Core.DomainModels.Stock stock = new()
            {
                Quantity = 10
            };

            product.Stock = stock;

            return product;
        }



        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PharmacyContext>();
            options.UseSqlServer("Server=.\\SQLEXPRESS;Database=Pharmacy;Trusted_Connection=True;");
            PharmacyContext _dbContext = new PharmacyContext(options.Options);

            //Set up Repos
            productRepository = new(_dbContext);
            stockRepository = new(_dbContext);

            //Set up Automapper
            AutoMapperConfiguration.Init();
            AutoMapperConfiguration.MapperConfiguration.AssertConfigurationIsValid();

        }

        [Test]
        public void GetProductTest()
        {
            //arrange
            ProductService service = GetService();

            //act
            var products = service.GetProducts();

            //assert
            Assert.That(products.Any());
        }

        [Test]
        public void CreateProductTest()
        {
            //arrange
            ProductService service = GetService();
            Guid createdProductId = Guid.Empty;
            try
            {

                Product product = CreateProductModel("TestName", "TestDescription", 15, "Producertest");

                //act
                ProductModel createdProduct = service.CreateProduct(product.ToModel());
                createdProductId = createdProduct.ProductId;

                //assert
                Assert.That(createdProduct != null);
                Assert.That(createdProduct?.Name == product.Name);
                Assert.That(createdProduct?.Price == product.Price);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                service.DeleteProduct(createdProductId);
            }
        }

        [Test]
        public void DeleteProductTest()
        {
            //arrange
            ProductService service = GetService();
            ProductModel product = CreateProductModel("TestName", "TestDescription", 15, "Producertest").ToModel();
            ProductModel createdProduct= service.CreateProduct(product);

            //act
            service.DeleteProduct(createdProduct.ProductId);

            //assert
            var deletedStudent = service.GetProductById(createdProduct.ProductId);
            Assert.That(deletedStudent == null);
        }
    }
}