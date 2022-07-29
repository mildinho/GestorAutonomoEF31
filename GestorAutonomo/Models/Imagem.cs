using GestorAutonomo.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorAutonomo.Models
{
    [Table("CS_IMAGEM")]
    public class Imagem : ModelBase
    {
        public string Caminho { get; set; }

        //Banco de Dados
        public Guid ProdutoId { get; set; }

    
        //POO
        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }
    }
}
