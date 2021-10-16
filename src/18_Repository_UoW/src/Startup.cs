using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using src.Data;
using Microsoft.EntityFrameworkCore;
using src.Domain;
using src.Data.Repositories;

namespace EFCore.UowRepsitory
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
                    
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EFCore.UowRepsitory", Version = "v1" });
            });

            services.AddDbContext<ApplicationContext>((provider, options) => 
            {
                options.UseNpgsql("Host=localhost;Database=UoW;Username=postgres;Password=123");
                options.LogTo(Console.WriteLine);
                options.EnableSensitiveDataLogging();
            });

            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EFCore.UowRepsitory v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            InicializarBaseDeDados(app);
        }

        public void InicializarBaseDeDados(IApplicationBuilder app)
        {
            using var db = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<ApplicationContext>();

            if (db.Database.EnsureCreated())
            {
                db.Departamentos.AddRange(Enumerable.Range(1, 20)
                    .Select(d => new Departamento
                    {
                        Descricao = $"Departamento - {d}",
                        Colaboradores = Enumerable.Range(1, 10)
                            .Select(c => new Colaborador
                            {
                                Nome = $"Colaborador: {c}/{d}"
                            }).ToList()
                    }));

                db.SaveChanges();
            }
        }
    }
}
