using desafio_api.Application.ViewModel;
using desafio_api.Domain.Models;
using desafio_api.Infrastructure.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace desafio_api.Controllers;

[ApiController]
[Route("api/v1/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryController> _logger;
    
    public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    public IActionResult CreateCategory(CategoryViewModel categoryView)
    {
        try
        {
            var category = new CategoryModel(categoryView.title, categoryView.description, categoryView.code);
            var createdCategory = _categoryRepository.CreateCategory(category);

            if (createdCategory.id == Guid.Empty)
            {
                _logger.LogError("Route - CreateCategory: Category not created");
                return BadRequest(new { message = "Category not created", StatusCode = 400 });
            }

            _logger.LogInformation("Route - CreateCategory: Category created");
            return Ok(new { message = "Category created", StatusCode = 200, category = createdCategory });
        }
        catch (Exception ex) when (ex.Message.Contains("Title is already in use"))
        {
            _logger.LogWarning(ex, "Route - CreateCategory: Title already exists");
            return Conflict(new { message = ex.Message, StatusCode = 409 });
        }
        catch(Exception ex) when (ex.Message.Contains("Code is already in use"))
        {
            _logger.LogWarning(ex, "Route - CreateCategory: Code already exists");
            return Conflict(new { message = ex.Message, StatusCode = 409 });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Route - CreateCategory: An unexpected error occurred");
            return StatusCode(500, new { message = "An unexpected error occurred", StatusCode = 500 });
        }
    }
    
    [HttpGet]
    public IActionResult GetCategories()
    {
        try 
        {
            var categories = _categoryRepository.getCategoryModels();
            
            if(categories.Count == 0)
            {
                _logger.LogWarning("Route - GetCategories: No categories found");
                return NoContent();
            }
            
            _logger.LogInformation("Route - GetCategories: Categories found");
            return Ok(new {message = "Categories found", StatusCode = 200, categories = categories});
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Route - GetCategories: An unexpected error occurred");
            return StatusCode(500, new {message = "An unexpected error occurred", StatusCode = 500});
        }
    }
    
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetCategoryById(Guid id)
    {
        try
        {
            var category = _categoryRepository.getCategoryModelById(id);

            if (category == null)
            {
                _logger.LogWarning("Route - GetCategoryById: Category not found");
                return NoContent();
            }

            _logger.LogInformation("Route - GetCategoryById: Category found");
            return Ok(new { message = "Category found", StatusCode = 200, category = category });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Route - GetCategoryById: An unexpected error occurred");
            return StatusCode(500, new { message = "An unexpected error occurred", StatusCode = 500 });
        }
    }
    
    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateCategory(Guid id, CategoryViewModel categoryView)
    {
        try
        {
            var category = _categoryRepository.getCategoryModelById(id);

            if (category == null)
            {
                _logger.LogWarning("Route - UpdateCategory: Category not found");
                return NoContent();
            }

            category.UpdateCategory(categoryView.title, categoryView.description, categoryView.code);
            var updatedCategory = _categoryRepository.UpdateCategory(category);

            if (updatedCategory.id == Guid.Empty)
            {
                _logger.LogError("Route - UpdateCategory: Category not updated");
                return BadRequest(new { message = "Category not updated", StatusCode = 400 });
            }

            _logger.LogInformation("Route - UpdateCategory: Category updated");
            return Ok(new { message = "Category updated", StatusCode = 200, category = updatedCategory });
        }
        catch (Exception ex) when (ex.Message.Contains("already exists"))
        {
            _logger.LogWarning(ex, "Route - UpdateCategory: Title already exists");
            return Conflict(new { message = ex.Message, StatusCode = 409 });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Route - UpdateCategory: An unexpected error occurred");
            return StatusCode(500, new { message = "An unexpected error occurred", StatusCode = 500 });
        }
    }
    
    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteCategory(Guid id)
    {
        try
        {
            var deleted = _categoryRepository.DeleteCategory(id);

            if (!deleted)
            {
                _logger.LogError("Route - DeleteCategory: Category not deleted");
                return BadRequest(new { message = "Category not deleted", StatusCode = 400 });
            }

            _logger.LogInformation("Route - DeleteCategory: Category deleted");
            return Ok(new { message = "Category deleted", StatusCode = 200 });
        }
        catch (Exception ex) when (ex.Message.Contains("not found"))
        {
            _logger.LogWarning(ex, "Route - DeleteCategory: Category not found");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Route - DeleteCategory: An unexpected error occurred");
            return StatusCode(500, new { message = "An unexpected error occurred", StatusCode = 500 });
        }
    }
    
}