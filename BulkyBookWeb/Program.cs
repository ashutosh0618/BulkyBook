
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BulkyBook.DataAccess;
using Microsoft.AspNetCore.Identity;
using BulkyBook.Model;
//using Microsoft.EntityFrameworkCore;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        //my
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefalutConnection")
            ));

        // builder.Services.AddDefaultIdentity<ApplicationUser>();
      

        builder.Services.AddIdentity<IdentityUser,IdentityRole> ()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders(); 



        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        


        builder.Services.AddRazorPages();

        //builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        //builder.Services.AddHttpClient();
        // builder.Services.AddHttpContextAccessor();

        //builder.Services.AddScoped<IUnitOfWork<Lead>, LeadManager>();

        builder.Services.AddControllers();
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
        //builder.Services.ConfigureIdentity();



        //builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();






        /*
         builder.Services.AddContext<ApplicationContext>(option => options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefalutConnection")
        ));
         */
        /*builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefalutConnection")
            ));*/

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication(); 

        app.UseAuthorization();
       
        app.MapRazorPages();//identity

        app.MapControllerRoute(
            name: "default",
            pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}