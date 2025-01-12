using desafio_api.Domain.Models;
using desafio_api.Infrastructure.Repository.Interface;

namespace desafio_api.Infrastructure.Repository;

public class CategoryRepository: ICategoryRepository
{
    private readonly ConnectionContext _context = new ConnectionContext();
    
    
    public void CreateCategory(CategoryModel category)
    {
       _context.Categories.Add(category);
       _context.SaveChanges();
    }

    public List<CategoryModel> getCategoryModels()
    {
        return _context.Categories.ToList();
    }
}