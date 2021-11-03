using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_EMPRESA")]
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "CNPJ / CNPJ")]
        [Required]
        public double CNPJ_CPF { get; set; }

        [Display(Name = "Nome")]
        [Required]
        public String Nome { get; set; }

        [Display(Name = "Fantasia")]
        [Required]
        public string Fantasia { get; set; }

        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.Date)]
        public DateTime Data_Cadastro { get; set; }

        [Display(Name = "Último ACesso")]
        [DataType(DataType.Date)]
        public DateTime Data_Ultimo_Acesso { get; set; }

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

        [Display(Name = "Data do Bloqueio")]
        [DataType(DataType.Date)]
        public DateTime Bloqueado { get; set; }

        [Display(Name = "Histórico")]
        public string Historico { get; set; }

        [Display(Name = "E-Mail")]
        public string Email { get; set; }



    }
}
