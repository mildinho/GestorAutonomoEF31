using GestorAutonomo.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

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
