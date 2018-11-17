using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguiñagalde.Entidades
{
    public class Tarifa : IComparable
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _Nombre;

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public override string ToString()
        {
            return _Nombre;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 0;
            Tarifa T = (Tarifa)obj;
            if (T.ID < this.ID)
                return 1;
            else
                return -1;
        }
    }
}
