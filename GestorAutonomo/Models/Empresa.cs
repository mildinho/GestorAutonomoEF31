using GestorAutonomo.Biblioteca.Lang;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorAutonomo.Models
{
    [Table("CS_EMPRESA")]
    public class Empresa
    {
        public Empresa()
        {
        }

        [Key]
        [Display(Name = "Código")]
        public Guid Id { get; set; }

        [Display(Name = "CNPJ / CNPJ")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double CNPJ_CPF { get; set; }


        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
        public String Nome { get; set; }


        [Display(Name = "Fantasia")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
        public string Fantasia { get; set; }
       
        
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



        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E004")]
        public string Email { get; set; }

        [Display(Name = "E-Mail Comercial")]
        public string EmailComercial { get; set; }

        [Display(Name = "E-Mail Financeiro")]
        public string EmailFinanceiro { get; set; }

        [Display(Name = "E-Mail Gerencial")]
        public string EmailGerencial { get; set; }


        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Telefone Principal")]
        [DataType(DataType.PhoneNumber)]
        public string TelefonePrincipal { get; set; }

        [Display(Name = "Telefone Comercial")]
        [DataType(DataType.PhoneNumber)]
        public string TelefoneComercial { get; set; }

        [Display(Name = "Telefone Financeiro")]
        [DataType(DataType.PhoneNumber)]
        public string TelefoneFinanceiro { get; set; }



        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "UF")] 
        public string UF { get; set; }

        


        public Empresa( double cNPJ_CPF, string nome, string fantasia, string telefone, string uf)
        {
            Id = Guid.NewGuid();
            CNPJ_CPF = cNPJ_CPF;
            Nome = nome;
            Fantasia = fantasia;
            TelefonePrincipal = telefone;
            UF = uf;
        }
    }
}
