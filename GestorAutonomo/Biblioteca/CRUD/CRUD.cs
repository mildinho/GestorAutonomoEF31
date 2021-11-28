using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Biblioteca.CRUD
{
    public class CRUD
    {
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public string Descricao { get; set; }
        public Opcoes Operacao { get; set; }

        public CRUD(string titulo, string subTitulo, string descricao, Opcoes operacao)
        {
            Titulo = titulo;
            SubTitulo = subTitulo;
            Descricao = descricao;
            Operacao = operacao;
        }
        public CRUD()
        {
            Titulo = "";
            SubTitulo = "";
            Descricao = "";
            Operacao = Opcoes.Information;

        }
    }

    public enum Opcoes
    {
        Create = 0,
        Read = 1,
        Update = 2,
        Delete = 3,
        Information = 4
            
    }
}
