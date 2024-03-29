﻿using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Infra.Data.Context;
using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestorAutonomo.Infra.Data.Repositories
{
    public class ImagemRepository : IImagemRepository
    {


        private readonly Context.DBContexto _context;
        public ImagemRepository(Context.DBContexto context)
        {
            _context = context;

        }
        public void Cadastrar(Imagem imagem)
        {

            _context.Add(imagem);
            _context.SaveChanges();
        }

        public void CadastrarImagens(List<Imagem> ListaImagens, Guid ProdutoId)
        {
            if (ListaImagens != null && ListaImagens.Count > 0)
            {

                foreach (var item in ListaImagens)
                {
                    Cadastrar(item);
                }

            }

        }

        public void Excluir(Guid Id)
        {
            Imagem obj = _context.Imagems.Find(Id);
            _context.Remove(obj);
            _context.SaveChanges();
        }

        public void ExcluirImagensProduto(Guid ProdutoId)
        {
            List<Imagem> obj = _context.Imagems.Where(a => a.ProdutoId == ProdutoId).ToList();

            foreach (Imagem item in obj)
            {
                _context.Remove(item);

            }
            _context.SaveChanges();
        }
    }
}
