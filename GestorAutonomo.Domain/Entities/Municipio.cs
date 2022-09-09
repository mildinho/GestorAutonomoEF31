using GestorAutonomo.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorAutonomo.Domain.Entities
{
    public class Municipio : ModelBase
    {

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }


        [Required]
        [ForeignKey("UF")]
        [Display(Name = "UF")]
        public Guid UFId { get; set; }
        virtual public UF UF { get; set; }


        [Display(Name = "Código Fiscal")]
        public string CodigoFiscal { get; set; }


    }

}
