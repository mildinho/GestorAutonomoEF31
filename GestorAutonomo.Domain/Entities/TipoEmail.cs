using GestorAutonomo.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GestorAutonomo.Models
{
    public class TipoEmail : ModelBase
    {
        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }


    }


}
