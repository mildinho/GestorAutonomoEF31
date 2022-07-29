using GestorAutonomo.Biblioteca.Lang;
using GestorAutonomo.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_PARCEIRO")]
    public class Parceiro : ModelBase
    {

        [Display(Name = "Cliente")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public int Cliente { get; set; } = 0;

        [Display(Name = "Fornecedor")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public int Fornecedor { get; set; } = 0;

        [Display(Name = "Vendedor")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public int Vendedor { get; set; } = 0;




        [Display(Name = "CNPJ / CNPJ")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [RegularExpression("^[0-9]{1,14}$", ErrorMessage = "Informe somente os Números")]
        [Remote(action: "Existe_CPF_CNPJ", controller:"Cliente", areaName:"Admin", AdditionalFields = "operacao")]
        public double CNPJ_CPF { get; set; }


        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
        public String Nome { get; set; }


        [Display(Name = "Fantasia")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
        public string Fantasia { get; set; }
      

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Display(Name = "Cidade")]
        public string Cidade { get; set; }



        [Display(Name = "CEP")]
        public string CEP { get; set; }



        [Display(Name = "E-Mail Principal")]
        [EmailAddress(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E004")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "E-Mail NF Eletronica")]
        [EmailAddress(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E004")]
        [DataType(DataType.EmailAddress)]
        public string EmailNFE { get; set; }



        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Telefone Principal")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone01 { get; set; }

        [Display(Name = "Telefone Secundario")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone02 { get; set; }


        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [ForeignKey("UF")]
        [Display(Name = "UF")]
        public Guid UFId { get; set; }

        public virtual UF UF { get; set; }



    }


}
