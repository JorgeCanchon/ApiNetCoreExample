using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace APIExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            InitDataBase(services);
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        public void InitDataBase(IServiceCollection services)
        {
            // CONFIGURACION DE SQLITE IN-MEMORY
            var connection = new SqliteConnection(Configuration.GetConnectionString("SqliteDBContext"));
            connection.Open();
            services.AddDbContext<RepositoryContext>(options =>
                options.UseSqlite(connection));

            var cmd = connection.CreateCommand();

            cmd.CommandText = @"CREATE TABLE Products 
                                (
                                    ProductId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                    Name TEXT  NULL,
                                    Price Price  NULL
                                )";

            cmd.ExecuteNonQuery();

            var options = new DbContextOptionsBuilder<RepositoryContext>().UseSqlite(connection).Options;

            using (var context = new RepositoryContext(options))
            {
                context.Products.Add(new ProductViewModel { Name = "Arroz", Price = 3000 });
                context.Products.Add(new ProductViewModel { Name = "Lenteja", Price = 3500 });
                context.SaveChanges();
            }

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
