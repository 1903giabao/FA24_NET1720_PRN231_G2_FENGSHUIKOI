using FENGSHUIKOI.Data.Models;
using FENGSHUIKOI.Service.Services;


namespace FENGSHUIKOI.MVCWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

      
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<NET1720_231_2_FENGSHUIKOIContext>();
            builder.Services.AddScoped<ElementService>();
            builder.Services.AddScoped<TypeService>();
            builder.Services.AddScoped<SuitableObjectService>();

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
