using GestorAutonomo.Biblioteca.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_PRODUTO")]
    public class Produto
    {

        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }


        [Display(Name = "Referência")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Referencia { get; set; }


        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Descricao { get; set; }

        [Display(Name = "Data da Alteracao")]
        [DataType(DataType.Date)]
        public DateTime Data_Alteracao { get; set; } = DateTime.Now;

        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.Date)]
        public DateTime Data_Cadastro { get; set; }


        [Display(Name = "Altura")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double Altura { get; set; }

        [Display(Name = "Largura")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double Largura { get; set; }

        [Display(Name = "Comprimento")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double Comprimento { get; set; }

        [Display(Name = "Peso")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double Peso { get; set; }

        [Display(Name = "Preço de Venda")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double PrecoVenda { get; set; }

        [Display(Name = "Preço de Custo")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double PrecoCusto { get; set; }

        [Display(Name = "Preço Médio")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double PrecoMedio { get; set; }

        //public virtual ICollection<PontosEstoque> PontosEstoque { get; set; }



        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [ForeignKey("Categoria")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
       
        public CategoriaProduto CategoriaProduto { get; set; }

        //public virtual ICollection<Imagem> Imagens { get; set; }






        public Produto(int id, string referencia, string descricao)
        {
            Id = id;
            Referencia = referencia;
            Descricao = descricao;
            Data_Cadastro = DateTime.Now;
            Altura = 22.56;
            Largura = 67.58;
            Comprimento = 89.23;
            Peso = 14;
            CategoriaId = 1;
            PrecoVenda = 10;
            PrecoMedio = 5;
            PrecoCusto = 5;
            CategoriaId = 2;

        }

        public Produto()
        {

        }

    }
}

