using GestorAutonomo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
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
