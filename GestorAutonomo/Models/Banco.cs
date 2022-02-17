using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_BANCO")]
    public class Banco
    {

        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }


        [Display(Name = "Banco")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Codigo { get; set; }


        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Descricao { get; set; }


        public Banco()
        {

        }
        public Banco(int id, string codigo, string descricao)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
        }


    }
}
