using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Controllers
{

    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CustomerController(ICustomerService customerService, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;

            _customerService = customerService;
        }


        [HttpGet("Customer/IndexCustomer")]
        public IActionResult IndexCustomer()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var customer = await _customerService.GetAllCustomer();
            return View(customer.Data);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CustomerRequestModel model, IFormFile customerPhoto)
        {
            string customerPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, "customerPhotos");
            Directory.CreateDirectory(customerPhotoPath);
            string contentType = customerPhoto.ContentType.Split('/')[1];
            string customerImage = $"CTM{Guid.NewGuid()}.{contentType}";
            string fullPath = Path.Combine(customerPhotoPath, customerImage);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                customerPhoto.CopyTo(fileStream);
            }
            model.CustomerPhoto = customerImage;
           var customer = await _customerService.RegisterCustomer(model);
           TempData["CreateCustomer"] = customer.Message;
            return RedirectToAction("SignIn", "User");
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            return View(customer.Data);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Profile()
        {
            //int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var customer = User.FindFirst(ClaimTypes.Email).Value;
            var custom = await _customerService.GetCustomerbymail(customer);
            var user = await _customerService.GetCustomer(custom.Data.Id);
            return View(user.Data);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer.Data);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          var customer =  await _customerService.DeleteCustomer(id);
          TempData["DeleteCustomer"] = customer.Message;
            return RedirectToAction("GetAllCustomer", "Customer");
        }


        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var customer = await _customerService.GetCustomerbymail(user);
            var customerr = await _customerService.GetCustomer(customer.Data.Id);
            if (customerr == null)
            {
                return NotFound();
            }
            return View(customerr.Data);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateCustomerRequestModel model, IFormFile customerPhoto)
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var customer = await _customerService.GetCustomerbymail(user);
            var customerr = await _customerService.GetCustomer(customer.Data.Id);
            string customerPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, "customerPhotos");
            Directory.CreateDirectory(customerPhotoPath);
            string contentType = customerPhoto.ContentType.Split('/')[1];
            string customerImage = $"CTM{Guid.NewGuid()}.{contentType}";
            string fullPath = Path.Combine(customerPhotoPath, customerImage);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                customerPhoto.CopyTo(fileStream);
            }
            model.CustomerPhoto = customerImage;
            var custom =  await _customerService.UpdateCustomer(customerr.Data.Id, model);
            TempData["UpdateCustomer"] = custom.Message;
            return RedirectToAction("Profile", "Customer");
        }
    }
}