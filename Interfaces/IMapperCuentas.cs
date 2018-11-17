using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aguiñagalde.Entidades;

namespace Aguiñagalde.Interfaces
{
    public interface IMapperCuentas:IMapper 
    {
        List<object> getMovimientosPendiente(int xCliente);

       

        

        object DetalleFactura(string xSerie, int xNumero);

        object DetallePosicion(string xSerie, int xNumero);
        decimal getImporte(int xNumero, string xSerie);
       
    }
}
