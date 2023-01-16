using Microsoft.EntityFrameworkCore;

namespace Contacts_Assignment.Models
{
    public class CategoryDbContext : DbContext
    {
        public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options) 
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}
