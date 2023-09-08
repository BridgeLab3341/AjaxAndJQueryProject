using AjaxAndJQueryProject.Entity;
using AjaxAndJQueryProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace AjaxAndJQueryProject.Controllers
{
    public class AjaxController : Controller
    {
        private readonly ApplicationContext context;
        public AjaxController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult EmployeeList()
        {
            var data = context.AjaxEmployee.ToList();
            return new JsonResult(data);
        }
        [HttpGet]
        public JsonResult Create(Employee model)
        {
            try
            {
                    Employee employee = new Employee()
                    {
                        Name = model.Name,
                        State = model.State,
                        City = model.City,
                        Salary = model.Salary,
                    };
                    context.AjaxEmployee.Add(employee);
                    context.SaveChanges();
                    return new JsonResult("Data is Saved");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult Edit(int id)
        {
            var employee = context.AjaxEmployee.FirstOrDefault(x=> x.Id == id);
            if (employee != null)
            {
                
            }
        }
    }
}
