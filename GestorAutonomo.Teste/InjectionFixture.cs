﻿using GestorAutonomo.Data;
using GestorAutonomo.Repositories;
using GestorAutonomo.Repositories.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorAutonomo.Teste
{
    public class InjectionFixture : IDisposable
    {
        public IServiceCollection Services { get; protected set; }
        public IServiceProvider ServiceProvider { get; protected set; }
        public IUnitOfWork unitOfWork { get; protected set; }
        public IConfiguration configuration { get; protected set; }


        public InjectionFixture()
        {
            Services = new ServiceCollection();
            ServiceProvider = Services.BuildServiceProvider();
            unitOfWork = new UnitOfWork(
                ServiceProvider.GetService<GestorAutonomoContext>(),
                configuration
                );
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