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
        public int Id { get; set; }

        [Display(Name = "CNPJ / CNPJ")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        //[MinLength(11, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
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
        public DateTime Data_Cadastro { get; set; }

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

        [Display(Name = "UF")]
        public string UF { get; set; }

        [Display(Name = "CEP")]
        public string CEP { get; set; }

        [Display(Name = "E-Mail")]
        [EmailAddress(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E004")]
        public string Email { get; set; }

        [Display(Name = "E-Mail Comercial")]
        public string EmailComercial { get; set; }

        [Display(Name = "E-Mail Financeiro")]
        public string EmailFinanceiro { get; set; }

        [Display(Name = "E-Mail Gerencial")]
        public string EmailGerencial { get; set; }

        public Empresa(int id, double cNPJ_CPF, string nome, string fantasia, DateTime data_Cadastro)
        {
            Id = id;
            CNPJ_CPF = cNPJ_CPF;
            Nome = nome;
            Fantasia = fantasia;
            Data_Cadastro = data_Cadastro;
        }
    }
}
