using GestorAutonomo.Biblioteca.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_CATEGORIAPRODUTO")]
    public class CategoriaProduto
    {

        [Display(Name = "Código")]
        public int Id { get; set; }


        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
        [Display(Name = "Nome")]
        public string Descricao { get; set; }


        [Display(Name = "Categoria Pai")]
        public int? CategoriaPaiId { get; set; }

        [ForeignKey("CategoriaPaiId")]
        public virtual CategoriaProduto CategoriaPai { get; set; }

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


       public CategoriaProduto()
        {
        }


        public CategoriaProduto(int id, string descricao, int? categoriaPaiId)
        {
            Id = id;
            Descricao = descricao;
            CategoriaPaiId = categoriaPaiId;
           
        }
    }
}
