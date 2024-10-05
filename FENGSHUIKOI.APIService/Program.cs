
using FENGSHUIKOI.Data.Models;
using FENGSHUIKOI.Service.Base;
using FENGSHUIKOI.Service.Services;

namespace FENGSHUIKOI.APIService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IPackageService, PackageService>();
            builder.Services.AddScoped<IProductImageService, ProductImageService>();
            builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
            builder.Services.AddScoped<ISuitableObjectService, SuitableObjectService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<IComboService, ComboService>();
            builder.Services.AddScoped<ITypeService, TypeService>();
            builder.Services.AddScoped<IElementService, ElementService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAnyOrigin");



            app.MapControllers();

            app.Run();
        }
    }
}
