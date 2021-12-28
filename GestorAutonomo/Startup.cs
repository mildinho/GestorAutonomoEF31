using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using GestorAutonomo.Data;
using GestorAutonomo.Session;
using GestorAutonomo.Models;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using GestorAutonomo.Repositories;
using GestorAutonomo.Repositories.Interface;
using GestorAutonomo.Biblioteca.Middleware;

namespace GestorAutonomo
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

            //Trabalhando com Sessão 
            services.AddHttpContextAccessor();

            //Configuracao dos Repositories
            services.AddScoped<IBancoRepository, BancoRepository>();
            services.AddScoped<IUFRepository, UFRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<ICategoriaProdutoRepository, CategoriaProdutoRepository>();
            services.AddScoped<IParceiroRepository, ParceiroRepository>();

            services.AddMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

       

            services.AddControllersWithViews();

            services.AddDbContext<GestorAutonomoContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("GestorAutonomoContext"), builder => builder.MigrationsAssembly("GestorAutonomo")));


            services.AddScoped<SeedingService>();
            
            services.AddScoped<Sessao>();
            services.AddScoped<SessaoUsuario>();



            services.AddControllersWithViews().AddRazorRuntimeCompilation();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService)
        {

            var enUS = new CultureInfo("en-US");
            var ptBR = new CultureInfo("pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(ptBR),
                SupportedCultures = new List<CultureInfo> { ptBR },
                SupportedUICultures = new List<CultureInfo> { ptBR }

            };

            app.UseRequestLocalization(localizationOptions);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                seedingService.Seed();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "Admin",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
