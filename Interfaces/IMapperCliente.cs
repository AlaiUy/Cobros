using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguiñagalde.Interfaces
{
    public interface  IMapperClientes:IMapper 
    {

        int getClienteIDByCedula(string xCodCliente);

        object getTarifaByCliente(int ID);

        int getClienteIDRut(string ID);

        object getSimpleByID(int xID);

        object ObtenerClientes();
    }
}
