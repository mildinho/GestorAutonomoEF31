using System.Collections.Generic;

namespace GestorAutonomo.Models
{
    public interface IImagemRepository
    {

        void Cadastrar(Imagem imagem);
        void CadastrarImagens(List<Imagem> ListaImagens, int ProdutoId);


        void Excluir(int Id);

        void ExcluirImagensProduto(int ProdutoId);


    }
}
