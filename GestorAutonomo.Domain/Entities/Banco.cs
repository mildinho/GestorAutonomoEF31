using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GestorAutonomo.Domain.Entities
{
    [Table("CS_BANCO")]
    public class Banco : ModelBase 
    {
        [Display(Name = "Banco")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Codigo { get; set; }


        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Descricao { get; set; }


           public Banco()
        {

        }
        public Banco(Guid empresa, string codigo, string descricao)
        {
            Id = Guid.NewGuid();
            EmpresaId = empresa;
            Codigo = codigo;
            Descricao = descricao;
        }


    }
}
