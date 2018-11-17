using Aguiñagalde.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguiñagalde.Interfaces
{
    public interface IMapperCobros:IMapper
    {


        object getCajaByID(string xNombreMaquina, object xUser);
        decimal getCotizacion();

        List<Moneda> getListaMonedas();

        object Parametros(string xNombreMaquina, List<int> Indexs);
        Empresa getClavesEmpresa(byte xSucursal);
        Usuario getUsuario(string xUsuario, string xPassword);
        void CambiarClaveUsuario(int xcodUsuario, string xpassword);

        Moneda getMonedaByID(int Moneda);

        //byte getPosicion(int xNumero, string xSerie);

        //int ActualizarMovimientos(List<object> xMovimientos, string xSerieRecibos);

    
        

        int getIdClienteByRecibo(string xSerie, int xID);

        List<object> getMovimientosByRecibo(string xSerie, int xID,int xCliente);

        int getMonedaByRecibo(string xSerie, int xID);

        object getAllRecibos(int xZ,string xCaja);
        void UpdateParameters(List<Config> xLista, string xNombreEquipo);
        bool PuedoCobrar(int z,string xCaja);
        void AnularMovimientos(List<object> list, List<object> xRemitos, object xClaves, object xCajaGeneral, bool xImprimir);
        Hashtable getSaldo(int xCodCliente);
        int GenerarEntrega(object entrega,object xCaja);

        int GenerarRemitos(object xRe, object xClaves, object xCajaGeneral, bool xImprimir);
        int GenerarRecibo(List<object> xMovimientos, object xCaja, List<object> xRemitos, bool Imprimir,object xClavesEmpresa);
    }
}
