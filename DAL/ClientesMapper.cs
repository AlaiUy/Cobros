using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aguiñagalde.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Aguiñagalde.Entidades;

namespace Aguiñagalde.DAL
{
    public class ClientesMapper : DataAccess, IMapperClientes
    {
       

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

        public int getClienteIDByCedula(string xCodCliente)
        {
            int Numero = -1;
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT TOP 1 CODCLIENTE as NUMERO FROM  CLIENTES WHERE (ALIAS = @CODCLIENTE OR CODCLIENTE = @CODCLIENTE)", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@CODCLIENTE", xCodCliente));
                    Numero = (int)ExecuteScalar(Com);
                }
            }
            return Numero;
            
        }

        public object ObtenerClientes()
        {
            DataTable DT;
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT ISNULL(CONVERT(VARCHAR(8),CODCLIENTE),'') CODIGO, ISNULL(ALIAS,'') AS CEDULA,ISNULL(CIF,'') as RUT,ISNULL(NOMBRECLIENTE,'') NOMBRE,ISNULL(DIRECCION1,'') AS DIRECCION,ISNULL(CONVERT(VARCHAR(8),TELEFONO1),'') AS TELEFONO FROM  CLIENTES WHERE (TIPO > 0) and DESCATALOGADO = 'F' ", (SqlConnection)Con))
                {
                    DT = new DataTable();
                    DT.Load(ExecuteReader(Com));
                }
            }
            return DT;

           
        }



        public object getSimpleByID(int xID)
        {
            object Cliente = null;
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT top 1 CODCLIENTE AS ID,NOMBRECLIENTE AS NOMBRE ,NOMBRECOMERCIAL AS NOMBRECOMERCIAL,CIF AS RUT,TELEFONO1 AS TELEFONO,TELEFONO2 AS CELULAR,PAIS AS PAIS,PROVINCIA AS DPTO,ZONA AS COBRADOR,CODPOSTAL AS POSTAL,DIRECCION1 AS DIRECCION,DIRECCION2 AS DIRECCIONOPCIONAL,TIPO,RIESGOCONCEDIDO AS TOPE,LCREDITO AS LINEA,DIAPAGO1 AS CIERRE,OBSERVACIONES AS OBS,DESCATALOGADO,FAX,CODMONEDA AS MONEDA,CONVERT(VARCHAR(12),FECHANACIMIENTO,103) AS FECHAN,ALIAS AS CEDULA,ACTIVO,ACTIVOCDIA,SOLOCONORDEN,FIDELIZADO,SOLOPESOS,DIC,NOBLOQUEAR,TIPOINTERNO,NUMTARJETA,SOLOTITULAR FROM CLIENTES WHERE CODCLIENTE = @CODCLIENTE", Con))
                {
                    Com.Parameters.Add(new SqlParameter("@CODCLIENTE", xID));
                    using (IDataReader Reader = ExecuteReader(Com))
                    {
                        while (Reader.Read())
                        {
                            Cliente  = getClienteFromReader(Reader);
                        }
                    }
                }
           }
            return Cliente;
        }


        private ClienteActivo getClienteFromReader(IDataReader Reader)
        {
            ClienteActivo Temporal = null;
            try
            {
                CamposLibres CL = null;
                int ID = (int)(Reader["ID"]);
                string nombre = (string)(Reader["NOMBRE"]);
                string Cedula = (string)(Reader["CEDULA"] is DBNull ? string.Empty : Reader["CEDULA"]);
                using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
                {
                    Con.Open();
                    using (SqlCommand Com = new SqlCommand("SELECT top 1 ACTIVIDAD,REF_COMERCIALES,ANTIGUEDAD,OBSERVACIONES_,OBSERVACIONES1,TARJ_CREDITO,MAIL,VEHICULOS,BIENES,ESTADO_CIVIL,ALQUILER,INGRESOS1,ACTIVIDAD1,CARGO,INGRESOS,CONYUGE FROM CLIENTESCAMPOSLIBRES WHERE CODCLIENTE = @CODCLIENTE", Con))
                    {
                        Com.Parameters.Add(new SqlParameter("@CODCLIENTE", ID));
                        using (IDataReader R = ExecuteReader(Com))
                        {
                            while (R.Read())
                            {
                                CL = getCLFromReader(R, ID);
                            }
                        }
                    }
                }
                Temporal = new ClienteActivo(ID, nombre,CL, Cedula);


                Temporal.Nombre = nombre;
                Temporal.IsBloqueo = getDBoolean((string)(Reader["NOBLOQUEAR"] is DBNull ? bool.FalseString : Reader["NOBLOQUEAR"]));
                Temporal.DIC = getDBoolean((string)(Reader["DIC"] is DBNull ? bool.FalseString : Reader["DIC"]));
                Temporal.isActivo = getDBoolean((string)(Reader["ACTIVO"] is DBNull ? bool.TrueString : Reader["ACTIVO"]));
                Temporal.isActivoDia = getDBoolean((string)(Reader["ACTIVOCDIA"] is DBNull ? bool.TrueString : Reader["ACTIVOCDIA"]));
                Temporal.isFidelizado = getDBoolean((string)(Reader["FIDELIZADO"] is DBNull ? bool.TrueString : Reader["FIDELIZADO"]));
                Temporal.isMonedaUnica = getDBoolean((string)(Reader["SOLOPESOS"] is DBNull ? bool.TrueString : Reader["SOLOPESOS"]));
                Temporal.isOrden = getDBoolean((string)(Reader["SOLOCONORDEN"] is DBNull ? bool.TrueString : Reader["SOLOCONORDEN"]));
                Temporal.NombreComercial = (string)(Reader["NOMBRECOMERCIAL"] is DBNull ? string.Empty : Reader["NOMBRECOMERCIAL"]);
                Temporal.Rut = (string)(Reader["RUT"] is DBNull ? string.Empty : Reader["RUT"]);
                Temporal.Telefono = (string)(Reader["TELEFONO"] is DBNull ? string.Empty : Reader["TELEFONO"]);
                Temporal.Celular = (string)(Reader["CELULAR"] is DBNull ? string.Empty : Reader["CELULAR"]);
                Temporal.Pais = (string)(Reader["PAIS"] is DBNull ? string.Empty : Reader["PAIS"]);
                Temporal.Dpto = (string)(Reader["DPTO"] is DBNull ? string.Empty : Reader["DPTO"]);
                Temporal.Cobrador = (string)(Reader["COBRADOR"] is DBNull ? string.Empty : Reader["COBRADOR"]);
                Temporal.Postal = (string)(Reader["POSTAL"] is DBNull ? string.Empty : Reader["POSTAL"]);
                Temporal.Direccion = (string)(Reader["DIRECCION"] is DBNull ? string.Empty : Reader["DIRECCION"]);
                Temporal.DireccionAlternativa = (string)(Reader["DIRECCIONOPCIONAL"] is DBNull ? string.Empty : Reader["DIRECCIONOPCIONAL"]);
                Temporal.Type = Convert.ToInt32((Reader["TIPO"] is DBNull ? 0 : Reader["TIPO"]));
                Temporal.Tope = Convert.ToDecimal((Reader["TOPE"] is DBNull ? 1 : Reader["TOPE"]));
                Temporal.Lineacredito = Convert.ToDecimal((Reader["LINEA"] is DBNull ? 1 : Reader["LINEA"]));
                Temporal.Cierre = Convert.ToByte((Reader["CIERRE"] is DBNull ? 0 : Reader["CIERRE"]));
                Temporal.Observaciones = (string)(Reader["OBS"] is DBNull ? string.Empty : Reader["OBS"]);
                Temporal.Descatalogado = Convert.ToBoolean((Reader["DESCATALOGADO"] as string == "T") ? true : false);
                Temporal.Fax = (string)(Reader["FAX"] is DBNull ? string.Empty : Reader["FAX"]);
                Temporal.Fecha = Convert.ToDateTime((Reader["FECHAN"] is DBNull ? DateTime.MinValue : Reader["FECHAN"]));
                Temporal.Tarifa = (Tarifa)getTarifaByCliente(ID);
                Temporal.SubCuentas = getSubCuentasByCliente(ID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Temporal;
        }

        private ClienteActivo getClienteFromReader(IDataReader Reader, CamposLibres xCP)
        {
            ClienteActivo Temporal = null;
            try
            {

                int ID = (int)(Reader["ID"]);
                if (xCP == null)
                    xCP = new CamposLibres(ID);
                string nombre = (string)(Reader["NOMBRE"]);
                string Cedula = (string)(Reader["CEDULA"] is DBNull ? string.Empty : Reader["CEDULA"]);
                Temporal = new ClienteActivo(ID, nombre, xCP, Cedula);

                
                Temporal.Nombre = nombre;
                Temporal.IsBloqueo = getDBoolean((string)(Reader["NOBLOQUEAR"] is DBNull ? bool.FalseString : Reader["NOBLOQUEAR"]));
                Temporal.DIC = getDBoolean((string)(Reader["DIC"] is DBNull ? bool.FalseString : Reader["DIC"]));
                Temporal.isActivo = getDBoolean((string)(Reader["ACTIVO"] is DBNull ? bool.TrueString : Reader["ACTIVO"]));
                Temporal.isActivoDia = getDBoolean((string)(Reader["ACTIVOCDIA"] is DBNull ? bool.TrueString : Reader["ACTIVOCDIA"]));
                Temporal.isFidelizado = getDBoolean((string)(Reader["FIDELIZADO"] is DBNull ? bool.TrueString : Reader["FIDELIZADO"]));
                Temporal.isMonedaUnica = getDBoolean((string)(Reader["SOLOPESOS"] is DBNull ? bool.TrueString : Reader["SOLOPESOS"]));
                Temporal.isOrden = getDBoolean((string)(Reader["SOLOCONORDEN"] is DBNull ? bool.TrueString : Reader["SOLOCONORDEN"]));
                Temporal.NombreComercial = (string)(Reader["NOMBRECOMERCIAL"] is DBNull ? string.Empty : Reader["NOMBRECOMERCIAL"]);
                Temporal.Rut = (string)(Reader["RUT"] is DBNull ? string.Empty : Reader["RUT"]);
                Temporal.Telefono = (string)(Reader["TELEFONO"] is DBNull ? string.Empty : Reader["TELEFONO"]);
                Temporal.Celular = (string)(Reader["CELULAR"] is DBNull ? string.Empty : Reader["CELULAR"]);
                Temporal.Pais = (string)(Reader["PAIS"] is DBNull ? string.Empty : Reader["PAIS"]);
                Temporal.Dpto = (string)(Reader["DPTO"] is DBNull ? string.Empty : Reader["DPTO"]);
                Temporal.Cobrador = (string)(Reader["COBRADOR"] is DBNull ? string.Empty : Reader["COBRADOR"]);
                Temporal.Postal = (string)(Reader["POSTAL"] is DBNull ? string.Empty : Reader["POSTAL"]);
                Temporal.Direccion = (string)(Reader["DIRECCION"] is DBNull ? string.Empty : Reader["DIRECCION"]);
                Temporal.DireccionAlternativa = (string)(Reader["DIRECCIONOPCIONAL"] is DBNull ? string.Empty : Reader["DIRECCIONOPCIONAL"]);
                Temporal.Type = Convert.ToInt32((Reader["TIPO"] is DBNull ? 0 : Reader["TIPO"]));
                Temporal.Tope = Convert.ToDecimal((Reader["TOPE"] is DBNull ? 1 : Reader["TOPE"]));
                Temporal.Lineacredito = Convert.ToDecimal((Reader["LINEA"] is DBNull ? 1 : Reader["LINEA"]));
                Temporal.Cierre = Convert.ToByte((Reader["CIERRE"] is DBNull ? 0 : Reader["CIERRE"]));
                Temporal.Observaciones = (string)(Reader["OBS"] is DBNull ? string.Empty : Reader["OBS"]);
                Temporal.Descatalogado = Convert.ToBoolean((Reader["DESCATALOGADO"] as string == "T") ? true : false);
                Temporal.Fax = (string)(Reader["FAX"] is DBNull ? string.Empty : Reader["FAX"]);
                Temporal.Fecha = Convert.ToDateTime((Reader["FECHAN"] is DBNull ? DateTime.MinValue : Reader["FECHAN"]));
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Temporal;
        }

        public int getClienteIDRut(string ID)
        {
            int Numero = -1;
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT TOP 1 CODCLIENTE as NUMERO FROM  CLIENTES WHERE (CIF = @CODCLIENTE)", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@CODCLIENTE", ID));
                    Numero = (int)ExecuteScalar(Com);
                }
            }
            return Numero;
        }

        private List<SubCuenta> getSubCuentasByCliente(int xCodCliente)
        {
            List<SubCuenta> SubCuentas = new List<SubCuenta>();
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT CODCLIENTE AS TITULAR,CODSUBCTA AS CODIGO, NOMBRECLIENTE AS NOMBRE,DIRECCION,TELEFONO,DESCATALOGADO,TIPOCUENTA as TIPO,RUT,CELULAR FROM CLIENTESSUBCUENTAS WHERE CODCLIENTE = @CODCLIENTE", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@CODCLIENTE", xCodCliente));
                    using (IDataReader Reader = ExecuteReader(Com))
                    {
                        while (Reader.Read())
                        {
                            SubCuentas.Add((SubCuenta)getSubCuentaFromReader(Reader));
                        }
                    }
                }
            }
            return SubCuentas;



            

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
                Temporal.Direccion = (string)(Reader["DIRECCION"] is DBNull ? string.Empty : Reader["DIRECCION"]);
                Temporal.Descatalogado = getDBoolean((Reader["DESCATALOGADO"]));
                Temporal.Rut = (string)(Reader["RUT"] is DBNull ? string.Empty : Reader["RUT"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Temporal;
        }

        public object getTarifaByCliente(int ID)
        {
            object obj = null;

            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT top 1  TV.idtarifav as CODIGO,T.DESCRIPCION FROM tarifascliente as TV INNER JOIN TARIFASVENTA T ON TV.IDTARIFAV = T.IDTARIFAV where codcliente = @codcliente", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@CODCLIENTE", ID));
                    using (IDataReader Reader = ExecuteReader(Com))
                    {
                        if (Reader.Read())
                        {
                            obj = (Tarifa)getTarifaFromReader(Reader);
                        }
                    }
                }
            }
            return obj;

        }

        private Tarifa getTarifaFromReader(IDataReader rd)
        {
            Tarifa Temporal = null;
            try
            {

                int idTarifa = (int)(rd["CODIGO"] is DBNull ? 2 : rd["CODIGO"]);
                string xdesc = (string)(rd["DESCRIPCION"] is DBNull ? "CREDITO" : rd["DESCRIPCION"]);
                Temporal = new Tarifa();
                Temporal.ID = idTarifa;
                Temporal.Nombre = xdesc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Temporal;
        }

        private CamposLibres getCLFromReader(IDataReader Reader, int xCodCliente)
        {
            CamposLibres Temporal = null;
            int ID = xCodCliente;
            Temporal = new CamposLibres(ID);
            Temporal.Actividad = (string)(Reader["ACTIVIDAD"] is DBNull ? string.Empty : Reader["ACTIVIDAD"]);
            Temporal.Comerciales = (string)(Reader["REF_COMERCIALES"] is DBNull ? string.Empty : Reader["REF_COMERCIALES"]);
            Temporal.Antiguedad = (string)(Reader["ANTIGUEDAD"] is DBNull ? string.Empty : Reader["ANTIGUEDAD"]);
            Temporal.OtrasObservaciones = (string)(Reader["OBSERVACIONES_"] is DBNull ? string.Empty : Reader["OBSERVACIONES_"]);
            Temporal.Plasticos = (string)(Reader["TARJ_CREDITO"] is DBNull ? string.Empty : Reader["TARJ_CREDITO"]);
            Temporal.Email = (string)(Reader["MAIL"] is DBNull ? string.Empty : Reader["MAIL"]);
            Temporal.Vehiculos = (string)(Reader["VEHICULOS"] is DBNull ? string.Empty : Reader["VEHICULOS"]);
            Temporal.Bienes = (string)(Reader["BIENES"] is DBNull ? string.Empty : Reader["BIENES"]);
            Temporal.Civil = (string)(Reader["ESTADO_CIVIL"] is DBNull ? string.Empty : Reader["ESTADO_CIVIL"]);
            Temporal.Alquiler = (string)(Reader["ALQUILER"] is DBNull ? string.Empty : Reader["ALQUILER"]);
            Temporal.ConyugeIngresos = (int)(Reader["INGRESOS1"] is DBNull ? 0 : Reader["INGRESOS1"]);
            Temporal.ConyugeActividad = (string)(Reader["ACTIVIDAD1"] is DBNull ? string.Empty : Reader["ACTIVIDAD1"]);
            Temporal.Cargo = (string)(Reader["CARGO"] is DBNull ? string.Empty : Reader["CARGO"]);
            Temporal.Ingresos = (int)(Reader["INGRESOS"] is DBNull ? 0 : Reader["INGRESOS"]);
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
    }
}
