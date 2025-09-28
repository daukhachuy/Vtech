using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PAS_APP.DAO;
using PAS_APP.DBContext;
using PAS_APP.Models;
using PAS_APP.Services;

namespace PAS_APP
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var hasher = new PasswordHasher<object>();
            var hash = hasher.HashPassword(new object(), "vtech123456789");
            Console.WriteLine(hash);
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // google
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
             .AddCookie() // dùng cookie lưu session người dùng
             .AddGoogle(options =>
              { 
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
               });



            builder.Services.AddDbContext<VtechDatabaseContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStringDB")));


            builder.Services.AddScoped<UserDao>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddSingleton<IPasswordHasher<object>, PasswordHasher<object>>();
            // Session để lưu người đăng nhập
            builder.Services.AddSession();

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
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Signin}/{id?}");

            app.Run();
        }
    }
}
