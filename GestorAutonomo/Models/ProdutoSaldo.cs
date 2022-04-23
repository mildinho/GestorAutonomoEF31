using GestorAutonomo.Biblioteca.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_PRODUTOSALDO")]
    public class ProdutoSaldo
    {

        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }



        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [ForeignKey("Produto")]
        [Display(Name = "Código do Produto")]
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }


        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [ForeignKey("PontosEstoque")]
        [Display(Name = "Código do Ponto de Produto")]
        public int PontosEstoqueId { get; set; }
        public virtual PontosEstoque PontosEstoque { get; set; }



        [Display(Name = "Saldo")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double Saldo { get; set; }

        [Display(Name = "Reserva")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double Reserva { get; set; }

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





        public ProdutoSaldo()
        {
                
        }


        public ProdutoSaldo(int Produto, int PontoEstoque, int SaldoInicial, int ReservaInicial)
        {
            ProdutoId = Produto;
            PontosEstoqueId = PontoEstoque;
            Saldo = SaldoInicial;
            Reserva = ReservaInicial;
        }
    }



  
}
