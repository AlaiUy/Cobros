using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aguiñagalde.Entidades;
using Aguiñagalde.Interfaces;
using Aguiñagalde.FabricaMappers;
using System.Data;

namespace Aguiñagalde.Gestoras
{
    public class GClientes
    {
        private static IMapperClientes DBClientes { get; set; }
        private static GClientes _Instance = null;
        private static readonly object padlock = new object();
        
        private DataTable DTClientes;

        public DataTable TablaClientes
        {
            get { return DTClientes.Copy(); }
            set { DTClientes = value; }
        }

        public void Iniciar()
        {
            DBClientes = (IMapperClientes)Factory.getMapper(this.GetType());
            DTClientes = (DataTable)DBClientes.ObtenerClientes();

        }

        public static GClientes Instance()
        {
            if (_Instance == null)
            {
                lock (padlock)
                {
                    if (_Instance == null)
                        _Instance = new GClientes();
                }
            }
                
            return _Instance;
        }

        public ClienteActivo getByID(string ID, bool xSimple)
        {

            if (ID.Length > 12)
                throw new Exception("No se ecuentra el cliente");

            if (ID.Length < 1)
                throw new Exception("No se ecuentra el cliente");

            if (!Tools.Numeros.IsNumeric(ID))
                throw new Exception("El codigo debe ser solo numeros");

            int IDCliente = -1;


            if (ID.ToString().Length > 5 && ID.ToString().Length < 9)
            {
                if (Tools.Numeros.Cedula(Convert.ToInt32(ID)))
                {
                    IDCliente = DBClientes.getClienteIDByCedula(ID);
                }
                else
                {
                    throw new Exception("No se ecuentra el cliente");
                }
            }
            else if (ID.Length > 10 && ID.Length < 13)
            {
                IDCliente = DBClientes.getClienteIDRut(ID);
            }
            else
            {
                IDCliente = Convert.ToInt32(ID);
            }


            ClienteActivo Obj;
            Obj = (ClienteActivo)DBClientes.getSimpleByID(IDCliente);

            if (Obj == null)
            {
                throw new Exception("No se ecuentra el cliente");
            }
            
            return Obj;
        }
    }
}
