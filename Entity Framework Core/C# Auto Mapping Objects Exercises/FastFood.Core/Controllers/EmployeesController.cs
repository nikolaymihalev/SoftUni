namespace FastFood.Core.Controllers
{
    using System;
    using System.Security.Cryptography.Xml;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using FastFood.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ViewModels.Employees;

    public class EmployeesController : Controller
    {
        private readonly FastFoodContext _context;
        private readonly IMapper _mapper;

        public EmployeesController(FastFoodContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Register()
        {
            var positions = await _context.Positions
                .ProjectTo<RegisterEmployeeViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return View(positions);
        }

        [HttpPost]
        public IActionResult Register(RegisterEmployeeInputModel model)
        {
            if (!ModelState.IsValid) 
            {
                RedirectToAction("Error", "Home");
            }

            //var employee = new Employee
            //{
            //    Name = model.Name,
            //    Age = model.Age,
            //    Address = model.Address,
            //    PositionId = model.PositionId
            //};

            var employee = _mapper.Map<Employee>(model);

            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("All");
        }

        public async Task<IActionResult> All()
        {
            var employees = await _context.Employees
                .Select(e=> new EmployeesAllViewModel 
                {
                    Name= e.Name,
                    Address= e.Address,
                    Age= e.Age,
                    Position = e.Position.Name
                })
                .ToListAsync();

            return View(employees);
        }
    }
}
