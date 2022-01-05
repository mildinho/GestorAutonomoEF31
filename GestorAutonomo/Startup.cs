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
using Microsoft.AspNetCore.Mvc;

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

            services.AddMvc(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "O campo deve ser preenchido");
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => "Valor Inválido");
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor não é válido.");
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Não foi fornecido um valor para o campo {0}.");
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Campo obrigatório.");
                options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio.");
                options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => "O valor '{0}' não é válido.");
                options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor fornecido é inválido.");
                options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser um número.");
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => "O valor fornecido é inválido para {0}.");
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "O campo {0} deve ser um número.");

            }).SetCompatibilityVersion(CompatibilityVersion.Latest);

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
