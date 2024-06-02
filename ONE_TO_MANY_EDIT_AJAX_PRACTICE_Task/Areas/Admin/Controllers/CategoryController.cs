using Microsoft.AspNetCore.Mvc;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Data;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Helpers;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Helpers.Extensions;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Models;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Services.Interface;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.ViewModels.Categories;
using System.Reflection.Metadata;

namespace ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Areas.Areas.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CategoryController( ICategoryService categoryService,
                                     AppDbContext context,
                                     IWebHostEnvironment env)
        {
            _categoryService = categoryService;
            _context = context;
            _env = env;

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool existCategory = await _categoryService.ExistAsync(category.Name);

            if (existCategory)
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }


            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!category.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Input can accept only image format");
                return View();
            }

            if (!category.Image.CheckFileSize(200))
            {
                ModelState.AddModelError("Image", "Image size must be max 200 KB");
                return View();
            }

            bool existBlog = await _categoryService.ExistAsync(category.Name);

            if (existBlog)
            {
                ModelState.AddModelError("Name", "Blog with this title or description already exists");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + category.Image.FileName;

            string path = _env.GenerateFilePath("assets/images", fileName);

            await category.Image.SaveFileToLocalAsync(path);

            await _categoryService.CreateAsync(new Category { Name = category.Name,Description = category.Description,Image = fileName });

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var categories = await _categoryService.GetAllPaginateAsync(page, 3);

            var mappedDatas = _categoryService.GetMappedDatas(categories);

            int totalPage = await GetPageCountAsync(3);

            Paginate<CategoryCourseVM> paginateDatas = new(mappedDatas, totalPage, page);

            return View(paginateDatas);
        }
        private async Task<int> GetPageCountAsync(int take)
        {
            int CategoryCount = await _categoryService.GetCountAsync();
            return (int)Math.Ceiling((decimal)CategoryCount / take);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var blog = await _categoryService.GetByIdAsync((int)id);

            if (blog is null) return NotFound();

            await _categoryService.DeleteAsync(blog.Id);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();

            var category = await _categoryService.GetByIdWithCoursesAsync((int)id);

            if (category is null) return NotFound();

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var category = await _categoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            return View(new CategoryEditVM { Name = category.Name,
                                             Image = category.Image,
                                             Description = category.Description });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if (id is null) return BadRequest();

            if (await _categoryService.ExistExceptByIdAsync((int)id, request.Name))
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }

            var category = await _categoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            if (!ModelState.IsValid)
            {
                request.Image = category.Image;
                return View(request);
            }

            if (request.NewImages is not null)
            {
                if (!request.NewImages.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "Input can accept only image format");
                    request.Image = category.Image;
                    return View(request);
                }

                if (!request.NewImages.CheckFileSize(200))
                {
                    ModelState.AddModelError("NewImage", "Image size must be max 200 KB");
                    request.Image = category.Image;
                    return View(request);
                }
            }

            if (request.NewImages is not null)
            {
                string oldPath = _env.GenerateFilePath("assets/images", category.Image);

                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImages.FileName;

                string newPath = _env.GenerateFilePath("assets/images", fileName);

                await request.NewImages.SaveFileToLocalAsync(newPath);

                category.Image = fileName;
            }


            category.Name = request.Name;
            category.Description = request.Description;


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}












