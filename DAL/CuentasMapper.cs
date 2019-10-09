using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aguiñagalde.DAL;
using Aguiñagalde.Interfaces;
using Aguiñagalde.Entidades;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace Aguiñagalde.DAL
{
    public class CuentasMapper : DataAccess, IMapperCuentas
    {

        List<Moneda> _ListaMonedas;

        private IDbConnection _Connection;
        public IDbConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new SqlConnection(GlobalConnectionString);
                }
                return _Connection;
            }

        }

        public void Delete(object o)
        {
            throw new Exception("No implementado");
        }

        public void Add(object o)
        {
            throw new Exception("No implementado");
        }
        public void Update(object o)
        {
            throw new Exception("No implementado");
        }




        public CuentasMapper()
        {
            _ListaMonedas = new List<Moneda>();
            _ListaMonedas = General.getInstance().getMonedas();
        }
        

        public List<object> getSubCuentasByCliente(int xCodCliente)
        {
            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@CODCLIENTE", xCodCliente));
            DbCommand Command = new SqlCommand("SELECT CODCLIENTE AS TITULAR,CODSUBCTA AS CODIGO, NOMBRECLIENTE AS NOMBRE,DIRECCION,TELEFONO,DESCATALOGADO,TIPOCUENTA as TIPO,RUT,CELULAR FROM CLIENTESSUBCUENTAS WHERE CODCLIENTE = @CODCLIENTE", (SqlConnection)Connection);
            System.Data.IDataReader rd = null;
            List<object> SubCuentas = new List<object>();
            SubCuenta Entity = null;
            if (_Connection.State == System.Data.ConnectionState.Closed)
                _Connection.Open();
            rd = ExecuteReader(Command, P);
            while (rd.Read())
            {
                Entity = (SubCuenta)getSubCuentaFromReader(rd);
                SubCuentas.Add(Entity);
            }
            rd.Close();
            CerrarConexion(_Connection);
            return SubCuentas;

        }

        


        


        

        public List<object> getMovimientosPendiente(int xCliente)
        {
           
            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@CLIENTE", xCliente));
            DbCommand Command = new SqlCommand("SELECT V.VENCIMIENTOCONTADO AS PRECIOCONTADO,DBO.JL_TARIFABYFACTURA(T.SERIE,T.NUMERO) AS TARIFA,DBO.JL_TIPOBYCODCLIENTE(t.codigointerno) as tipocliente,T.SERIEDOC,t.NUMERODOC,t.codigointerno as CLIENTE,V.TIPODOC as NUMEROTIPO,T.MORA AS MORA,T.CAJASALDADO AS CAJASALDADO,T.ZSALDADO AS ZSALDADO,T.FACTORMONEDA AS FMONEDA,T.ORIGEN,T.GENAPUNTE AS APUNTE,T.CODTIPOPAGO AS TIPOPAGO,T.CODFORMAPAGO AS FORMAPAGO,MAX(T.POSICION) AS POSICION,MAX(D.DESCRIPCION) AS DESCRIPCION, MAX(T.FECHADOCUMENTO) AS 'FECHA DEL DOCUMENTO', MAX(T.SERIE) AS 'SERIE DE DOCUMENTO', MAX(T.NUMERO) AS 'NUMERO DE DOCUMENTO',CAST(SUM(T.IMPORTE) AS DECIMAL(16, 2)) AS IMPORTE,T.CODMONEDA AS MONEDA,T.ESTADO AS ESTADO,T.FECHAVENCIMIENTO AS VENCIMIENTO,T.NUMEROREMESA AS REMESA, T.TIPODOCUMENTO AS TIPODOC,T.FECHASALDADO AS SALDADO,SUBCTA FROM TESORERIA AS T LEFT OUTER JOIN FACTURASVENTA AS V ON T.SERIE = V.NUMSERIE AND T.NUMERO = V.NUMFACTURA AND T.N = V.N LEFT OUTER JOIN SERIES AS D ON V.NUMSERIE = D.SERIE WHERE (T.ORIGEN = 'C' AND (T.TIPODOCUMENTO='F' OR T.TIPODOCUMENTO ='L')) AND (T.CODIGOINTERNO = @CLIENTE) AND T.N='B'  AND  (T.ESTADO='P') GROUP BY V.VENCIMIENTOCONTADO,T.SERIEDOC,t.NUMERODOC,t.codigointerno,V.TIPODOC,T.MORA,T.CAJASALDADO,T.ZSALDADO,T.FACTORMONEDA,T.ORIGEN,T.GENAPUNTE,T.CODTIPOPAGO,T.CODFORMAPAGO,T.FECHADOCUMENTO,T.SERIE, T.NUMERO,T.CODMONEDA,T.ESTADO,T.FECHAVENCIMIENTO,T.NUMEROREMESA, T.TIPODOCUMENTO,T.FECHASALDADO,SUBCTA,T.POSICION ORDER BY T.FECHADOCUMENTO ASC ", (SqlConnection)Connection);
            System.Data.IDataReader rd = null;
            List<object> Movimientos = new List<object>();
            MovimientoGeneral Entity = null;

            if (Connection.State == System.Data.ConnectionState.Closed)
                _Connection.Open();
            rd = ExecuteReader(Command, P);
            while (rd.Read())
            {
                Entity = (MovimientoGeneral)getMovimientoFromReader(rd,_ListaMonedas,false);

                Movimientos.Add(Entity);
            }
            rd.Close();
            CerrarConexion(Connection);

            foreach (Movimiento M in Movimientos)
            {
                ((Movimiento)M).CFE = (CFE)getCFEByFactura(M.Numero, M.Serie);
            }
            return Movimientos;
        }

        public static object getCFEByFactura(int Numero, string Serie)
        {
            CFE Temporal = null;

            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT top 1 * from TESORERIACFE where SERIEFAC = @SERIE and NUMEROFAC = @NUMERO", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@NUMERO", Numero));
                    Com.Parameters.Add(new SqlParameter("@SERIE", Serie));
                    using (IDataReader Reader = Com.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                                int Tipo = (int)(Reader["TIPOCFE"] is DBNull ? 0 : Reader["TIPOCFE"]);
                                string CFESerie = (string)(Reader["SERIECFE"] is DBNull ? string.Empty : Reader["SERIECFE"]);
                                int CFENumero = (int)(Reader["NUMEROCFE"] is DBNull ? 0 : Reader["NUMEROCFE"]);
                                string Link = (string)(Reader["LINKCFE"] is DBNull ? string.Empty : Reader["LINKCFE"]);
                                string Albaran = (string)(Reader["SERIEALB"] is DBNull ? string.Empty : Reader["SERIEALB"]);
                                int NAlbaran = (int)(Reader["NUMEROALB"] is DBNull ? 0 : Reader["NUMEROALB"]);
                                string SerieF = (string)(Reader["SERIEFAC"] is DBNull ? string.Empty : Reader["SERIEFAC"]);
                                int NumeroF = (int)(Reader["NUMEROFAC"] is DBNull ? 0 : Reader["NUMEROFAC"]);
                                Temporal = new CFE(Tipo, CFESerie, CFENumero, Link, Albaran, NAlbaran, SerieF, NumeroF);
                            }
                        }
                    }
                }
            return Temporal;
        }

            

            



        public object DetalleFactura(string xSerie, int xNumero)
        {
            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@NUMERO", xNumero));
            P.Add(new SqlParameter("@SERIE", xSerie));
            DbCommand Command = new SqlCommand("SELECT LIN.REFERENCIA, LIN.DESCRIPCION, LIN.UNID1 AS 'TOTAL DE UNIDADES', CAST(LIN.PRECIOIVA AS DECIMAL(16,2))AS 'PRECIO CON IVA', CAST(LIN.TOTAL+(LIN.TOTAL*LIN.IVA/100) AS DECIMAL(16,2)) AS 'TOTAL CON IVA' FROM ALBVENTALIN AS LIN INNER JOIN ALBVENTACAB CAB ON (LIN.NUMALBARAN = CAB.NUMALBARAN AND LIN.NUMSERIE = CAB.NUMSERIE AND LIN.N = CAB.N) INNER JOIN FACTURASVENTA AS FV ON (FV.NUMSERIE = CAB.NUMSERIE AND FV.N = CAB.N AND FV.NUMFACTURA = CAB.NUMFAC) WHERE CAB.N = 'B' AND FV.NUMSERIE = @SERIE AND FV.NUMFACTURA = @NUMERO", (SqlConnection)Connection);
            if (Connection.State == System.Data.ConnectionState.Closed)
                Connection.Open();
            DataTable DT = new DataTable();
            DT.Load(ExecuteReader(Command, P));
            CerrarConexion(Connection);
            return DT;
        }

        public object DetallePosicion(string xSerie, int xNumero)
        {
            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@NUMERO", xNumero));
            P.Add(new SqlParameter("@SERIE", xSerie));
            DataTable DT = new DataTable();
            DbCommand Command = new SqlCommand("SELECT POSICION, FECHAVENCIMIENTO, FECHASALDADO, IMPORTE, CODMONEDA, MORA, ESTADO, SUDOCUMENTO AS SERIE, NUMEROREMESA AS NUMERO FROM TESORERIA  WHERE ORIGEN = 'C' AND N = 'B' AND SERIE = @SERIE AND NUMERO = @NUMERO", (SqlConnection)Connection);
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
                DT.Load(ExecuteReader(Command, P));
                CerrarConexion(Connection);
            }
                
            return DT;
        }
        private SubCuenta getSubCuentaFromReader(IDataReader Reader)
        {
            SubCuenta Temporal = null;
            try
            {

                int idcliente = Convert.ToInt32((Reader["TITULAR"] is DBNull ? 0 : Reader["TITULAR"]));
                int codigo = Convert.ToInt32((Reader["CODIGO"] is DBNull ? 0 : Reader["CODIGO"]));
                Temporal = new SubCuenta(idcliente, codigo);
                Temporal.Telefono = (string)(Reader["TELEFONO"] is DBNull ? 0 : Reader["TELEFONO"]);
                Temporal.Nombre = (string)(Reader["NOMBRE"] is DBNull ? string.Empty : Reader["NOMBRE"]);
                Temporal.Tipo = (string)(Reader["TIPO"] is DBNull ? string.Empty : Reader["TIPO"]);
                Temporal.Celular = (string)(Reader["CELULAR"] is DBNull ? string.Empty : Reader["CELULAR"]);
                Temporal.Descatalogado = getDBoolean((Reader["DESCATALOGADO"]));
                Temporal.Rut = (string)(Reader["RUT"] is DBNull ? string.Empty : Reader["RUT"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Temporal;
        }

        

        
       

        public override int ExecuteNonQuery(DbCommand cmd)
        {
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                CerrarConexion(cmd.Connection);
                throw e;
            }
        }

        public override int ExecuteNonQuery(DbCommand cmd, List<IDataParameter> Lts)
        {
            {
                foreach (IDataParameter P in Lts)
                {
                    cmd.Parameters.Add(P);
                }

                try
                {
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception E)
                {
                    CerrarConexion(cmd.Connection);
                    throw E;
                }
            }
        }

        public override IDataReader ExecuteReader(DbCommand cmd)
        {
            try
            {
                return cmd.ExecuteReader(CommandBehavior.Default);
            }
            catch (Exception e)
            {
                CerrarConexion(cmd.Connection);
                throw e;
            }
        }

        public override IDataReader ExecuteReader(DbCommand cmd, List<IDataParameter> Lts)
        {
            foreach (IDataParameter P in Lts)
            {
                cmd.Parameters.Add(P);
            }
            try
            {
                return cmd.ExecuteReader(CommandBehavior.Default);
            }
            catch (Exception e)
            {
                CerrarConexion(cmd.Connection);
                throw e;
            }

        }

        public override object ExecuteScalar(DbCommand cmd, List<IDataParameter> Lts)
        {
            {
                foreach (IDataParameter P in Lts)
                {
                    cmd.Parameters.Add(P);
                }
                try
                {
                    return cmd.ExecuteScalar();
                }
                catch (Exception e)
                {
                    CerrarConexion(cmd.Connection);
                    throw e;
                }

            }
        }

        public override object ExecuteScalar(DbCommand cmd)
        {
            try
            {
                return cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                CerrarConexion(cmd.Connection);
                throw e;
            }
        }

        public decimal getImporte(int xNumero, string xSerie)
        {
            decimal Importe = 0;
            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@SERIE", xSerie));
            P.Add(new SqlParameter("@NUMERO", xNumero));
            DbCommand Command = new SqlCommand("SELECT TOP 1 ISNULL(TOTALNETO,0) AS NUMERO FROM  FACTURASVENTA WHERE N = 'B' AND NUMSERIE = @SERIE AND  NUMFACTURA = @NUMERO", (SqlConnection)Connection);
            if (Connection.State == System.Data.ConnectionState.Closed)
                Connection.Open();
            Importe = Convert.ToDecimal(ExecuteScalar(Command, P));
            CerrarConexion(_Connection);
            return Importe;
        }
    }
}
