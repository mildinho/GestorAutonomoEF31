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
    public interface IProdutoRepository : IGenericoRepository<Produto>
    {

        Task<IPagedList<Produto>> ListarTodosRegistrosAsync(int? pagina, string pesquisa);

        Task<IEnumerable<Produto>> ListarTodosRegistrosAsync();

        Produto AjustarCampos(Produto produto);



    }


}
