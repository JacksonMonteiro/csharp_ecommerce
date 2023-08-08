using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configura o banco de dados
            builder.Services.AddDbContext<EcommerceContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("EcommerceContext"), builder => builder.MigrationsAssembly("Ecommerce")));


            /* Serviço responsável por adicionar as primeiras promoções ao 
             * banco de dados quando a aplicação é iniciada */
            builder.Services.AddScoped<SeedingService>();

            // Serviço responsável pelas operações no banco de dados para a tabela de produtos
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<PromotionService>();

            // Carrinho
            builder.Services.AddSession();
            builder.Services.AddSingleton<Cart>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Adicionado promoções ao banco de dados
            app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();

            // Adicionando sessões ao aplicativo
            app.UseSession();

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