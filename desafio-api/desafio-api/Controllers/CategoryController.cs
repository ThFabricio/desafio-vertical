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
        _categoryRepository.CreateCategory(category);
        
        if(category.id == Guid.Empty)
        {
            _logger.LogError("Category not created");
            return BadRequest();
        }
        
        _logger.LogInformation("Category created");
        return Ok();
    }
    
    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _categoryRepository.getCategoryModels();
        
        if(categories.Count == 0)
        {
            _logger.LogWarning("No categories found");
            return NoContent();
        }
        
        _logger.LogInformation("Categories found");
        return Ok(categories);
    }
    
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetCategoryById(Guid id)
    {
        var category = _categoryRepository.getCategoryModelById(id);
        
        if(category == null)
        {
            _logger.LogWarning("Category not found");
            return NoContent();
        }
        
        _logger.LogInformation("Category found");
        return Ok(category);
    }
    
    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateCategory(Guid id, CategoryViewModel categoryView)
    {
        var category = _categoryRepository.getCategoryModelById(id);
        
        if(category == null)
        {
            _logger.LogWarning("Category not found");
            return NoContent();
        }
        
        category.UpdateCategory(categoryView.title, categoryView.description, categoryView.code);
        _categoryRepository.UpdateCategory(category);
        
        if(category.id == Guid.Empty)
        {
            _logger.LogError("Category not updated");
            return BadRequest();
        }
        
        _logger.LogInformation("Category updated");
        return Ok();
    }
    
    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteCategory(Guid id)
    {
        _categoryRepository.DeleteCategory(id);
        return Ok();
    }
    
}