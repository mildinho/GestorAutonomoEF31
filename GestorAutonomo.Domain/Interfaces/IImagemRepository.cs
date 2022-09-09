using GestorAutonomo.Domain.Entities;
using System;
using System.Collections.Generic;

namespace GestorAutonomo.Models
{
    public interface IImagemRepository
    {

        void Cadastrar(Imagem imagem);
        void CadastrarImagens(List<Imagem> ListaImagens, Guid ProdutoId);


        void Excluir(Guid Id);

        void ExcluirImagensProduto(Guid ProdutoId);


    }
}
