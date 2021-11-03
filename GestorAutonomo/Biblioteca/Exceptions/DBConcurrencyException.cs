﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GestorAutonomo.Biblioteca.Exceptions

{
    public class DBConcurrencyException : ApplicationException
    {
        public DBConcurrencyException(string msg) : base(msg)
        {

        }
    }
}
