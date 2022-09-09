using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorAutonomo.Domain.Entities
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
