using desafio_api.Domain.Models;

namespace desafio_api.Infrastructure.Repository.Interface;

public interface ICategoryRepository
{
    CategoryModel CreateCategory(CategoryModel category);

    List<CategoryModel> getCategoryModels();
    
    CategoryModel getCategoryModelById(Guid id);
    
    CategoryModel UpdateCategory(CategoryModel category);
    
    Boolean DeleteCategory(Guid id);
}