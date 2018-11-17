using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;
using System.Data;
using Aguiñagalde.Entidades;

namespace Aguiñagalde.DAL
{
    public abstract class DataAccess
    {

        private static string globalConnectionString;

        private static string generalConnectionString;


        public static string GlobalConnectionString
        {
            get
            {
                if (globalConnectionString == null)
#if DEBUG
                    globalConnectionString = ConfigurationManager.ConnectionStrings["Servidor2AguinaG"].ConnectionString;
#else
                globalConnectionString = ConfigurationManager.ConnectionStrings["ServidorAguinaG"].ConnectionString;
#endif


                return DataAccess.globalConnectionString;
            }
            set { DataAccess.globalConnectionString = value; }
        }

        public static string GeneralConnectionString
        {
            get
            {
                if (generalConnectionString == null)
#if DEBUG
                    generalConnectionString = ConfigurationManager.ConnectionStrings["Servidor2Gestion"].ConnectionString;
#else
                generalConnectionString = ConfigurationManager.ConnectionStrings["ServidorGestion"].ConnectionString;
#endif

                return DataAccess.generalConnectionString;
            }
            set { DataAccess.generalConnectionString = value; }
        }

        public abstract int ExecuteNonQuery(DbCommand cmd);


        public abstract int ExecuteNonQuery(DbCommand cmd, List<IDataParameter> Lts);



        public abstract IDataReader ExecuteReader(DbCommand cmd);

        public abstract IDataReader ExecuteReader(DbCommand cmd, List<IDataParameter> Lts);


        public abstract object ExecuteScalar(DbCommand cmd, List<IDataParameter> Lts);


        public abstract object ExecuteScalar(DbCommand cmd);


        protected bool getDBoolean(object Value)
        {
            string V = (string)Value;
            if (V == "T")
                return true;
            return false;
        }

        protected char setDBoolean(bool Value)
        {
            if (Value)
                return 'T';
            return 'F';
        }



        protected object GetValueFromProperty(object Value)
        {
            object LastValue = Value;
            if (LastValue == null)
                return System.DBNull.Value;

            switch (Type.GetTypeCode(Value.GetType()))
            {
                case TypeCode.String: // Valor recibido de la property es un texto
                    string Valor = (string)Value;
                    if (Valor.Length < 1)
                    {
                        LastValue = System.DBNull.Value;
                    }
                    return LastValue;
                case TypeCode.Int32: // Valor recibido de la property es un entero
                    if ((int)Value == 0)
                    {
                        LastValue = System.DBNull.Value;
                    }
                    return LastValue;
                case TypeCode.DateTime:
                    return (DateTime)LastValue;
                case TypeCode.Boolean:
                    if ((Boolean)LastValue)
                        return 'T';
                    else
                        return 'F';
            }
            return LastValue;
        }

        protected void CerrarConexion(IDbConnection CN)
        {
            if (CN.State == ConnectionState.Open)
                CN.Close();
        }
        protected DateTime getFechaFromReader(IDataReader rd)
        {
            DateTime ztmp = DateTime.MinValue;
            try
            {
                ztmp = Convert.ToDateTime((rd["FECHA"] is DBNull ? DateTime.MinValue : rd["FECHA"]));
                return ztmp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected object getMovimientoFromReader(IDataReader Reader, List<Moneda> xListaMonedas,bool xRecibo)
        {
           

            int Numero,cTarifa, Tipo, SubCta, codFormaPago, Remesa, Moneda, zSaldado, zCodCliente, zNumeroDoc, xTipoCliente, Tipopago;
            string Serie, sDoc, TipoDoc, Estado, Origen,apunte,Descripcion;
            DateTime FV, FS, Fecha, VC;
            decimal Mora, fMoneda, Importe;
            byte Linea;


            Moneda = (int)(Reader["MONEDA"]);
            fMoneda = Convert.ToDecimal((Reader["FMONEDA"] is DBNull ? 1 : Reader["FMONEDA"]));
            Tipo = (int)(Reader["NUMEROTIPO"] is DBNull ? 0 : Reader["NUMEROTIPO"]);
            Importe = (decimal)(Reader["IMPORTE"]);
            Moneda M = xListaMonedas.Find(xObj => xObj.Codmoneda == Moneda);
            Numero = (int)(Reader["NUMERO DE DOCUMENTO"]);
            Serie = (string)(Reader["SERIE DE DOCUMENTO"] is DBNull ? string.Empty : Reader["SERIE DE DOCUMENTO"]);
            Fecha = Convert.ToDateTime((Reader["FECHA DEL DOCUMENTO"]));
            Linea = Convert.ToByte((Reader["POSICION"]));
            fMoneda = Convert.ToDecimal((Reader["FMONEDA"] is DBNull ? 1 : Reader["FMONEDA"]));
            SubCta = (int)(Reader["SUBCTA"] is DBNull ? 0 : Reader["SUBCTA"]);
            zNumeroDoc = (int)(Reader["NUMERODOC"] is DBNull ? -1 : Reader["NUMERODOC"]);

            if (SubCta < 1)
                SubCta = 1;

            if (xRecibo)
            {
                return new MovimientoRecibo(Numero, Serie, Fecha, Linea, fMoneda,Importe,SubCta,M,Tipo);
            }
            else
            {
                MovimientoGeneral Temporal = null;
                cTarifa = (int)(Reader["TARIFA"] is DBNull ? 1 : Reader["TARIFA"]);
               
                
                codFormaPago = Convert.ToInt32((Reader["FORMAPAGO"]));
                Remesa = Convert.ToInt32((Reader["REMESA"] is DBNull ? -1 : Reader["REMESA"]));
                
                Tipopago = Convert.ToInt32((Reader["TIPOPAGO"]));
                zSaldado = (int)(Reader["ZSALDADO"] is DBNull ? -1 : Reader["ZSALDADO"]);
                zCodCliente = (int)(Reader["CLIENTE"] is DBNull ? -1 : Reader["CLIENTE"]);
                
                sDoc = (string)(Reader["SERIEDOC"] is DBNull ? string.Empty : Reader["SERIEDOC"]);
                Origen = (string)(Reader["ORIGEN"] is DBNull ? string.Empty : Reader["ORIGEN"]);
                apunte = (string)(Reader["APUNTE"] is DBNull ? string.Empty : Reader["APUNTE"]);
                Descripcion = (string)(Reader["DESCRIPCION"] is DBNull ? string.Empty : Reader["DESCRIPCION"]);
                TipoDoc = (string)(Reader["TIPODOC"]);
                Estado = (string)(Reader["ESTADO"]);
                FV = Convert.ToDateTime((Reader["VENCIMIENTO"]));
                FS = Convert.ToDateTime((Reader["SALDADO"]));
                VC = Convert.ToDateTime((Reader["PRECIOCONTADO"] is DBNull ? DateTime.MinValue : Reader["PRECIOCONTADO"]));
                Mora = Convert.ToDecimal((Reader["MORA"] is DBNull ? 0 : Reader["MORA"]));
                
                xTipoCliente = (int)(Reader["tipocliente"] is DBNull ? -1 : Reader["tipocliente"]);
                
                Temporal = new MovimientoGeneral(Numero, Serie, Descripcion, Importe, Fecha, (Moneda)M, Linea, Origen, cTarifa, fMoneda,SubCta,zNumeroDoc);
                Temporal.Mora = Mora;
                Temporal.Codcliente = zCodCliente;
                Temporal.Factormoneda = fMoneda;
                Temporal.GenApunte = apunte;
                Temporal.TipoPago = Tipopago;
                Temporal.FormaPago = codFormaPago;
                Temporal.Estado = Estado;
                Temporal.FechaVencimiento = FV;
                Temporal.Numeroremesa = Remesa;
                Temporal.Tipodocumento = TipoDoc;
                Temporal.Saldado = FS;
                Temporal.Zsaldado = zSaldado;
                Temporal.SerieDoc = sDoc;
                Temporal.VencimientoContado = VC;
                Temporal.Tipocliente = xTipoCliente;
                Temporal.CFE = (CFE)CuentasMapper.getCFEByFactura(Numero, Serie);
                return Temporal;
                
            }
        }



        public int ExecuteNonQuery(DbCommand cmd, List<IDataParameter> Lts, List<string> xQuerys)
        {
            foreach (IDataParameter P in Lts)
            {
                cmd.Parameters.Add(P);
            }

            try
            {
                if (xQuerys.Count > 0)
                {
                    foreach (string S in xQuerys)
                    {
                        cmd.CommandText = S;
                        cmd.ExecuteNonQuery();
                    }

                }
                return 1;
            }
            catch (Exception E)
            {
                CerrarConexion(cmd.Connection);
                throw E;
            }
        }

        public int ExecuteNonQuery(DbCommand cmd, IDataParameter Param)
        {
            try
            {
                cmd.Parameters.Add(Param);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                CerrarConexion(cmd.Connection);
                throw new Exception(e.Message);
            }
        }

    }
}
