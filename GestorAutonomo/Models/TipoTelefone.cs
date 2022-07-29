using GestorAutonomo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    public class TipoTelefone : ModelBase
    {
        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }


        public TipoTelefone()
        {
        }

        public TipoTelefone(Guid empresa, string descricao)
        {

            Id = Guid.NewGuid();
            EmpresaId = empresa;
            Descricao = descricao;
        }
    }
}
