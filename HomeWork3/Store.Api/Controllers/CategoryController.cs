using Microsoft.AspNetCore.Mvc;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Service;
using Store.Service.Categories;
using Store.Service.Categories.Commands;

namespace Store.Api.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync([FromServices] IRequestHandler<IList<CategoryResponse>> getCategoriesQuery)
        {
            return Ok(await getCategoriesQuery.Handle());
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int categoryId, [FromServices] IRequestHandler<int, CategoryResponse> getCategoryByIdQuery)
        {
            return Ok(await getCategoryByIdQuery.Handle(categoryId));
        }

        [HttpPost]
        public async Task<IActionResult> UpsertCategoryAsync([FromServices] IRequestHandler<UpsertCategoryCommand, CategoryResponse> upsertMovieCommand, [FromBody] UpsertCategoryRequest request)
        {
            var category = await upsertMovieCommand.Handle(new UpsertCategoryCommand
            {
               CategoryId = request.CategoryId,
               Name = request.Name,
            });

            return Ok(category);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategoryById(int categoryId, [FromServices] IRequestHandler<DeleteCategoryCommand, bool> deleteCategoryByIdCommand)
        {
            var result = await deleteCategoryByIdCommand.Handle(new DeleteCategoryCommand { CategoryId = categoryId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
