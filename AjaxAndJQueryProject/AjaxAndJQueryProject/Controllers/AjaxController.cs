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
        [HttpPost]
        public JsonResult AddEmployee(Employee model)
        {
            try
            {
                var employee = new Employee()
                {
                    Name = model.Name,
                    State = model.State,
                    City = model.City,
                    Salary = model.Salary
                };
                context.AjaxEmployee.Add(employee);
                context.SaveChanges();
                return new JsonResult("Data is Saved");
            }
            catch(Exception )
            {
                throw new Exception("Error occurred while saving data to the database");
            }
        }
        public JsonResult Edit(int id)
        {
            try
            {
                var employee = context.AjaxEmployee.FirstOrDefault(x => x.Id == id);
                return new JsonResult(employee);
            }
            catch (Exception ) 
            {
                throw new Exception("Error occurred while editing data.");
            }
        }
        [HttpPost]
        public JsonResult Update(Employee employee)
        {
            try
            {
                context.AjaxEmployee.Update(employee);
                context.SaveChanges();
                return new JsonResult("Updated Successfully");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Updating data.");
            }
        }
        public JsonResult Delete(int id)
        {
            try
            {
                var employee = context.AjaxEmployee.FirstOrDefault(x => x.Id == id);
                context.AjaxEmployee.Remove(employee);
                context.SaveChanges();
                return new JsonResult("Deleted Successfully");
            }
            catch(Exception ex)
            {
                throw new Exception("Error occurred while Deleting data.");
            }
        }
    }
}
