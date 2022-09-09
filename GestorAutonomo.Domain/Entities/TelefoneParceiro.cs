using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorAutonomo.GestorAutonomo.Domain.Entities
{
    public class TelefoneParceiro : ModelBase
    {

        [Required]
        [ForeignKey("TipoTelefone")]
        [Display(Name = "Tipo de Telefone")]
        public Guid TipoTelefoneId { get; set; }
        virtual public TipoTelefone TipoTelefone { get; set; }


        [Required]
        [ForeignKey("Parceiro")]
        [Display(Name = "Parceiro")]
        public Guid ParceiroId { get; set; }

        public string Pais { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }


    }

}
