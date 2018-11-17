using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguiñagalde.Entidades
{
    public class FPago
    {
        string mID;

        public string ID
        {
            get { return mID; }
            set { mID = value; }
        }

        string mNombre;

        public string Nombre
        {
            get { return mNombre; }
            set { mNombre = value; }
        }

        public FPago()
        {

        }

        public FPago(string xID, string xNombre)
        {
            mNombre = xNombre;
            mID = xID;
        }

        public override string ToString()
        {
            return mNombre;
        }
    }
}
