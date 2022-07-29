using GestorAutonomo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_PONTOESTOQUE")]
    public class PontosEstoque : ModelBase
    {

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Descricao { get; set; }


      

        public PontosEstoque()
        {

        }

        public PontosEstoque(string descricao)
        {
            Id = Guid.NewGuid();
            Descricao = descricao;
        }
    }
}
