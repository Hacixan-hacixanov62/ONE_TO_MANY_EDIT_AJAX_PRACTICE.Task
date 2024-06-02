using System.ComponentModel.DataAnnotations;

namespace ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.ViewModels.Categories
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "This input can't be empty")]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
