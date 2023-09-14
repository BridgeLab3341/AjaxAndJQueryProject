using AjaxAndJQueryProject.Entity;
using AjaxAndJQueryProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjaxAndJQueryProject.Controllers
{
    public class AjaxController : Controller
    {
        private readonly ApplicationContext context;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public AjaxController(ApplicationContext context, IDistributedCache distributedCache, IMemoryCache memoryCache)
        {
            this.context = context;
            this.distributedCache = distributedCache;
            this.memoryCache = memoryCache;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult EmployeeList()
        {
            var cacheKey = "EmployeeList";
            string serializedEmployeeList;
            var empolyeelist = new List<Employee>().ToList();
            // Try to retrieve data from the cache
            var cachedData = distributedCache.Get(cacheKey);
            if (cachedData != null)
            {
                // Data found in the cache, deserialize it
                serializedEmployeeList = Encoding.UTF8.GetString(cachedData);
                empolyeelist = JsonConvert.DeserializeObject<List<Employee>>(serializedEmployeeList);
                return Json(empolyeelist);
            }
            else
            {
                var idClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("Id", StringComparison.InvariantCultureIgnoreCase));
                var userId = Int32.Parse(idClaim.Value);
                    // Data not found in the cache, retrieve it from the database
                empolyeelist = context.AjaxEmployee.ToList();
                    // Serialize and store in the cache
                 serializedEmployeeList = JsonConvert.SerializeObject(empolyeelist);
                    var redisEmployeeList = Encoding.UTF8.GetBytes(serializedEmployeeList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTimeOffset.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    distributedCache.Set(cacheKey, redisEmployeeList, options);
            }   
            return new JsonResult(empolyeelist);
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
