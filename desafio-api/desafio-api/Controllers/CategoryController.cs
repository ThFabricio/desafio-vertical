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
    
    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    [HttpPost]
    public IActionResult CreateCategory(CategoryViewModel categoryView)
    {
        var category = new CategoryModel(categoryView.title, categoryView.description, categoryView.code);
        _categoryRepository.CreateCategory(category);
        return Ok();
    }
    
    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _categoryRepository.getCategoryModels();
        return Ok(categories);
    }
    
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetCategoryById(Guid id)
    {
        var category = _categoryRepository.getCategoryModelById(id);
        return Ok(category);
    }
    
    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateCategory(Guid id, CategoryViewModel categoryView)
    {
        var category = _categoryRepository.getCategoryModelById(id);
        category.UpdateCategory(categoryView.title, categoryView.description, categoryView.code);
        _categoryRepository.UpdateCategory(category);
        return Ok();
    }
    
}