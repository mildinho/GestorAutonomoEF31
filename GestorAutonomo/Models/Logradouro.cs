using GestorAutonomo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    public class Logradouro : ModelBase
    {
        [Required]
        [ForeignKey("TipoLogradouro")]
        [Display(Name = "Tipo de Logradouro")]
        public Guid TipoLogradouroId { get; set; }
        virtual public TipoLogradouro TipoLogradouro { get; set; }



        [Required]
        [ForeignKey("Parceiro")]
        [Display(Name = "Parceiro")]
        public Guid ParceiroId { get; set; }


        [Display(Name = "Endereço")]
        public string Endereco { get; set; }


        [Display(Name = "Número")]
        public string Numero { get; set; }


        [Display(Name = "Complemento")]
        public string Complemento { get; set; }


        [Display(Name = "Bairro")]
        public string Bairro { get; set; }


        [Display(Name = "Ponto de Referência")]
        public string PontoReferencia { get; set; }

        [ForeignKey("Municipio")]
        [Display(Name = "Cidade")]
        public Guid MunicipioId { get; set; }
        virtual public Municipio Municipio { get; set; }


        virtual public UF UF { get; set; }

        [Display(Name = "CEP")]
        public string CEP { get; set; }




    }
}
