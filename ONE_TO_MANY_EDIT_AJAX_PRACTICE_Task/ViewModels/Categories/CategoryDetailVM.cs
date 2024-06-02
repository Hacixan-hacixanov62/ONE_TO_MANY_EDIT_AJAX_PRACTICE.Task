namespace ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.ViewModels.Categories
{
    public class CategoryDetailVM
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int CourseCount { get; set; }
        public string CreateDate { get; set; }
        public ICollection<string> Courses { get; set; }
    }
}
