using Microsoft.EntityFrameworkCore;
using MVCTest.Models;

namespace MVCTest.ContectClass
{
    public class ConText:DbContext
    {
        public ConText(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Users> UserTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>()
                .Property(x => x.id)
                .ValueGeneratedOnAdd();
        }
    }
}
