using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Models;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.ViewModels.Categories;

namespace ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Services.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetAllWithCoursesAsync();
        Task<IEnumerable<Category>> GetAllPaginateAsync(int page, int take);
        IEnumerable<CategoryCourseVM> GetMappedDatas(IEnumerable<Category> categories);
        Task<int> GetCountAsync();
        Task CreateAsync(Category category);
        Task DeleteAsync(int id);
        Task<bool> ExistAsync(string name);
        Task<Category> GetByIdAsync(int id);
        Task<CategoryDetailVM> GetByIdWithCoursesAsync(int id);
        Task EditAsync(int id, CategoryEditVM request);
        Task<bool> ExistExceptByIdAsync(int id, string name);

    }
}
