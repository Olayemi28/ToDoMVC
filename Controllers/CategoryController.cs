using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Controllers
{
     [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }

        public async Task<IActionResult> GetAllCategory()
        {
            var category = await _categoryService.GetAllCategory();
            return View(category.Data);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoryRequestModel model)
        {
          var category = await _categoryService.RegisterCategory(model);
          TempData["CreateCategory"] = category.Message;
            return RedirectToAction("GetAllCategory");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetCategory(id);
            return View(category.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategory(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
         var category = await  _categoryService.DeleteCategory(id);
         TempData["DeleteCategory"] = category.Message;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetCategory(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category.Data);
        }

       
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateCategoryRequestModel model)
        {
          var category = await _categoryService.UpdateCategory(id, model);
          TempData["UpdateCategory"] = category.Message;
            return RedirectToAction("Index");
        }
    }
}