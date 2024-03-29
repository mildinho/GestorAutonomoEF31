﻿using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Newtonsoft.Json;
using GestorAutonomo.Domain.Entities;

namespace GestorAutonomo.Domain.Interfaces
{
    public interface IPontosEstoqueRepository : IGenericoRepository<PontosEstoque>
    {
        Task<IPagedList<PontosEstoque>> ListarTodosRegistrosAsync( int? pagina, string pesquisa);

        Task<IEnumerable<PontosEstoque>> ListarTodosRegistrosAsync();

        PontosEstoque AjustarCampos(PontosEstoque pontos);



    }

   
}
