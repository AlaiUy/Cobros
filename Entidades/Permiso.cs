using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguiñagalde.Entidades
{
    public class Permiso
    {
        int _idPermiso;

        public Permiso(int xID)
        {
            _idPermiso = xID;
        }

        public int IdPermiso
        {
            get
            {
                return _idPermiso;
            }
        }
    }
}
