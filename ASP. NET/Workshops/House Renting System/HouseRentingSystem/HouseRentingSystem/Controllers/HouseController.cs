using HouseRentingSystem.Core.Models.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class HouseController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return View(new AllHousesQueryModel());
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            return View(new HouseDetailsViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            return View(new AllHousesQueryModel());
        }

        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseFormViewModel model)
        {
            return RedirectToAction(nameof(Details));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(new HouseFormViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HouseFormViewModel model, int id)
        {
            return RedirectToAction(nameof(Details));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(new HouseDetailsViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel model, int id)
        {
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
