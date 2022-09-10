using GestorAutonomo.Biblioteca.Middleware;
using GestorAutonomo.Biblioteca.Notification;
using GestorAutonomo.Domain.Interfaces;
using GestorAutonomo.Infra.Data.Context;
using GestorAutonomo.Models;
using GestorAutonomo.Repositories;
using GestorAutonomo.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;

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
            services.AddScoped<IImagemRepository, ImagemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

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
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "O campo deve ser preenchido.  - A");
                
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => "Valor Inválido - B");
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor não é válido. - C");
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Não foi fornecido um valor para o campo {0}.  -D");
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Campo obrigatório.  --E ");
                options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio.  -F");
                options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => "O valor '{0}' não é válido.  -- G");
                options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor fornecido é inválido. ---H");
                options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser um número.   -I");
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => "O valor fornecido é inválido para {0}.  -- J");
                //options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "O campo {0} deve ser um número.  - K");
                
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddControllersWithViews().AddRazorRuntimeCompilation();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService, IHttpContextAccessor accessor)
        {

            var enUS = new CultureInfo("en-US");
            var ptBR = new CultureInfo("pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS }

            };

            app.UseRequestLocalization(localizationOptions);

            AlertHandler.SetHttpContextAccessor(accessor);



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

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/PageNotFound";
                    await next();
                }
            });



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

//TODO: - CRIAÇÃO DESENVOLVIMENTO DA UNITOFWORK
//TODO: - Fazer a tratativa de foreingkey ( ou seja) nao deixar deletar um Ponto de Estoque que tenha um produto vinculado e Saldo.
//TODO: - No Cadastro de Produto - Exibir os Pontos de Estoque para a Peça;
//TODO: - Fazer o seedsingservice do modelo MUNICIPIO;
//TODO: - Fazer o seedsingservice do modelo TIPOEMAIL;
//TODO: - Fazer o seedsingservice do modelo TIPOLOGRADOURO;
