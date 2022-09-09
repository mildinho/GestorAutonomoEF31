using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorAutonomo.Domain.Entities
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

        public PontosEstoque(Guid empresa, string descricao)
        {
            Id = Guid.NewGuid();
            EmpresaId = empresa;
            Descricao = descricao;
        }
    }
}
