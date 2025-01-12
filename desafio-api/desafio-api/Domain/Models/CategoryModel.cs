using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace desafio_api.Domain.Models;

[Table("category")]
public class CategoryModel
{
 
    [Key]
    public Guid id { get; private init; }
    public string title { get; private set; }
    public string description { get; private set; }
    public string code { get; private set; }
    public DateTime created_at { get; private set; }
    public DateTime? updated_at { get; private set; }
    public DateTime? deleted_at { get; private set; }
    
    public CategoryModel(string title, string description, string code)
    {
        this.id = Guid.NewGuid();
        this.title = title;
        this.description = description;
        this.code = code;
        this.created_at = DateTime.UtcNow;
    }
    
    public void UpdateCategory(string title, string description, string code)
    {
        this.title = title;
        this.description = description;
        this.code = code;
        this.updated_at = DateTime.UtcNow;
    }
    
    public void DeleteCategory()
    {
        this.deleted_at = DateTime.UtcNow;
    }
    
}