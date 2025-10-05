using Company.G01.BLL;
using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Data.Contexts;
using Company.G01.PL.Mapping;
using Company.G01.PL.Services;
using Microsoft.EntityFrameworkCore;

namespace Company.G01.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); //Register Built-In MVC Service
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>(); // Allow DI For DepartmentRepository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // Allow DI For EmployeeRepository

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // Allow DI For UnitOfWork

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));   
            }); // Allow DI For CompanyDbContext

            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));

            //Lifetime
            //builder.Services.AddScoped(); // Create Object Life Time Per Request - Unreachable Object
            //builder.Services.AddKeyedTransient(); // Create Object Life Time per Operation
            //builder.Services.AddSingleton(); // Create Object Life Time Per Application - Reachable Object

            //builder.Services.AddScoped<IScopedService,ScopedService>(); // Per Request
            //builder.Services.AddTransient<ITransientService, TransientService>(); // Per Operation
            //builder.Services.AddSingleton<ISingletonService, SingletonService>(); // Per Application 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
