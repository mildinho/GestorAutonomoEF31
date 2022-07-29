﻿using GestorAutonomo.Biblioteca.Lang;
using GestorAutonomo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Models
{
    [Table("CS_PRODUTOSALDO")]
    public class ProdutoSaldo : ModelBase
    {
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [ForeignKey("Produto")]
        [Display(Name = "Código do Produto")]
        public Guid ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }


        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [ForeignKey("PontosEstoque")]
        [Display(Name = "Código do Ponto de Produto")]
        public Guid PontosEstoqueId { get; set; }
        public virtual PontosEstoque PontosEstoque { get; set; }



        [Display(Name = "Saldo")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double Saldo { get; set; }

        [Display(Name = "Reserva")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double Reserva { get; set; }

    


        public ProdutoSaldo()
        {
                
        }


        public ProdutoSaldo(Guid id, Guid PontoEstoque, int SaldoInicial, int ReservaInicial)
        {
            ProdutoId = id;
            PontosEstoqueId = PontoEstoque;
            Saldo = SaldoInicial;
            Reserva = ReservaInicial;
        }
    }



  
}
