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


        public CRUD()
        {

        }
    }

    public enum Opcoes
    {
        Create = 0,
        Read = 1,
        Update = 2,
        Delete = 3
            
    }
}
