using Microsoft.EntityFrameworkCore;

namespace Contacts_Assignment.Models
{
    public class SubcategoryDbContext : DbContext
    {
        public SubcategoryDbContext(DbContextOptions<SubcategoryDbContext> options) : base(options)
        {
            
        }

        public DbSet<Subcategory> Subcategories { get; set; }
    }
}
