using AngularwithAPI.Models;


using Microsoft.EntityFrameworkCore;

namespace AngularwithAPI.Data
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set;}

    }
}
