namespace ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Models
{
    public class CourseImage : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Course Course { get; set; }
    }
}
