using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_UF")]

    public class UF
    {
        public UF()
        {
        }

        public UF(int id, string sigla, string descricao)
        {
            Id = id;
            Sigla = sigla;
            Descricao = descricao;
        }


        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "UF")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Sigla { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Descricao { get; set; }

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
