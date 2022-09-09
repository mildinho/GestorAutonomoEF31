using GestorAutonomo.Domain.Biblioteca.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorAutonomo.Domain.Entities
{
    [Table("CS_PRODUTO")]
    public class Produto : ModelBase
    {

        [Display(Name = "Referência")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Referencia { get; set; }


        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Descricao { get; set; }

       
        [Display(Name = "Altura")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Range(2, 105, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        public int Altura { get; set; }




        [Display(Name = "Largura")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Range(11, 105, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        public int Largura { get; set; }




        [Display(Name = "Comprimento")]
        [Range(16, 105, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public int Comprimento { get; set; }



        [Display(Name = "Peso")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Range(0.001, 30, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        public double Peso { get; set; }


        [Column(TypeName = "decimal(18,4)")]
        [Display(Name = "Preço de Venda")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [DisplayFormat(DataFormatString = "{0:###.##00}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        public decimal PrecoVenda { get; set; }

        [Display(Name = "Preço de Custo")]
        //[DataType(DataType.Currency)]
        public double? PrecoCusto { get; set; }

        [Display(Name = "Preço Médio")]
        //[DataType(DataType.Currency)]
        public double? PrecoMedio { get; set; }




        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [ForeignKey("CategoriaProduto")]
        [Display(Name = "Categoria")]
        public Guid CategoriaProdutoId { get; set; }
        public virtual CategoriaProduto CategoriaProduto { get; set; }
        
        
        public virtual ICollection<ProdutoSaldo> ProdutoSaldo { get; set; }
        

        public virtual ICollection<Imagem> Imagens { get; set; }


        public Produto(Guid empresa, string referencia, string descricao, Guid categoria)
        {
            Id = Guid.NewGuid();
            EmpresaId = empresa;
            Referencia = referencia;
            Descricao = descricao;
            Altura = 22;
            Largura = 66;
            Comprimento = 89;
            Peso = 14;
            PrecoVenda = 10;
            PrecoMedio = 5;
            PrecoCusto = 5;
            CategoriaProdutoId = categoria;



        }

        public Produto()
        {

        }

    }
}

