using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguiñagalde.Entidades
{
    public class Config
    {
        private string _dato;
        private int _nro;

        public string Dato
        {
            get
            {
                return _dato;
            }
        }

        public int ID
        {
            get
            {
                return _nro;
            }
        }

        public Config(string xNombre, int xNumero)
        {
            _dato = xNombre;
            _nro = xNumero;
        }
    }
}
