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




    }
}
