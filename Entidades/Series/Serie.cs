using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguiñagalde.Entidades
{
    public class  Serie
    {
        private string _serie;
        private int _nro;

        public string NroSerie
        {
            get
            {
                return _serie;
            }
        }

        public int ID
        {
            get
            {
                return _nro;
            }
        }

        public Serie(string xNombre,int xNumero)
        {
            _serie = xNombre;
            _nro = xNumero;
        }

    }
}
