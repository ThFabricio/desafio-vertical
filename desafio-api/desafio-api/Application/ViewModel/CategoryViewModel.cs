using System.ComponentModel.DataAnnotations;

namespace desafio_api.Application.ViewModel;

public class CategoryViewModel
{
    [Required (ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title must have a maximum of 100 characters")]
    public string title { get; set; }
    [Required (ErrorMessage = "Description is required")]
    [StringLength(255, ErrorMessage = "Description must have a maximum of 255 characters")]
    public string description { get; set; }
    [Required (ErrorMessage = "Code is required")]
    [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Code must have only letters and numbers")]
    public string code { get; set; }
}