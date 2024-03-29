﻿using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Domain.Interfaces
{
    public interface IBancoRepository : IGenericoRepository<Banco>
    {

        Task<IPagedList<Banco>> ListarTodosRegistrosAsync(int? pagina, string pesquisa);

    }
}
