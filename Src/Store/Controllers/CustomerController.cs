using Microsoft.AspNetCore.Mvc;
using Store.ServiceLayer.Contracts;
using Store.ViewModel.Models;

namespace Store.Web.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _customerService.GetAllAsync();
            return View(result);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _customerService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }



        public async Task<IActionResult> Edit(int id)
        {
            var result = await _customerService.GetByIdAsync(id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _customerService.UpdateAsync(model);
                ViewBag.Result = "Success";
                // return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            await _customerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
