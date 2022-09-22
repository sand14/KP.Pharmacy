using KP.Common.Model.Models;
using KP.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KP.Web.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        [Route("/api/Products")]
        [HttpGet]
        public IEnumerable<ProductModel> GetProducts()
        {
            try
            {
                var products = productService.GetProducts();
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Products/{ProductId}")]
        [HttpGet]
        public ProductModel GetProductById(Guid productId)
        {
            try
            {
                var product = productService.GetProductById(productId);

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Products/{ProductId}")]
        [HttpDelete]
        public void DeleteProduct(Guid productId)
        {
            try
            {
                productService.DeleteProduct(productId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Products/{productId}")]
        [HttpPut]
        public ProductModel UpdateProduct(Guid productId, [FromBody] ProductModel product)
        {
            try
            {
                ProductModel updateProduct = productService.UpdateProduct(product);
                return updateProduct;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Products")]
        [HttpPost]
        public ProductModel Create([FromBody] ProductModel product)
        {
            try
            {
                //var studentModel = JsonSerializer.Deserialize<StudentModel>(student);
                ProductModel createdProduct = productService.CreateProduct(product);
                return createdProduct;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
