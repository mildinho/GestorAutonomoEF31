using GestorAutonomo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_UF")]

    public class UF : ModelBase
    {
        public UF()
        {
        }

        public UF(Guid empresa, string sigla, string descricao)
        {
            Id = Guid.NewGuid();
            EmpresaId = empresa;
            Sigla = sigla;
            Descricao = descricao;
        }


        [Display(Name = "UF")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Sigla { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Descricao { get; set; }

        [Display(Name = "Código Fiscal")]
        public string CodigoFiscal { get; set; }

    }
}
