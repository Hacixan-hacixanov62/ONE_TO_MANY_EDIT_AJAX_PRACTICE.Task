namespace ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Models
{
    public  abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool SoftDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
