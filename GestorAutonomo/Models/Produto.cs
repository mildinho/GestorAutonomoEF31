using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_PRODUTO")]
    public class Produto
    {

        [Key]
        [Display(Name = "Código")]

        public int Id { get; set; }


        [Display(Name = "Referencia")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Referencia { get; set; }


        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Descricao { get; set; }

        [Display(Name = "Data da Alteracao")]
        [DataType(DataType.Date)]
        public DateTime Data_Alteracao { get; set; } = DateTime.Now;

        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.Date)]
        public DateTime Data_Cadastro { get; set; }


    }
}
