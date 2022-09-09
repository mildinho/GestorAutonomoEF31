using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorAutonomo.Domain.Entities
{
    [Table("CS_LOGIN")]
    public class Login : ModelBase
    {


        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Informe o E-Mail Válido")]
        public string EMail { get; set; }


        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(255, ErrorMessage = "Mínimo de 3 até 255", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        public Login()
        {
        }

        public Login(Guid empresa, string eMail, string password)
        {
            Id = Guid.NewGuid();
            EmpresaId = empresa;
            EMail = eMail;
            Password = password;

        }
    }
}
