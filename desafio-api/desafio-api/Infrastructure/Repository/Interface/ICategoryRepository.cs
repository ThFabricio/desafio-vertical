using desafio_api.Domain.Models;

namespace desafio_api.Infrastructure.Repository.Interface;

public interface ICategoryRepository
{
    void CreateCategory(CategoryModel category);

    List<CategoryModel> getCategoryModels();
    
    CategoryModel getCategoryModelById(Guid id);
    
    void UpdateCategory(CategoryModel category);
}