using Microsoft.AspNetCore.Mvc;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Data.Entites;
using Store.Service;
using Store.Service.Customers;
using Store.Service.Customers.Commands;
using Store.Service.Products.Commands;
using System.Xml.Linq;

namespace Store.Api.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync([FromServices] IRequestHandler<IList<ProductResponse>> getProductsQuery)
        {
            return Ok(await getProductsQuery.Handle());
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByIdAsync(int productId, [FromServices] IRequestHandler<int, ProductResponse> getProductByIdQuery)
        {
            return Ok(await getProductByIdQuery.Handle(productId));
        }

        [HttpPost]
        public async Task<IActionResult> UpsertProductAsync([FromServices] IRequestHandler<UpsertProductCommand, ProductResponse> upsertProductCommand, [FromBody] UpsertProductRequest request)
        {
            var product = await upsertProductCommand.Handle(new UpsertProductCommand
            {
               ProductId = request.ProductId,
               CategoryId = request.CategoryId,
               Name = request.Name,
               Price = request.Price
            });
            
            return Ok(product);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductById(int productId, [FromServices] IRequestHandler<DeleteProductCommand, bool> deleteProductByIdCommand)
        {
            var result = await deleteProductByIdCommand.Handle(new DeleteProductCommand { ProductId = productId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
