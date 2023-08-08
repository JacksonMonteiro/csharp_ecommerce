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


            /* Servi�o respons�vel por adicionar as primeiras promo��es ao 
             * banco de dados quando a aplica��o � iniciada */
            builder.Services.AddScoped<SeedingService>();

            // Servi�o respons�vel pelas opera��es no banco de dados para a tabela de produtos
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

            // Adicionado promo��es ao banco de dados
            app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();

            // Adicionando sess�es ao aplicativo
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