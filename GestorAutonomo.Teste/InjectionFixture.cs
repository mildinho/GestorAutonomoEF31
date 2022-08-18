using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorAutonomo.Teste
{
    class InjectionFixture : IDisposable
    {
        public IServiceCollection Services { get; protected set; }
        public IServiceProvider ServiceProvider { get; protected set; }


        public InjectionFixture()
        {
            Services = new ServiceCollection();
            ServiceProvider = Services.BuildServiceProvider();

        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }



    }
}