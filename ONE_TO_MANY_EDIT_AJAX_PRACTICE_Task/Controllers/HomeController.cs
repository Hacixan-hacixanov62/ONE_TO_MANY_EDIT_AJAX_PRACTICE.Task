using Microsoft.AspNetCore.Mvc;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Services.Interface;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.ViewModels.Categories;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.ViewModels.Home;
using System.Diagnostics;

namespace ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        public HomeController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public  async Task<IActionResult> Index()
        {
            var datas = await _categoryService.GetAllAsync();

            HomeVM response = new HomeVM();

            response.Categories = datas.Select(m => new CategoryVM
            {
                Id = m.Id,
                Name = m.Name,
                Image = m.Image,
                Description = m.Description

            });
            
            return View(response);
        }

       





    }
}
