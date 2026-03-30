using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MIT_AT2_Portal.Models
{

    public class StudentDataContext: IdentityDbContext
    {
        public DbSet<Student> student {  get; set; }
    

    public StudentDataContext(DbContextOptions<StudentDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    
    }
}
