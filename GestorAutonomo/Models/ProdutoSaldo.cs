using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_PRODUTOSALDO")]
    public class ProdutoSaldo
    {

        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }


        //TODO: fazer referent ao ponto de estoque

        [Display(Name = "Saldo")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double Saldo { get; set; }

        [Display(Name = "Reserva")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double Reserva { get; set; }

        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Data_Cadastro { get; set; }

        [Display(Name = "Data de Alteração")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Data_Alteracao { get; set; }


    }
}
