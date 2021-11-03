using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_UF")]

    public class UF
    {
        public UF()
        {
        }

        public UF(int id, string sigla, string descricao)
        {
            Id = id;
            Sigla = sigla;
            Descricao = descricao;
        }


        [Key]
        public int Id { get; set; }

        [Display(Name = "UF")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Sigla { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Descricao { get; set; }

        
    }
}
