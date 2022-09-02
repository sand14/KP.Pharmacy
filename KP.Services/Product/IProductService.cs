using KP.Common.Model.Models;

namespace KP.Services.Product
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetProducts();
        ProductModel GetProductById(Guid ProductId);
        void DeleteProduct(Guid ProductId);
        ProductModel GetProductByName(string Name);
        ProductModel CreateProduct(ProductModel Product);
        ProductModel UpdateProduct(ProductModel Product);
    }
}
