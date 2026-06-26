using Microsoft.AspNetCore.Mvc;
using TransactionService.Api.Dtos;
using TransactionService.Api.Services;

namespace TransactionService.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponse>> Create(CreateCategoryRequest request)
    {
        try
        {
            var category = await _categoryService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = category.Id, userId = category.UserId }, category);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryResponse>>> GetByUser([FromQuery] int userId)
    {
        try
        {
            return await _categoryService.GetByUserAsync(userId);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryResponse>> GetById(int id, [FromQuery] int userId)
    {
        try
        {
            var category = await _categoryService.GetByIdAsync(id, userId);
            return category == null ? NotFound() : category;
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoryResponse>> Update(int id, UpdateCategoryRequest request)
    {
        try
        {
            var category = await _categoryService.UpdateAsync(id, request);
            return category == null ? NotFound() : category;
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
    {
        try
        {
            var deleted = await _categoryService.DeleteAsync(id, userId);
            return deleted ? NoContent() : NotFound();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
