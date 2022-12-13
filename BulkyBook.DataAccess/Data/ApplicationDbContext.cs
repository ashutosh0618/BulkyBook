using BulkyBook.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


//using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;


namespace BulkyBook.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // private readonly DbContextOptions<ApplicationDbContext> options;
        //configuration
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //pass to the base class
        {
            //this.opitions = options;
        }

        
        /*
public ApplicationDbContext(DbContextOption<ApplicationDbContext> options) : base(options)
*/
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }

        //crate table

        public DbSet<category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Branch> Branches { get; set; }

        public DbSet<Player> Players { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }


    }
}
