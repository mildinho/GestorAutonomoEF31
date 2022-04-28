using GestorAutonomo.Biblioteca.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_DUPLICATA")]
    public class Duplicata
    {

        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Tipo de Duplicata")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public TipoDuplicata TipoDuplicata { get; set; } = 0;

        [Display(Name = "Numero do Documento")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double Documento { get; set; }

        [Display(Name = "Parcela do Documento")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double Parcela { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [ForeignKey("Parceiro")]
        [Display(Name = "Código do Parceiro")]
        public int ParceiroId { get; set; }
        public virtual Parceiro Parceiro { get; set; }



        [Display(Name = "Data de Emissão")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data_Emissao { get; set; }

        [Display(Name = "Data de Vencimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data_Vencimento { get; set; }

        [Display(Name = "Data de Pagamento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data_Pagamento { get; set; }

        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        public double? Valor { get; set; }

        [Display(Name = "Abatimento")]
        [DataType(DataType.Currency)]
        public double? Abatimento { get; set; }

        [Display(Name = "Acréscimo")]
        [DataType(DataType.Currency)]
        public double? Acrescimo { get; set; }

        [Display(Name = "Valor Pago")]
        [DataType(DataType.Currency)]
        public double? Valor_Pago { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [ForeignKey("Banco")]
        [Display(Name = "Banco")]
        public int BancoId { get; set; }
        public virtual Banco Banco { get; set; }

        [Display(Name = "Histórico")]
        public string Historico { get; set; }


        [Display(Name = "Nosso Número")]
        public string Nosso_Numero { get; set; }

        [Display(Name = "Número da Remessa")]
        public string Remessa_Numero { get; set; }


        [Display(Name = "Data Registro Banco")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data_Registro_Banco { get; set; }


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

