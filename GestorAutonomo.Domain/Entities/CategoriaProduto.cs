﻿using GestorAutonomo.Domain.Biblioteca.Lang;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorAutonomo.Domain.Entities
{
    [Table("CS_CATEGORIAPRODUTO")]
    public class CategoriaProduto : ModelBase
    {

    
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
        [Display(Name = "Nome")]
        public string Descricao { get; set; }


        [Display(Name = "Categoria Pai")]
        public Guid? CategoriaPaiId { get; set; }

        [ForeignKey("CategoriaPaiId")]
        public virtual CategoriaProduto CategoriaPai { get; set; }

      
       public CategoriaProduto()
        {
        }


        public CategoriaProduto(Guid empresa, string descricao, Guid? categoriaPaiId)
        {
            Id = Guid.NewGuid(); ;
            EmpresaId = empresa;
            Descricao = descricao;
            CategoriaPaiId = categoriaPaiId;
           
        }
    }
}
