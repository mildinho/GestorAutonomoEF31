using GestorAutonomo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace GestorAutonomo.Teste
{
    public class TesteManutencaoBanco : IClassFixture<InjectionFixture>

    {

        public IServiceCollection _services;
        public IServiceProvider _serviceProvider;

        public TesteManutencaoBanco(InjectionFixture injection)
        {
            _services = injection.Services;
            _serviceProvider = injection.ServiceProvider;
            Console.WriteLine("Passei pela Injecao de Dependencia.");


            _services = new ServiceCollection();

            _services.AddDbContext<GestorAutonomoContext>(opt => opt.UseInMemoryDatabase(databaseName: "InMemoryDb"),
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped);

            _serviceProvider = _services.BuildServiceProvider();


        }


        //[Fact]
        //public void Inserindo_Registro_Album()
        //{
        //    using (var dbContext = _serviceProvider.GetService<GestorAutonomoContext>())
        //    {
        //        dbContext.Banco.Add(
        //            new ba
        //            {
        //                AlbumId = 1,
        //                Destino = "Guaxupe",
        //                Inicio = DateTime.Today,
        //                Fim = DateTime.Today
        //            }
        //            );


        //        dbContext.SaveChanges();
        //        Assert.True(dbContext.Albuns.Count() == 1);
        //    }
        //}

    }
}
