using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Controllers
{
    public class TodoitemController : Controller
    {
        private readonly ITodoitemService _todoitemService;
        private readonly ICustomerService _customerservice;
        private readonly ICategoryService _categoryService;

        public TodoitemController(ITodoitemService todoitemService, ICustomerService customerservice, ICategoryService categoryService)
        {
            _todoitemService = todoitemService;
            _customerservice = customerservice;
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTodos()
        {
            var todoitem = await _todoitemService.GetAllTodoitem();
            return View(todoitem.Data);
        }
        
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategory();
            ViewData["Categories"] = new SelectList(categories.Data, "Id", "Name");
            return View();
        }

        [Authorize(Roles="Customer")]
        [HttpPost]
        public  async Task<IActionResult> Create(TodoRequestModel model)
        {
            // var Mail = User.FindFirst(ClaimTypes.Email).Value;
            // var rest=  await _customerservice.GetCustomerbymail(Mail);
            // await _todoitemService.RegisterTodoitem(model, rest.Data.Id);
            var signedInUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var todoitem = await _todoitemService.RegisterTodoitem(model, signedInUserId);
            TempData["CreateTodoitem"] = todoitem.Message;
            return RedirectToAction("IndexCustomer", "Customer");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var todoitem = await _todoitemService.GetTodoitem(id);
            return View(todoitem.Data);
        }

        [HttpGet]
         [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Delete(int id)
        {
            var todoitem = await _todoitemService.GetTodoitem(id);
            if (todoitem == null) 
            {
                return ViewBag.Message("Item not found");
            }
            return View(todoitem.Data);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          var todoitem = await _todoitemService.DeleteTodoitem(id);
          TempData["DeleteAdmin"] = todoitem.Message;
            return RedirectToAction("GetAllTodos", "Todoitem");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var todoitem = await _todoitemService.GetTodoitem(id);
            if(todoitem == null)
            {
                return ViewBag.Message("Item not found");
            }
            return View(todoitem.Data);
        }

        [HttpPost]
      //  [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Update(int id,UpdateTodoitemRequestModel model)
        {
          var todoitem = await _todoitemService.UpdateTodoitem(id, model);
          TempData["UpdateTodoitem"] = todoitem.Message;
            return RedirectToAction("Today", "Todoitem");
        }
        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> Done(int id)
        {
            var res = await _todoitemService.Done(id);
            
            return RedirectToAction("TaskDone");
        }
        [Authorize(Roles = "Customer")]
         [HttpGet]
        public async Task<IActionResult> All()
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllCustomerTaskById(rest.Data.Id);
            return View(res.Data);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Today()
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllCustomerTodayTaskById(rest.Data.Id);
            return View(res.Data);
        }

        public async Task<IActionResult> TaskDone()
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllCustomerDoneTaskById(rest.Data.Id);
            TempData["DoneTask"] = res.Message;
            return View(res.Data);
        }

        public async Task<IActionResult> Skipped()
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllCustomerSkippedTaskById(rest.Data.Id);
            return View(res.Data);
        }

        public async Task<IActionResult> Upcoming()
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllCustomerUpcomingTaskById(rest.Data.Id);
            return View(res.Data);
        }


        [HttpPost]

        public async Task<IActionResult> GetCustomerTaskByDate(DateTime OriginalTime)
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllCustomerTaskByDate(rest.Data.Id, OriginalTime);
            return View(res.Data);
        }


        [HttpPost]
        public async Task<IActionResult> GetCustomerTaskByTime(DateTime OriginalTime)
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllCustomerTaskByTime(rest.Data.Id, OriginalTime);
            return View(res.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerTaskByDay(DaysOfTheWeek DayOfTheWeek)
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllCustomerTaskByDay(rest.Data.Id, DayOfTheWeek);
            return View(res.Data);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTodayTask()
        {
            var task = await _todoitemService.GetAllTodayTask();
            return View(task.Data);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUpcomingTask()
        {
            var task = await _todoitemService.GetAllUpcomingTask();
            return View(task.Data);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDoneTask()
        {
            var task = await _todoitemService.GetAllDoneTask();
            return View(task.Data);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllSkippedTask()
        {
            var task = await _todoitemService.GetAllSkippedTask();
            return View(task.Data);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTaskByDate(DateTime date)
        {
            var task = await _todoitemService.GetAllTaskByDate(date);
            return View(task.Data);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTaskByTime(DateTime time)
        {
            var task = await _todoitemService.GetAllTaskByTime(time);
            return View(task.Data);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTaskByDay(DateTime day)
        {
            var task = await _todoitemService.GetAllTaskByDay(day);
            return View(task.Data);
        }
    }
}