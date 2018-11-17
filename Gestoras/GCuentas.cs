using System.Collections.Generic;
using Aguiñagalde.Interfaces;
using Aguiñagalde.Entidades;
using Aguiñagalde.FabricaMappers;
using System.Data;
using System;

namespace Aguiñagalde.Gestoras
{
    public class GCuentas
    {
       

        private static GCuentas _Instance = null;
        private static IMapperCuentas  DBCuentas { get; set; }
        private GCuentas()
        {
            DBCuentas = (IMapperCuentas)Factory.getMapper(this.GetType());
        }
        public static GCuentas getInstance()
        {
            if (_Instance == null)
                _Instance = new GCuentas();
            return _Instance;
        }
        public List<MovimientoGeneral> PendientesByCliente(ClienteActivo xCliente)
        {
            List<MovimientoGeneral> Movimientos = new List<MovimientoGeneral>();
            foreach(object Obj in DBCuentas.getMovimientosPendiente(xCliente.IdCliente))
            {
                MovimientoGeneral M = (MovimientoGeneral)Obj; 
                Movimientos.Add(M);
            }
            return Movimientos;
        }

        public decimal getImporteByFactura(int xNumero, string xSerie)
        {
            return DBCuentas.getImporte(xNumero, xSerie);
        }



        

       

        public DataTable DetalleFactura(string xSerie,int xNumero)
        {
            return (DataTable)DBCuentas.DetalleFactura(xSerie, xNumero);
        }

        public DataTable DetallePosicion(string xSerie, int xNumero)
        {
            return (DataTable)DBCuentas.DetallePosicion(xSerie, xNumero);
        }

        
    }


}
