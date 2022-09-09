using GestorAutonomo.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorAutonomo.Domain.Entities
{
    public class EmailParceiro : ModelBase
    {

        [Required]
        [ForeignKey("TipoEmail")]
        [Display(Name = "Tipo de Email")]
        public Guid TipoEmailId { get; set; }
        virtual public TipoEmail TipoEmail { get; set; }


        [Required]
        [ForeignKey("Parceiro")]
        [Display(Name = "Parceiro")]
        public Guid ParceiroId { get; set; }


        public string Email { get; set; }


    }


}
