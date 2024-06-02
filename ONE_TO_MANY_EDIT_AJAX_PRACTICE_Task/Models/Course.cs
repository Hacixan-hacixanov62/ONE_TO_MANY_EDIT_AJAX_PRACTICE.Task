namespace ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Models
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Description{ get; set; }
        public decimal Price { get; set; }  
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<CourseImage> CourseImages { get; set; }
    }
}
