using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorAutonomo.Models
{
    [Table("CS_IMAGEM")]
    public class Imagem
    {
        public int Id { get; set; }
        public string Caminho { get; set; }

        //Banco de Dados
        public int ProdutoId { get; set; }

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


        //POO
        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }
    }
}
