using GestorAutonomo.Data;
using GestorAutonomo.Models;
using GestorAutonomo.Repositories;
using GestorAutonomo.Repositories.Interface;
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
        public IUnitOfWork _unitOfWork;

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


        [Fact]
        public void Verificando_Se_Verdade()
        {
            bool a = true;
            bool b = true;

            Assert.True(a == b);

        }



        [Fact]
        public void Verificando_Se_Falso()
        {
            bool a = true;
            bool b = false;
            bool c = (a == b);

            Assert.False(c);
        }


        [Fact]
        public void Verificando_Letra_Inicial()
        {
            string nome = "Fernando";
            Assert.StartsWith("F", nome);
        }

        [Fact]
        public void Verificando_Letra_Final()
        {
            string nome = "Fernando".ToUpper();

            Assert.EndsWith("O", nome);
        }


    }
}
