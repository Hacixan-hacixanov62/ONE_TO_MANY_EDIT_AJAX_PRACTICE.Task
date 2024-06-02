using Microsoft.EntityFrameworkCore;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Data;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Helpers.Extensions;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Models;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Services.Interface;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.ViewModels.Categories;

namespace ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Services
{
    public class CategoryService : ICategoryService
    {
        public readonly AppDbContext _context;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryService(AppDbContext context, 
                               IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }

        public async Task CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            string imgPath = _webHostEnvironment.GenerateFilePath("assets/images", category.Image);
            imgPath.DeleteFileFromLocal();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, CategoryEditVM request)
        {

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            category.Name = request.Name;

            category.Description = request.Description;

            if (request.NewImages is not null)
            {
                string oldPath = _webHostEnvironment.GenerateFilePath("img", category.Image);

                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImages.FileName;

                string newPath = _webHostEnvironment.GenerateFilePath("img", fileName);

                await request.NewImages.SaveFileToLocalAsync(newPath);

                category.Image = fileName;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Categories.AnyAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task<bool> ExistExceptByIdAsync(int id, string name)
        {
            return await _context.Categories.AnyAsync(m => m.Id != id && m.Name.Trim() == name.Trim() );
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllPaginateAsync(int page, int take)
        {
            return await _context.Categories.Include(m => m.Courses)
                                              .OrderByDescending(m=>m.Id)
                                              .Skip((page - 1) * take)
                                              .Take(take)
                                              .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllWithCoursesAsync()
        {
            return await _context.Categories.Include(m=>m.Courses).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<CategoryDetailVM> GetByIdWithCoursesAsync(int id)
        {
            var category = await _context.Categories
               .Where(m => m.Id == id)
               .Include(m => m.Courses)
               .FirstOrDefaultAsync();

            return new CategoryDetailVM
            {
                Name = category.Name,
                Courses = category.Courses .Select(m => m.Name).ToList(),
                Image = category.Image,
                Description = category.Description,
                CourseCount = category.Courses.Count,
                CreateDate = category.CreatedDate.ToString("MM.dd.yyyy"),
            };
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public IEnumerable<CategoryCourseVM> GetMappedDatas(IEnumerable<Category> categories)
        {
            return categories.Select(m => new CategoryCourseVM
            {
                Id = m.Id,
                Name = m.Name,
                Image = m.Image,
                Description = m.Description,
                CourseCount =m.Courses.Count
         
            });
        }

    }
}
