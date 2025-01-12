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
        var category = new CategoryModel(categoryView.title, categoryView.description, categoryView.code);
        var created_category = _categoryRepository.CreateCategory(category);
        
        if(created_category.id == Guid.Empty)
        {
            _logger.LogError("Route - CreateCategory: Category not created");
            return BadRequest(new {message = "Category not created", StatusCode = 400});
        }
        
        _logger.LogInformation("Route - CreateCategory: Category created");
        return Ok(new {message = "Category created", StatusCode = 200, category = created_category});
    }
    
    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _categoryRepository.getCategoryModels();
        
        if(categories.Count == 0)
        {
            _logger.LogWarning("Route - GetCategories: Categories not found");
            return NoContent();
        }
        
        _logger.LogInformation("Route - GetCategories: Categories found");
        return Ok(new {message = "Categories found", StatusCode = 200, categories = categories});
    }
    
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetCategoryById(Guid id)
    {
        var category = _categoryRepository.getCategoryModelById(id);
        
        if(category == null)
        {
            _logger.LogWarning("Route - GetCategoryById: Category not found");
            return NoContent();
        }
        
        _logger.LogInformation("Route - GetCategoryById: Category found");
        return Ok(new {message = "Category found", StatusCode = 200, category = category});
    }
    
    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateCategory(Guid id, CategoryViewModel categoryView)
    {
        var category = _categoryRepository.getCategoryModelById(id);
        
        if(category == null)
        {
            _logger.LogWarning("Route - UpdateCategory: Category not found");
            return NoContent();
        }
        
        category.UpdateCategory(categoryView.title, categoryView.description, categoryView.code);
        var update_category = _categoryRepository.UpdateCategory(category);
        
        if(update_category.id == Guid.Empty)
        {
            _logger.LogError("Route - UpdateCategory: Category not updated");
            return BadRequest(new {message="Category not updated", StatusCode = 400});
        }
        
        _logger.LogInformation("Route - UpdateCategory: Category updated");
        return Ok(new {message = "Category updated", StatusCode = 200, category = update_category});
    }
    
    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteCategory(Guid id)
    {
        var category = _categoryRepository.getCategoryModelById(id);
        
        if(category == null)
        {
            _logger.LogWarning("Route - DeleteCategory: Category not found");
            return NoContent();
        }
        
        var deleted = _categoryRepository.DeleteCategory(id);
        
        if(!deleted)
        {
            _logger.LogError("Route - DeleteCategory: Category not deleted");
            return BadRequest(new {message = "Category not deleted", StatusCode = 400});
        }
        
        _logger.LogInformation("Route - DeleteCategory: Category deleted");
        return Ok(new {message = "Category deleted", StatusCode = 200});
    }
    
}