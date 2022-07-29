using GestorAutonomo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    public class TipoEmail : ModelBase
    {
        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }


    }


}
