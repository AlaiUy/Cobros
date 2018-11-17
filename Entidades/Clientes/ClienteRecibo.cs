using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguiñagalde.Entidades.Clientes
{
    public class ClienteRecibo : Persona
    {
        public ClienteRecibo(int xIdCliente, string xNombre,string xDireccion,string xCedula) : base(xIdCliente, xNombre, xCedula)
        {
            base.Direccion = xDireccion;
        }
    }
}
