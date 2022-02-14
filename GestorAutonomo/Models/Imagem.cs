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

        //POO
        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }
    }
}
