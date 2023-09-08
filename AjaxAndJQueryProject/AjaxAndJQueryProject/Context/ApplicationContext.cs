using AjaxAndJQueryProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AjaxAndJQueryProject.Entity
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        public DbSet<Employee> AjaxEmployee { get; set; }
    }
}
