using desafio_api.Domain.Models;
using desafio_api.Infrastructure.Repository.Interface;

namespace desafio_api.Infrastructure.Repository;

public class CategoryRepository: ICategoryRepository
{
    private readonly ConnectionContext _context = new ConnectionContext();
    
    
    public CategoryModel CreateCategory(CategoryModel category)
    {
        var categoryExists = _context.Categories.FirstOrDefault(x => x.title == category.title && x.deleted_at == null);
        if(categoryExists != null)
        {
            throw new Exception("Category already exists");
        }
        _context.Categories.Add(category);
        _context.SaveChanges();
        return category;
    }

    public List<CategoryModel> getCategoryModels()
    {
        return _context.Categories.Where(x => x.deleted_at == null).ToList();
    }
    
    public CategoryModel? getCategoryModelById(Guid id)
    {
        return _context.Categories.FirstOrDefault(x => x.id == id && x.deleted_at == null);
    }
    
    public CategoryModel UpdateCategory(CategoryModel category)
    {
        var categoryExists = _context.Categories.FirstOrDefault(x => x.title == category.title && x.id != category.id && x.deleted_at == null);
        if(categoryExists != null)
        {
            throw new Exception("Category already exists");
        }
        _context.Categories.Update(category);
        _context.SaveChanges();
        return category;
    }
    
    public Boolean DeleteCategory(Guid id)
    {
        var category = _context.Categories.FirstOrDefault(x => x.id == id && x.deleted_at == null);
        if(category == null)
        {
            throw new Exception("Category not found");
        }
        category.DeleteCategory();
        _context.Categories.Update(category);
        _context.SaveChanges();
        return true;
    }
}