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
    public interface IDuplicataRepository : IGenericoRepository<Duplicata>
    {

        Task<IPagedList<Duplicata>> ListarTodosRegistrosAsync(TipoDuplicata Tipo, Guid IdParceiro, int? pagina);

        Task<IEnumerable<Duplicata>> ListarTodosRegistrosAsync(TipoDuplicata Tipo, Guid IdParceiro);

        Duplicata AjustarCampos(Duplicata duplicata);



    }


}
