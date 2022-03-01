using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_LOGIN")]
    public class Login
    {


        [Key]
        [Display(Name = "Código")]

        public int Id { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Informe o E-Mail Válido")]
        public string EMail { get; set; }

        
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(255, ErrorMessage = "Mínimo de 3 até 255", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

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



        public Login()
        {
        }

        public Login(int id, string eMail, string password)
        {
            Id = id;
            EMail = eMail;
            Password = password;
           
        }
    }
}
