﻿namespace ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
