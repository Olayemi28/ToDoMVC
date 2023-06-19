using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;

        }

        public async Task<IActionResult> GetAllRole()
        {
            var role = await _roleService.GetAllRole();
            return View(role.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleRequestModel model)
        {
           var role = await _roleService.RegisterRole(model);
           TempData["CreateRole"] = role.Message;
            return RedirectToAction("IndexAdmin", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var role = await _roleService.GetRole(id);
            return View(role.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleService.GetRole(id);
            if(role == null)
            {
                return ViewBag.Message("Role not found");
            }
            return View(role.Data);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          var role = await _roleService.DeleteRole(id);
          TempData["DeleteRole"] = role.Message;
            return RedirectToAction("GetAllRole", "Role");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var role = await _roleService.GetRole(id);
            if(role == null)
            {
                return ViewBag.Message("Role not found");
            }
            return View(role.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateRoleRequestModel model)
        {
          var role = await _roleService.UpdateRole(id, model);
          TempData["UpdateRole"] = role.Message;
            return RedirectToAction("GetAllRole", "Role");
        }
    }
}