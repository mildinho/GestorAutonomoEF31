using GestorAutonomo.Data;
using GestorAutonomo.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace GestorAutonomo.Repositories
{
    public class ImagemRepository : IImagemRepository
    {


        private readonly GestorAutonomoContext _context;
        public ImagemRepository(GestorAutonomoContext context)
        {
            _context = context;

        }
        public void Cadastrar(Imagem imagem)
        {

            _context.Add(imagem);
            _context.SaveChanges();
        }

        public void CadastrarImagens(List<Imagem> ListaImagens, int ProdutoId)
        {
            if (ListaImagens != null && ListaImagens.Count > 0)
            {

                foreach (var item in ListaImagens)
                {
                    Cadastrar(item);
                }

            }

        }

        public void Excluir(int Id)
        {
            Imagem obj = _context.imagems.Find(Id);
            _context.Remove(obj);
            _context.SaveChanges();
        }

        public void ExcluirImagensProduto(int ProdutoId)
        {
            List<Imagem> obj = _context.imagems.Where(a => a.ProdutoId == ProdutoId).ToList();

            foreach (Imagem item in obj)
            {
                _context.Remove(item);

            }
            _context.SaveChanges();
        }
    }
}
