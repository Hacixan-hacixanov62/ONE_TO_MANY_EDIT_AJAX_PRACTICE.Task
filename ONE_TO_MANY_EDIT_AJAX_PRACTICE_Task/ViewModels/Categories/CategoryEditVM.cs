using System.ComponentModel.DataAnnotations;

namespace ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.ViewModels.Categories
{
    public class CategoryEditVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "This input can't be empty")]
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile NewImages { get; set; }
    }
}
