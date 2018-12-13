using Aguiñagalde.Entidades;
using Aguiñagalde.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;
using Aguiñagalde.XMLManager;
using System.Xml;
using System.IO;

namespace Aguiñagalde.DAL
{
    public class CobrosMapper : DataAccess, IMapperCobros
    {
        private List<Moneda> _ListaMonedas;


        public CobrosMapper()
        {
            _ListaMonedas = new List<Moneda>();
            foreach (Moneda M in getMonedas())
            {
                _ListaMonedas.Add(M);

            }


            //SetRegion();
        }


        public List<object> getMonedas()
        {
            object ObjMoneda = null;
            List<object> Monedas = new List<object>();
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT top 2 CODMONEDA AS ID,DESCRIPCION AS NOMBRE, INICIALES,MORA FROM MONEDAS where codmoneda between 1 and 2 Order by CodMoneda asc", Con))
                {
                    using (IDataReader Reader = ExecuteReader(Com))
                    {
                        while (Reader.Read())
                        {
                            ObjMoneda = getMonedaFromReader(Reader);
                            if (((Moneda)ObjMoneda).Descripcion.Length > 0)
                                Monedas.Add(ObjMoneda);
                        }
                    }
                }
            }
            return Monedas;
        }


        public enum Series
        {
            RECIBOS = 20,
            DESCUENTO = 21,
            MORA = 19,
            DEBITO = 23,
            ENTREGA = 62,
            DESCUENTOINCO = 22
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





        public Usuario getUsuario(string xUsuario, string xPassword)
        {
            Usuario U = null;

            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT TOP 1 IDUSER,NOMBRE,PASSWORD,EMAIL,PASSEMAIL,EMAILHOST,CODVENDEDOR,NOMBRE_REAL FROM USUARIOS WHERE NOMBRE = @USUARIO AND PASSWORD = @PASSWORD", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@USUARIO", xUsuario));
                    Com.Parameters.Add(new SqlParameter("@PASSWORD", xPassword));
                    using (IDataReader Reader = ExecuteReader(Com))
                    {
                        if (Reader.Read())
                        {
                            U = getUsuarioFromReader(Reader);
                        }
                    }
                }
            }

            return U;
        }

        private Usuario getUsuarioFromReader(IDataReader Reader)
        {
            Usuario U = null;
            int IdUsuario = ((int)Reader["IDUSER"]);
            List<Permiso> L = getPermisos(IdUsuario);
            U = new Usuario(IdUsuario, L);
            U.CodVendedor = (int)(Reader["CODVENDEDOR"] is DBNull ? 0 : Reader["CODVENDEDOR"]);
            U.Email = (string)(Reader["EMAIL"] is DBNull ? string.Empty : Reader["EMAIL"]);
            U.EmailHost = (string)(Reader["EMAILHOST"] is DBNull ? string.Empty : Reader["EMAILHOST"]);
            U.Nombre = (string)(Reader["NOMBRE"] is DBNull ? string.Empty : Reader["NOMBRE"]);
            U.PassEmail = (string)(Reader["PASSEMAIL"] is DBNull ? string.Empty : Reader["PASSEMAIL"]);
            U.Password = (string)(Reader["PASSWORD"] is DBNull ? string.Empty : Reader["PASSWORD"]);
            U.Nombre = (string)(Reader["NOMBRE_REAL"] is DBNull ? string.Empty : Reader["NOMBRE_REAL"]);
            U.NombreUsuario = (string)(Reader["NOMBRE"] is DBNull ? string.Empty : Reader["NOMBRE"]);
            return U;
        }
        private List<Permiso> getPermisos(int xUsuario)
        {

            List<Permiso> Permisos = new List<Permiso>();
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT IDPERMISO FROM PERMISOSUSUARIO WHERE IDUSUARIO =  @ID", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@ID", xUsuario));
                    using (IDataReader Reader = ExecuteReader(Com))
                    {
                        while (Reader.Read())
                            Permisos.Add(getPermisoFromReader(Reader));
                    }
                }
            }
            return Permisos;
        }



        public object getAllRecibos(int xZ, string xCaja)
        {

            DataTable DT;
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT A.CODIGOINTERNO,DBO. JL_NOMBREBYCODIGO(A.CODIGOINTERNO) AS NOMBRE,dbo.JL_TIPOBYCODIGO(a.codigointerno) as TIPO,A.SUDOCUMENTO AS SERIE,A.NUMEROREMESA AS NUMERO,SUM(A.IMPORTE) AS IMPORTE,a.codmoneda AS MONEDA FROM TESORERIA AS A WHERE A.ORIGEN='C' AND A.NUMEROREMESA > 0 AND A.ZSALDADO = @Z AND A.CAJASALDADO = @CAJA GROUP BY A.CODIGOINTERNO,A.CODMONEDA,A.SUDOCUMENTO,A.NUMEROREMESA HAVING SUM(A.IMPORTE) > 0 order by NUMEROREMESA ASC ", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@Z", xZ));
                    Com.Parameters.Add(new SqlParameter("@CAJA", xCaja));
                    DT = new DataTable();
                    DT.Load(ExecuteReader(Com));
                }
            }
            return DT;
        }



        public List<Moneda> getListaMonedas()
        {
            return _ListaMonedas;
        }



        private Moneda getMonedaFromReader(IDataReader Reader)
        {
            try
            {
                Moneda Entity = new Moneda((int)Reader["ID"], (string)Reader["Nombre"]);
                Entity.Iniciales = (string)(Reader["INICIALES"] is DBNull ? 0 : Reader["INICIALES"]);
                Entity.Mora = (decimal)Convert.ToDouble(((Reader["MORA"] is DBNull ? 0 : Reader["MORA"])));
                return Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public decimal getCotizacion()
        {
            decimal Cotizacion = -1;
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT GENERAL.DBO.MICOTIZACION(GETDATE(),@MONEDA) AS DECIMAL", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@MONEDA", 2));
                    Cotizacion = Convert.ToDecimal(ExecuteScalar(Com));
                }
            }
            return Convert.ToDecimal(Cotizacion);
        }



        private byte getPosicion(int xNumero, string xSerie, IDbConnection xCon, IDbTransaction xTransaccion)
        {
            byte Pos = 0;

            using (SqlCommand Com = new SqlCommand("SELECT MAX(POSICION) AS BYTE FROM TESORERIA WHERE ORIGEN = 'C' AND NUMERO = @NUMERO AND SERIE = @SERIE", (SqlConnection)xCon))
            {
                Com.Transaction = (SqlTransaction)xTransaccion;
                Com.Parameters.Add(new SqlParameter("@NUMERO", xNumero));
                Com.Parameters.Add(new SqlParameter("@SERIE", xSerie));
                Pos = Convert.ToByte(ExecuteScalar(Com));
            }
            return Pos;
        }

        public int getIdClienteByRecibo(string xSerie, int xId)
        {
            int Z = -1;
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT top 1 CODIGOINTERNO AS NUMERO FROM TESORERIA WHERE NUMEROREMESA = @ID and SUDOCUMENTO = @SERIE", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@ID", xId));
                    Com.Parameters.Add(new SqlParameter("@SERIE", xSerie));
                    Z = (int)ExecuteScalar(Com);
                }
            }
            return Z;
        }

        public List<object> getMovimientosByRecibo(string xSerie, int xID, int xCliente)
        {
            List<object> Movimientos = new List<object>();
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT V.VENCIMIENTOCONTADO AS PRECIOCONTADO,DBO.JL_TARIFABYFACTURA(T.SERIEDOC,t.NUMERODOC) AS TARIFA,DBO.JL_TIPOBYCODCLIENTE(t.codigointerno) as tipocliente,T.SERIEDOC,t.NUMERODOC,t.codigointerno as cliente,V.TIPODOC as NUMEROTIPO,T.MORA AS MORA,T.CAJASALDADO AS CAJASALDADO,T.ZSALDADO AS ZSALDADO,T.FACTORMONEDA AS FMONEDA,T.ORIGEN,T.GENAPUNTE AS APUNTE,T.CODTIPOPAGO AS TIPOPAGO,T.CODFORMAPAGO AS FORMAPAGO,MAX(T.POSICION) AS POSICION,MAX(D.DESCRIPCION) AS DESCRIPCION, MAX(T.FECHADOCUMENTO) AS 'FECHA DEL DOCUMENTO', MAX(T.SERIE) AS 'SERIE DE DOCUMENTO', MAX(T.NUMERO) AS 'NUMERO DE DOCUMENTO',CAST(SUM(T.IMPORTE) AS DECIMAL(16, 2)) AS IMPORTE,T.CODMONEDA AS MONEDA,T.ESTADO AS ESTADO,T.FECHAVENCIMIENTO AS VENCIMIENTO,T.NUMEROREMESA AS REMESA, T.TIPODOCUMENTO AS TIPODOC,T.FECHASALDADO AS SALDADO,T.SUBCTA FROM TESORERIA AS T LEFT OUTER JOIN FACTURASVENTA AS V ON T.SERIE = V.NUMSERIE AND T.NUMERO = V.NUMFACTURA AND T.N = V.N LEFT OUTER JOIN SERIES AS D ON V.NUMSERIE = D.SERIE WHERE     (T.ORIGEN = 'C') AND t.codigointerno = @CLIENTE and (T.NUMEROREMESA = @RECIBO and t.sudocumento = @SERIE) GROUP BY V.VENCIMIENTOCONTADO,T.SERIEDOC,t.NUMERODOC,t.codigointerno,V.TIPODOC,T.MORA,T.CAJASALDADO,T.ZSALDADO,T.FACTORMONEDA,T.ORIGEN,T.GENAPUNTE,T.CODTIPOPAGO,T.CODFORMAPAGO,T.FECHADOCUMENTO,T.SERIE, T.NUMERO,T.CODMONEDA,T.ESTADO,T.FECHAVENCIMIENTO,T.NUMEROREMESA, T.TIPODOCUMENTO,T.FECHASALDADO,T.SUBCTA", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@RECIBO", xID));
                    Com.Parameters.Add(new SqlParameter("@SERIE", xSerie));
                    Com.Parameters.Add(new SqlParameter("@CLIENTE", xCliente));
                    using (IDataReader Reader = ExecuteReader(Com))
                    {
                        while (Reader.Read())
                        {
                            Movimientos.Add(getMovimientoFromReader(Reader, _ListaMonedas, true));
                        }
                    }
                }
            }
            return Movimientos;
        }

        public int GenerarRecibo(List<object> xMovimientos, object xCaja, List<object> xRemitos, bool Imprimir, object xClavesEmpresa)
        {
            int NumRecibo = -1;
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlTransaction Tran = Con.BeginTransaction())
                {
                    try
                    {
                        CajaGeneral C = (CajaGeneral)xCaja;
                        NumRecibo = ActualizarMovimientos(xMovimientos, C.Recibos, Con, Tran);
                        if (NumRecibo > 0)
                        {
                            if (xRemitos != null && xRemitos.Count > 0)
                            {
                                foreach (object o in xRemitos)
                                {
                                    try
                                    {
                                        Remito Re = (Remito)o;
                                        Re.Recibo = NumRecibo;
                                        Re.SerieRecibo = C.Recibos;
                                        Re.Caja = C.Id;
                                        Re.Z = C.Z;
                                        int NumeroRemito = GenerarRemitos(Re, xClavesEmpresa, C, Imprimir, Con, Tran);
                                    }
                                    catch (Exception e)
                                    {
                                        throw new Exception(e.Message + " -- " + "Print And Save");
                                    }
                                }
                            }
                            
                        }
                    }
                    catch (Exception E)
                    {
                        throw E;
                    }
                    Tran.Commit();
                }
            }
            return NumRecibo;
        }

        public int GenerarRemitos(object xRe, object xClaves, object xCajaGeneral, bool xImprimir)
        {
            int Numero = -1;
            Empresa Claves = (Empresa)xClaves;
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlTransaction Tran = Con.BeginTransaction())
                {
                    try
                    {
                        Remito R = (Remito)xRe;
                        Numero = NumeroRecibo(R.Serie, Con, Tran, R.TipoDoc());
                        GuardarCabecera(R, Con, Tran, Numero);
                        GuardarVentaLin(R, Con, Tran, Numero);
                        GuardarFVentas(R, Con, Tran, Numero);
                        GuardatVentasTotales(R, Con, Tran, Numero);
                        GuardarTesoreria(R, Con, Tran, Numero);
                        if (R.Comentario.Length > 0)
                            GuardarComentario(R, Numero, ((CajaGeneral)xCajaGeneral).Usuario.CodUsuario, Con, Tran);
                        ImprimirRemito(R, Numero, Claves, (CajaGeneral)xCajaGeneral, xImprimir, Con, Tran);
                        Tran.Commit();
                    }
                    catch (Exception E)
                    {
                        throw E;
                    }
                }

            }
            return Numero;
        }

        public int GenerarRemitos(object xRe, object xClaves, object xCajaGeneral, bool xImprimir,IDbConnection xCon,IDbTransaction xTran)
        {
            int Numero = -1;
            Empresa Claves = (Empresa)xClaves;
            
                    try
                    {
                        Remito R = (Remito)xRe;
                        Numero = NumeroRecibo(R.Serie, xCon, xTran, R.TipoDoc());
                        GuardarCabecera(R, xCon, xTran, Numero);
                        GuardarVentaLin(R, xCon, xTran, Numero);
                        GuardarFVentas(R, xCon, xTran, Numero);
                        GuardatVentasTotales(R, xCon, xTran, Numero);
                        GuardarTesoreria(R, xCon, xTran, Numero);
                        if (R.Comentario.Length > 0)
                            GuardarComentario(R, Numero, ((CajaGeneral)xCajaGeneral).Usuario.CodUsuario, xCon, xTran);
                        ImprimirRemito(R, Numero, Claves, (CajaGeneral)xCajaGeneral, xImprimir, xCon, xTran);
                       
                    }
                    catch (Exception E)
                    {
                        throw E;
                    }
            return Numero;

           
        }

        private void ImprimirRemito(Remito xRemito, int xNumeroRemito, Empresa xClaves, CajaGeneral xCaja, bool xImprimir, IDbConnection xCon, IDbTransaction xTran)
        {

            XMLInfo.getInstance().GenerarXMLRemito(xRemito, xNumeroRemito, xClaves, xCaja, xImprimir);
            //GenerarXMLRemito(xRemito, xNumeroRemito, xClaves, xCaja, xImprimir);
            string xFile = "RET" + xRemito.Serie + xNumeroRemito;
            if (XMLInfo.getInstance().LeerXMLRetorno(xFile, xCaja))
            {
                CFE gCFE = LeerCFERetorno(xFile, xRemito, xCaja);
                if (gCFE != null)
                    GuardarCFE(gCFE, xCon, xTran);
            }
        }

        public void GenerarXMLRemito(Remito xR, int xNumero, Empresa Claves, CajaGeneral xCaja, bool xImprimir)
        {
            Remito R = (Remito)xR;
            //XmlTextWriter Writer = new XmlTextWriter(xCaja.EntradaCFE.Trim() + xR.Serie + xR.Numero + ".xml", Encoding.UTF8);
            XmlTextWriter Writer = new XmlTextWriter(xCaja.TemporalCFE.Trim() + xR.Serie + xNumero + ".xml", Encoding.UTF8);

            Writer.WriteStartDocument();
            Writer.WriteStartElement("EnvioCFE");
            Writer.WriteStartElement("Encabezado");
            Writer.WriteStartElement("EmpCodigo");
            Writer.WriteString(Claves.CodEmpresa.ToString());
            Writer.WriteEndElement();
            Writer.WriteStartElement("EmpPK");
            Writer.WriteString(Claves.EmpPK);
            Writer.WriteEndElement();
            Writer.WriteStartElement("EmpCA");
            Writer.WriteString(Claves.Clave.ToString());
            Writer.WriteEndElement();
            Writer.WriteEndElement();

            Writer.WriteStartElement("CFE");

            Writer.WriteStartElement("CFEItem");

            Writer.WriteStartElement("IdDoc");

            Writer.WriteStartElement("CFETipoCFE");
            Writer.WriteValue(R.NumeroCFE());
            Writer.WriteEndElement();
            Writer.WriteStartElement("CFESerie");

            Writer.WriteEndElement();
            Writer.WriteStartElement("CFENro");

            Writer.WriteEndElement();
            Writer.WriteStartElement("CFEImpresora");
            Writer.WriteString(xCaja.Impresora);
            Writer.WriteEndElement();
            Writer.WriteStartElement("CFEImp");
            if (xImprimir)
                Writer.WriteString("S");
            else
                Writer.WriteString("N");
            Writer.WriteEndElement();
            Writer.WriteStartElement("CFEImpCantidad");
            Writer.WriteValue(1);
            Writer.WriteEndElement();
            Writer.WriteStartElement("CFEFchEmis");
            Writer.WriteString(R.Fecha.ToString("yyyy-MM-dd"));
            Writer.WriteEndElement();
            Writer.WriteStartElement("CFEPeriodoDesde");

            Writer.WriteEndElement();
            Writer.WriteStartElement("CFEPeriodoHasta");

            Writer.WriteEndElement();
            Writer.WriteStartElement("CFEMntBruto");
            Writer.WriteValue(1);
            Writer.WriteEndElement();
            Writer.WriteStartElement("CFEFmaPago");
            // revisar aca///
            Writer.WriteValue(1);
            ////
            Writer.WriteEndElement();
            Writer.WriteStartElement("CFEFchVenc");
            Writer.WriteString(DateTime.Today.ToShortDateString());
            Writer.WriteEndElement();
            Writer.WriteStartElement("CFETipoTraslado");
            Writer.WriteValue(1);
            Writer.WriteEndElement();
            Writer.WriteStartElement("CFEAdenda");
            Writer.WriteString(R.Adenda());
            Writer.WriteEndElement();
            Writer.WriteStartElement("CAESeq");
            Writer.WriteString("0");
            Writer.WriteEndElement();


            //            Writer.WriteEndElement();
            Writer.WriteStartElement("CFENumReferencia");
            Writer.WriteValue(1);

            Writer.WriteEndElement();


            //Writer.WriteEndElement();
            Writer.WriteStartElement("CFEImpFormato");

            Writer.WriteValue(1);

            Writer.WriteEndElement();
            Writer.WriteStartElement("CFEIdCompra");
            Writer.WriteValue(0);

            Writer.WriteEndElement();
            Writer.WriteStartElement("CFEQrCode");
            Writer.WriteValue(1);
            Writer.WriteEndElement();
            Writer.WriteStartElement("CFEDatosAvanzados");
            Writer.WriteValue(1);
            Writer.WriteEndElement();
            Writer.WriteStartElement("CFERepImpresa");
            Writer.WriteValue(1);
            Writer.WriteEndElement();
            Writer.WriteEndElement();




            #region Emisor
            /* DATOS EMISOR */
            Writer.WriteStartElement("Emisor");

            Writer.WriteStartElement("EmiRznSoc");
            Writer.WriteString("Ferreteria y Barraca Aguiñagalde");
            Writer.WriteEndElement();
            Writer.WriteStartElement("EmiComercial");
            Writer.WriteString("Hector B. Aguiñagalde");
            Writer.WriteEndElement();
            Writer.WriteStartElement("EmiGiroEmis");
            //'.WriteString("NI IDEA")
            Writer.WriteEndElement();
            Writer.WriteStartElement("EmiTelefono");
            Writer.WriteString("25106");
            Writer.WriteEndElement();
            Writer.WriteStartElement("EmiTelefono2");
            //'.WriteString("473 20501");
            Writer.WriteEndElement();
            Writer.WriteStartElement("EmiCorreoEmisor");
            Writer.WriteString("eticket@aguinagalde.com.uy");
            Writer.WriteEndElement();
            Writer.WriteStartElement("EmiSucursal");
            Writer.WriteString("1");
            Writer.WriteEndElement();
            Writer.WriteStartElement("EmiDomFiscal");
            Writer.WriteString("Barbieri 1080");
            Writer.WriteEndElement();
            Writer.WriteStartElement("EmiCiudad");
            Writer.WriteString("Salto");
            Writer.WriteEndElement();
            Writer.WriteStartElement("EmiDepartamento");
            Writer.WriteString("Salto");
            Writer.WriteEndElement();
            Writer.WriteStartElement("EmiInfAdicional");
            Writer.WriteEndElement();
            Writer.WriteEndElement();
            #endregion


            #region Receptor

            Writer.WriteStartElement("Receptor");
            Writer.WriteStartElement("RcpTipoDocRecep");
            Writer.WriteValue(R.Cliente.TipoDocumento(R.IS.Codigo));
            Writer.WriteEndElement();
            Writer.WriteStartElement("RcpTipoDocDscRecep");
            Writer.WriteString("");
            Writer.WriteEndElement();
            Writer.WriteStartElement("RcpCodPaisRecep");
            Writer.WriteString("UY");
            Writer.WriteEndElement();
            Writer.WriteStartElement("RcpDocRecep");
            Writer.WriteString(R.Cliente.Documento(R.IS.Codigo));
            Writer.WriteEndElement();
            //acordate aca porner el subcuenta de la bonificacion corresponditnete;
            //Writer.WriteValue(R.Cliente.Documento(0));



            Writer.WriteStartElement("RcpRznSocRecep");
            Writer.WriteString(R.Cliente.NombreSubCuenta(R.IS.Codigo));
            Writer.WriteEndElement();
            Writer.WriteStartElement("RcpDirRecep");
            Writer.WriteString(R.Cliente.DireccionSubCuenta(R.IS.Codigo));
            Writer.WriteEndElement();
            Writer.WriteStartElement("RcpCiudadRecep");
            Writer.WriteString("Salto");
            Writer.WriteEndElement();
            Writer.WriteStartElement("RcpDeptoRecep");
            Writer.WriteString("Salto");
            Writer.WriteEndElement();
            Writer.WriteStartElement("RcpCP");
            Writer.WriteString("");
            Writer.WriteEndElement();
            Writer.WriteStartElement("RcpCorreoRecep");
            Writer.WriteString(R.Cliente.CamposLibres.Email);
            Writer.WriteEndElement();
            Writer.WriteStartElement("RcpInfAdiRecep");
            Writer.WriteString("");
            Writer.WriteEndElement();
            Writer.WriteStartElement("RcpDirPaisRecep");
            Writer.WriteString("");
            Writer.WriteEndElement();
            Writer.WriteStartElement("RcpDstEntregaRecep");
            Writer.WriteEndElement();
            Writer.WriteStartElement("RcpEmlArchivos");
            Writer.WriteValue(1);
            Writer.WriteEndElement();
            // End If
            Writer.WriteEndElement();

            #endregion

            #region Totales
            Writer.WriteStartElement("Totales");

            Writer.WriteStartElement("TotTpoMoneda");
            Writer.WriteString(R.Moneda.CFESubfijo());
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotTpoCambio");
            Writer.WriteValue(R.FactorMoneda);
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotMntNoGrv");
            Writer.WriteValue(0);
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotMntExpoyAsim");
            Writer.WriteValue(0);
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotMntImpuestoPerc");
            Writer.WriteValue(0);
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotMntIVaenSusp");
            Writer.WriteValue(0);
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotMntNetoIvaTasaMin");

            Writer.WriteValue(0);

            Writer.WriteEndElement();
            Writer.WriteStartElement("TotMntNetoIVATasaBasica");
            Writer.WriteValue(Math.Abs(R.TotalBruto()));
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotMntNetoIVAOtra");
            Writer.WriteValue(0);
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotIVATasaMin");
            Writer.WriteValue(10);
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotIVATasaBasica");
            Writer.WriteValue(22);
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotMntIVATasaMin");
            Writer.WriteValue(0);
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotMntIVATasaBasica");
            Writer.WriteValue(Math.Abs((Math.Abs(R.Importe()) - Math.Abs(R.TotalBruto()))));
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotMntIVAOtra");
            Writer.WriteValue(0);
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotMntTotal");
            Writer.WriteValue(Math.Abs(R.Importe()));
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotMntTotRetenido");
            Writer.WriteValue(0);
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotMntCreditoFiscal");
            Writer.WriteValue(0);
            Writer.WriteEndElement();
            Writer.WriteStartElement("RetencPercepTot");
            Writer.WriteEndElement();


            Writer.WriteStartElement("TotMontoNF");
            Writer.WriteValue(0);
            Writer.WriteEndElement();
            Writer.WriteStartElement("TotMntPagar");
            Writer.WriteValue(Math.Abs(R.Importe()));
            Writer.WriteEndElement();
            Writer.WriteEndElement();


            #endregion






            Writer.WriteStartElement("Detalle");
            foreach (LineaRemito L in R.Lineas)
            {
                Writer.WriteStartElement("Item");
                Writer.WriteStartElement("CodItem");
                Writer.WriteEndElement();
                Writer.WriteStartElement("IteIndFact");
                Writer.WriteValue(3);
                Writer.WriteEndElement();
                Writer.WriteStartElement("IteIndAgenteResp");
                Writer.WriteEndElement();
                Writer.WriteStartElement("IteNomItem");
                Writer.WriteString(L.Descripcion);
                Writer.WriteEndElement();
                Writer.WriteStartElement("IteDscItem");
                Writer.WriteEndElement();
                Writer.WriteStartElement("IteCantidad");
                Writer.WriteValue(Math.Abs(L.Unidadestotal));
                Writer.WriteEndElement();
                Writer.WriteStartElement("IteUniMed");
                Writer.WriteString("C/U");
                Writer.WriteEndElement();
                Writer.WriteStartElement("ItePrecioUnitario");
                Writer.WriteValue(Math.Abs(L.Total()));
                Writer.WriteEndElement();
                Writer.WriteStartElement("IteDescuentoPct");
                Writer.WriteValue(0);
                Writer.WriteEndElement();
                Writer.WriteStartElement("IteDescuentoMonto");
                Writer.WriteValue(0);
                Writer.WriteEndElement();
                Writer.WriteStartElement("SubDescuento");
                Writer.WriteEndElement();
                Writer.WriteStartElement("RetencPercep");
                Writer.WriteEndElement();
                Writer.WriteStartElement("IteMontoItem");
                Writer.WriteValue(Math.Abs(L.Total()));
                Writer.WriteEndElement();
                Writer.WriteEndElement();
            }
            Writer.WriteEndElement();

            Writer.WriteStartElement("Referencia");
            int Index = 0;
            if (R.CFE())
            {
                while (Index < 39 && Index < R.Movimiento.Count)
                {
                    MovimientoGeneral M = (MovimientoGeneral)R.Movimiento[Index];
                    Writer.WriteStartElement("ReferenciaItem");
                    Writer.WriteStartElement("RefNroLinRef");
                    Writer.WriteValue(Index + 1);
                    Writer.WriteEndElement();
                    Writer.WriteStartElement("RefIndGlobal");
                    Writer.WriteValue(0);
                    Writer.WriteEndElement();
                    Writer.WriteStartElement("RefTpoDocRef");
                    Writer.WriteValue(M.CFE.Tipo);
                    Writer.WriteEndElement();
                    Writer.WriteStartElement("RefSerie");
                    Writer.WriteString(M.CFE.Serie);
                    Writer.WriteEndElement();
                    Writer.WriteStartElement("RefNroCFERef");
                    Writer.WriteValue(M.CFE.Numero);
                    Writer.WriteEndElement();
                    Writer.WriteStartElement("RefRazonRef");
                    Writer.WriteEndElement();
                    Writer.WriteStartElement("RefFechaCFEref");
                    Writer.WriteString(M.Fecha.ToString("yyyy-MM-dd"));
                    Writer.WriteEndElement();
                    Writer.WriteEndElement();
                    Index += 1;
                }

            }
            else
            {
                Writer.WriteStartElement("ReferenciaItem");
                Writer.WriteStartElement("RefNroLinRef");
                Writer.WriteValue(1);
                Writer.WriteEndElement();
                Writer.WriteStartElement("RefIndGlobal");
                Writer.WriteValue(1);
                Writer.WriteEndElement();
                Writer.WriteStartElement("RefTpoDocRef");
                Writer.WriteValue(R.NumeroCFE());
                Writer.WriteEndElement();
                Writer.WriteStartElement("RefSerie");
                Writer.WriteEndElement();
                Writer.WriteStartElement("RefNroCFERef");
                Writer.WriteEndElement();
                Writer.WriteStartElement("RefRazonRef");
                Writer.WriteString("Documento a anular es un documento anterior al inicio de la facturacion electronica");
                Writer.WriteEndElement();
                Writer.WriteStartElement("RefFechaCFEref");
                Writer.WriteString(R.Fecha.ToString("yyyy-MM-dd"));
                Writer.WriteEndElement();


                Writer.WriteEndElement();
                Index += 1;
            }
            Writer.WriteEndElement();
            Writer.WriteEndElement();
            Writer.WriteEndElement();
            Writer.WriteEndDocument();
            Writer.Close();
            File.Move(xCaja.TemporalCFE.Trim() + xR.Serie + xNumero + ".xml", xCaja.EntradaCFE.Trim() + xR.Serie + xNumero + ".xml");
        }



        private void GuardarCFE(object xCFE, IDbConnection xConexion, IDbTransaction xTran)
        {
            CFE CFE = (CFE)xCFE;
            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@TIPOCFE", CFE.Tipo));
            P.Add(new SqlParameter("@SERIECFE", CFE.Serie));
            P.Add(new SqlParameter("@NUMEROCFE", CFE.Numero));
            P.Add(new SqlParameter("@LINKCFE", CFE.Link));
            P.Add(new SqlParameter("@SERIEALB", CFE.SerieAlbaran));
            P.Add(new SqlParameter("@NUMEROALB", CFE.NumeroAlbaran));
            P.Add(new SqlParameter("@SERIEFAC", CFE.SerieFactura));
            P.Add(new SqlParameter("@NUMEROFAC", CFE.NumeroFactura));
            using (SqlCommand Com = new SqlCommand("INSERT INTO TESORERIACFE(TIPOCFE,SERIECFE,NUMEROCFE,LINKCFE,SERIEALB,NUMEROALB,SERIEFAC,NUMEROFAC) VALUES (@TIPOCFE,@SERIECFE,@NUMEROCFE,@LINKCFE,@SERIEALB,@NUMEROALB,@SERIEFAC,@NUMEROFAC)", (SqlConnection)xConexion))
            {
                Com.Transaction = (SqlTransaction)xTran;
                ExecuteNonQuery(Com, P);
            }
        }

        private CFE LeerCFERetorno(string xRuta, Remito xR, CajaGeneral xCaja)
        {
            CFE C = null;
            string Salida = xCaja.SalidaCFE.Trim();
            XmlReader document = new XmlTextReader(Salida + xRuta + ".xml");

            while (document.Read())
            {
                XmlNodeType type = document.NodeType;
                if (type == XmlNodeType.CDATA)
                {
                    XmlDocument a = new XmlDocument();
                    a.LoadXml(document.ReadContentAsString());
                    int tipo = Convert.ToInt32(a.GetElementsByTagName("CFETipo").Item(0).InnerXml);
                    string serie = a.GetElementsByTagName("CFESerie").Item(0).InnerXml;
                    int numero = Convert.ToInt32(a.GetElementsByTagName("CFENro").Item(0).InnerXml);
                    string link = a.GetElementsByTagName("CFERepImpressa").Item(0).InnerXml;
                    C = new CFE(tipo, serie, numero, link, xR.Serie, xR.Numero, xR.Serie, xR.Numero);
                }
            }
            document.Close();
            try
            {
                File.Move(Salida + xRuta + ".xml", xCaja.BackCFE.Trim() + xRuta + ".xml");
            }
            catch (Exception)
            { }
            return C;
        }

        private int NumeroRecibo(string xSerie, IDbConnection xCon, IDbTransaction xTran, int xTipoDoc)
        {
            int Numero = -1;

            using (SqlCommand Com = new SqlCommand("UPDATE SERIESDOC SET CONTADORB = CONTADORB+1 WHERE (SERIE = @SERIERECIBO) AND TIPODOC = @TIPO", (SqlConnection)xCon))
            {
                Com.Transaction = (SqlTransaction)xTran;
                Com.Parameters.Add(new SqlParameter("@SERIERECIBO", xSerie));
                Com.Parameters.Add(new SqlParameter("@TIPO", xTipoDoc));
                ExecuteNonQuery(Com);
                Com.CommandText = "SELECT  CONTADORB+1 FROM SERIESDOC WHERE (SERIE = @SERIERECIBO) AND TIPODOC = @TIPO";
                Numero = (int)ExecuteScalar(Com);
            }
            return Numero;

        }

        //public int GenerarRemitos(object xRe, object xUsuario)
        //{
        //    try
        //    {

        //        if (_Connection.State == System.Data.ConnectionState.Closed)
        //            _Connection.Open();
        //        _Transaccion = (SqlTransaction)Connection.BeginTransaction();
        //        Remito R = (Remito)xRe;
        //        int Numero = getNumeroBySerie(R.Serie);
        //        GuardarCabecera(R, _Connection, Numero);
        //        GuardarVentaLin(R, _Connection, Numero);
        //        GuardarFVentas(R, _Connection, Numero);
        //        GuardatVentasTotales(R, _Connection, Numero);
        //        GuardarTesoreria(R, _Connection, Numero);
        //        UpNumeroBySerie(R.Serie, Numero);
        //        if (R.Comentario.Length > 0)
        //            GuardarComentario(R, Numero, ((Usuario)xUsuario).CodUsuario, _Connection);
        //        _Transaccion.Commit();
        //        return Numero;
        //    }
        //    catch (Exception Ex)
        //    {
        //        _Transaccion.Rollback();
        //        throw Ex;

        //    }
        //    finally
        //    {
        //        CerrarConexion(_Connection);
        //    }
        //}


        //public void DeleteDoc(string xSerie, int xNumero, string xN)
        //{

        //    List<string> QueryDelete = new List<string>();

        //    int ALBARAN = -1;
        //    ALBARAN = getAlbaran(xSerie, xNumero, xN);
        //    List<IDataParameter> P = new List<IDataParameter>();
        //    P.Add(new SqlParameter("@ALBARAN", ALBARAN));
        //    P.Add(new SqlParameter("@SERIE", xSerie));
        //    P.Add(new SqlParameter("@NUMERO", xNumero));
        //    P.Add(new SqlParameter("@N", xN));
        //    QueryDelete.Add("DELETE TOP 1 FROM ALBVENTACAB WHERE NUMSERIE = @SERIE AND NUMALBARAN = @ALBARAN AND N = @N ");
        //    QueryDelete.Add("DELETE TOP 1 FROM ALBVENTALIN WHERE NUMSERIE = @SERIE AND NUMALBARAN = @ALBARAN AND N = @N ");
        //    QueryDelete.Add("DELETE TOP 1 FROM FACTURASVENTA WHERE NUMSERIE = @SERIE AND NUMFACTURA = @NUMERO AND N = @N ");
        //    QueryDelete.Add("DELETE TOP 1 FROM ALBVENTATOT WHERE SERIE = @SERIE AND NUMERO = @ALBARAN AND N = @N ");
        //    QueryDelete.Add("DELETE TOP 1 FROM FACTURASVENTATOT WHERE SERIE = @SERIE AND NUMERO = @NUMERO AND N = @N ");
        //    QueryDelete.Add("DELETE  FROM TESORERIA WHERE ORIGEN = 'C' AND SERIE = @SERIE AND NUMERO = @NUMERO AND N = @N and estado");
        //    QueryDelete.Add("DELETE TOP 1 FROM TESORERIACFE WHERE  AND SERIEFAC = @SERIE AND NUMEROFAC = @NUMERO AND N = @N ");
        //    try
        //    {

        //        if (_Connection.State == System.Data.ConnectionState.Closed)
        //            _Connection.Open();
        //        _Transaccion = (SqlTransaction)Connection.BeginTransaction();
        //        DbCommand Command = new SqlCommand();
        //        Command.Connection = (SqlConnection)Connection;
        //        Command.Transaction = _Transaccion;
        //        ExecuteNonQuery(Command, P, QueryDelete);
        //        _Transaccion.Commit();
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //    finally
        //    {
        //        CerrarConexion(_Connection);
        //    }
        //}


        private void GuardarComentario(Remito xRemito, int xNumero, int xUsuario, IDbConnection xConexion, IDbTransaction xTran)
        {
            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@NUMERO", xNumero));
            P.Add(new SqlParameter("@SERIE", xRemito.Serie));
            P.Add(new SqlParameter("@COMENTARIO", xRemito.Comentario));
            P.Add(new SqlParameter("@USUARIO", xUsuario));
            using (SqlCommand Com = new SqlCommand("INSERT INTO COMENTARIODOCUMENTOS(SERIE,NUMERO,COMENTARIO,USUARIO) VALUES (@SERIE,@NUMERO,@COMENTARIO,@USUARIO)", (SqlConnection)xConexion))
            {
                Com.Transaction = (SqlTransaction)xTran;
                ExecuteNonQuery(Com, P);
            }
        }






        private void GuardatVentasTotales(Remito R, IDbConnection xConexion, IDbTransaction xTran, int xNumero)
        {
            decimal ImporteBruto = R.TotalBruto();
            decimal Impuestos = R.TotalImpuestos();
            decimal Costo = R.Costo();
            decimal Importe = R.Importe();
            decimal CostoIva = R.TotalCostoIva();
            if (R is EntregaCuenta)
            {
                ImporteBruto = 0;
                Impuestos = 0;
                Costo = 0;
                Importe = 0;
                CostoIva = 0;
            }

            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@SERIE", R.Serie));
            P.Add(new SqlParameter("@NUMERO", xNumero));
            P.Add(new SqlParameter("@N", "B"));
            P.Add(new SqlParameter("@NUMLINEA", 1));
            P.Add(new SqlParameter("@BRUTO", ImporteBruto));
            P.Add(new SqlParameter("@DTOCOMERC", Convert.ToInt32(0)));
            P.Add(new SqlParameter("@TOTDTOCOMERC", Convert.ToInt32(0)));
            P.Add(new SqlParameter("@DTOPP", Convert.ToInt32(0)));
            P.Add(new SqlParameter("@TOTDTOPP", Convert.ToInt32(0)));
            P.Add(new SqlParameter("@BASEIMPONIBLE", ImporteBruto));
            P.Add(new SqlParameter("@IVA", 22));
            P.Add(new SqlParameter("@TOTIVA", Importe - ImporteBruto));
            P.Add(new SqlParameter("@REQ", Convert.ToInt32(0)));
            P.Add(new SqlParameter("@TOTREQ", Convert.ToInt32(0)));
            P.Add(new SqlParameter("@TOTAL", R.Importe()));
            P.Add(new SqlParameter("@ESGASTO", 'F'));
            P.Add(new SqlParameter("@CODDTO", -1));
            P.Add(new SqlParameter("@DESCRIPCION", string.Empty));
            using (SqlCommand Com = new SqlCommand("INSERT INTO ALBVENTATOT(SERIE,NUMERO,N,NUMLINEA,BRUTO,DTOCOMERC,TOTDTOCOMERC,DTOPP,TOTDTOPP,BASEIMPONIBLE,IVA,TOTIVA,REQ,TOTREQ,TOTAL,ESGASTO,CODDTO,DESCRIPCION) VALUES (@SERIE,@NUMERO,@N,@NUMLINEA,@BRUTO,@DTOCOMERC,@TOTDTOCOMERC,@DTOPP,@TOTDTOPP,@BASEIMPONIBLE,@IVA,@TOTIVA,@REQ,@TOTREQ,@TOTAL,@ESGASTO,@CODDTO,@DESCRIPCION)", (SqlConnection)xConexion))
            {
                Com.Transaction = (SqlTransaction)xTran;
                ExecuteNonQuery(Com, P);
                Com.CommandText = "INSERT INTO FACTURASVENTATOT(SERIE,NUMERO,N,NUMLINEA,BRUTO,DTOCOMERC,TOTDTOCOMERC,DTOPP,TOTDTOPP,BASEIMPONIBLE,IVA,TOTIVA,REQ,TOTREQ,TOTAL,ESGASTO,CODDTO,DESCRIPCION) VALUES (@SERIE,@NUMERO,@N,@NUMLINEA,@BRUTO,@DTOCOMERC,@TOTDTOCOMERC,@DTOPP,@TOTDTOPP,@BASEIMPONIBLE,@IVA,@TOTIVA,@REQ,@TOTREQ,@TOTAL,@ESGASTO,@CODDTO,@DESCRIPCION)";
                ExecuteNonQuery(Com);
            }
        }

        private void GuardarFVentas(Remito R, IDbConnection xConexion, IDbTransaction xTran, int xNumero)
        {
            decimal ImporteBruto = R.TotalBruto();
            decimal Impuestos = R.TotalImpuestos();
            decimal Costo = R.Costo();
            decimal Importe = R.Importe();
            if (R is EntregaCuenta)
            {
                ImporteBruto = 0;
                Impuestos = 0;
                Costo = 0;
                Importe = 0;

            }
            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@NUMSERIE", R.Serie));
            P.Add(new SqlParameter("@NUMFACTURA", xNumero));
            P.Add(new SqlParameter("@N", "B"));
            P.Add(new SqlParameter("@CODCLIENTE", R.Cliente.IdCliente));
            P.Add(new SqlParameter("@FECHA", R.Fecha));
            P.Add(new SqlParameter("@TOTALBRUTO", ImporteBruto));
            P.Add(new SqlParameter("@TOTALIMPUESTOS", Impuestos));
            P.Add(new SqlParameter("@TOTALNETO", Importe));
            P.Add(new SqlParameter("@TOTALCOSTE", Costo));
            P.Add(new SqlParameter("@CODMONEDA", R.Moneda.Codmoneda));
            P.Add(new SqlParameter("@FACTORMONEDA", R.FactorMoneda));
            P.Add(new SqlParameter("@IVAINCLUIDO", 'T'));
            P.Add(new SqlParameter("@CODVENDEDOR", R.CodVendedor));
            P.Add(new SqlParameter("@TIPODOC", R.TipoDoc()));
            P.Add(new SqlParameter("@Z", R.NumeroZ()));
            P.Add(new SqlParameter("@CAJA", R.SerieCaja()));
            P.Add(new SqlParameter("@TOTALCOSTEIVA", R.TotalCostoIva()));
            using (SqlCommand Com = new SqlCommand("INSERT INTO FACTURASVENTA (NUMSERIE,NUMFACTURA,N,CODCLIENTE,FECHA,TOTALBRUTO,TOTALIMPUESTOS,TOTALNETO,TOTALCOSTE,CODMONEDA,FACTORMONEDA,IVAINCLUIDO,CODVENDEDOR,TIPODOC,Z,CAJA,TOTALCOSTEIVA) VALUES (@NUMSERIE,@NUMFACTURA,@N,@CODCLIENTE,@FECHA,@TOTALBRUTO,@TOTALIMPUESTOS,@TOTALNETO,@TOTALCOSTE,@CODMONEDA,@FACTORMONEDA,@IVAINCLUIDO,@CODVENDEDOR,@TIPODOC,@Z,@CAJA,@TOTALCOSTEIVA)", (SqlConnection)xConexion))
            {
                Com.Transaction = (SqlTransaction)xTran;
                ExecuteNonQuery(Com, P);
            }

        }

        private void GuardarVentaLin(Remito R, IDbConnection xCon, IDbTransaction xTran, int xNumero)
        {

            foreach (LineaRemito L in R.Lineas)
            {
                List<IDataParameter> Lin = new List<IDataParameter>();
                Lin.Add(new SqlParameter("@NUMSERIE", L.Serie));
                Lin.Add(new SqlParameter("@NUMALBARAN", xNumero));
                Lin.Add(new SqlParameter("@N", L.N));
                Lin.Add(new SqlParameter("@NUMLIN", L.NumLin));
                Lin.Add(new SqlParameter("@CODARTICULO", L.CodArticulo));
                Lin.Add(new SqlParameter("@REFERENCIA", L.Referencia));
                Lin.Add(new SqlParameter("@DESCRIPCION", L.Descripcion));
                Lin.Add(new SqlParameter("@COLOR", L.Color));
                Lin.Add(new SqlParameter("@TALLA", L.Talla));
                Lin.Add(new SqlParameter("@UNID1", L.Unid1));
                Lin.Add(new SqlParameter("@UNID2", L.Unid2));
                Lin.Add(new SqlParameter("@UNID3", L.Unid3));
                Lin.Add(new SqlParameter("@UNID4", L.Unid4));
                Lin.Add(new SqlParameter("@UNIDADESTOTAL", L.Unidadestotal));
                Lin.Add(new SqlParameter("@PRECIO", L.Precio));
                Lin.Add(new SqlParameter("@DTO", L.Dto));
                Lin.Add(new SqlParameter("@TOTAL", L.Total()));
                Lin.Add(new SqlParameter("@COSTE", L.Costo));
                Lin.Add(new SqlParameter("@PRECIODEFECTO", L.PrecioDefecto()));
                Lin.Add(new SqlParameter("@TIPOIMPUESTO", L.Tipoimpuesto));
                Lin.Add(new SqlParameter("@IVA", L.Iva));
                Lin.Add(new SqlParameter("@CODTARIFA", L.Codtarifa));
                Lin.Add(new SqlParameter("@CODALMACEN", L.CodAlmacen));
                Lin.Add(new SqlParameter("@CODVENDEDOR", R.CodVendedor));
                Lin.Add(new SqlParameter("@PRECIOIVA", L.Precioiva));
                Lin.Add(new SqlParameter("@UDSEXPANSION", L.Udsexpansion));
                Lin.Add(new SqlParameter("@EXPANDIDA", L.Expandida));
                Lin.Add(new SqlParameter("@TOTALEXPANSION", L.Totalexpansion));
                Lin.Add(new SqlParameter("@COSTEIVA", L.Costeiva));
                Lin.Add(new SqlParameter("@FECHAENTREGA", R.Fecha));
                Lin.Add(new SqlParameter("@NUMKGEXPANSION", L.Numkgentrega));
                using (SqlCommand Com = new SqlCommand("INSERT INTO ALBVENTALIN (NUMSERIE,NUMALBARAN,N,NUMLIN,CODARTICULO,REFERENCIA,DESCRIPCION,COLOR,TALLA,UNID1,UNID2,UNID3,UNID4,UNIDADESTOTAL,PRECIO,DTO,TOTAL,COSTE,PRECIODEFECTO,TIPOIMPUESTO,IVA,CODTARIFA,CODALMACEN,CODVENDEDOR,PRECIOIVA,UDSEXPANSION,EXPANDIDA,TOTALEXPANSION,COSTEIVA,FECHAENTREGA,NUMKGEXPANSION) VALUES (@NUMSERIE,@NUMALBARAN,@N,@NUMLIN,@CODARTICULO,@REFERENCIA,@DESCRIPCION,@COLOR,@TALLA,@UNID1,@UNID2,@UNID3,@UNID4,@UNIDADESTOTAL,@PRECIO,@DTO,@TOTAL,@COSTE,@PRECIODEFECTO,@TIPOIMPUESTO,@IVA,@CODTARIFA,@CODALMACEN,@CODVENDEDOR,@PRECIOIVA,@UDSEXPANSION,@EXPANDIDA,@TOTALEXPANSION,@COSTEIVA,@FECHAENTREGA,@NUMKGEXPANSION)", (SqlConnection)xCon))
                {
                    Com.Transaction = (SqlTransaction)xTran;
                    ExecuteNonQuery(Com, Lin);
                }
            }
        }

        private void GuardarTesoreria(Remito R, IDbConnection xConexion, IDbTransaction xTran, int xNumero)
        {
            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@SUDOCUMENTO", R.Sudocumento()));
            P.Add(new SqlParameter("@CAJASALDADO", R.SerieCaja()));
            P.Add(new SqlParameter("@ZSALDADO", R.NumeroZ()));
            P.Add(new SqlParameter("@NUMEROREMESA", R.Remesa()));
            P.Add(new SqlParameter("@GENAPUNTE", R.GenApunte()));
            P.Add(new SqlParameter("@ESTADO", R.Estado()));
            P.Add(new SqlParameter("@ORIGEN", "C"));
            P.Add(new SqlParameter("@TIPODOCUMENTO", "F"));
            P.Add(new SqlParameter("@SERIE", R.Serie));
            P.Add(new SqlParameter("@NUMERO", xNumero));
            P.Add(new SqlParameter("@N", 'B'));
            P.Add(new SqlParameter("@POSICION", 1));
            P.Add(new SqlParameter("@FECHADOCUMENTO", DateTime.Today));
            P.Add(new SqlParameter("@FECHAVENCIMIENTO", DateTime.Today));
            P.Add(new SqlParameter("@CODIGOINTERNO", R.Cliente.IdCliente));
            P.Add(new SqlParameter("@IMPORTE", R.Importe()));
            P.Add(new SqlParameter("@CODFORMAPAGO", R.FormaPago()));
            P.Add(new SqlParameter("@CODTIPOPAGO", R.TipoPago()));
            P.Add(new SqlParameter("@FECHASALDADO", R.Fecha));
            P.Add(new SqlParameter("@FACTORMONEDA", R.FactorMoneda));
            P.Add(new SqlParameter("@CODMONEDA", R.Moneda.Codmoneda));
            P.Add(new SqlParameter("@SUBCTA", R.IS.Codigo));
            using (SqlCommand Com = new SqlCommand("INSERT INTO TESORERIA (ORIGEN,TIPODOCUMENTO,SERIE,NUMERO,N,POSICION,FECHADOCUMENTO,FECHAVENCIMIENTO,CODIGOINTERNO,IMPORTE,CODFORMAPAGO,CODTIPOPAGO,ESTADO,FECHASALDADO,FACTORMONEDA,CODMONEDA,ZSALDADO,CAJASALDADO,NUMEROREMESA,GENAPUNTE,SUDOCUMENTO,SUBCTA) VALUES (@ORIGEN,@TIPODOCUMENTO,@SERIE,@NUMERO,@N,@POSICION,@FECHADOCUMENTO,@FECHAVENCIMIENTO,@CODIGOINTERNO,@IMPORTE,@CODFORMAPAGO,@CODTIPOPAGO,@ESTADO,@FECHASALDADO,@FACTORMONEDA,@CODMONEDA,@ZSALDADO,@CAJASALDADO,@NUMEROREMESA,@GENAPUNTE,@SUDOCUMENTO,@SUBCTA)", (SqlConnection)xConexion))
            {
                Com.Transaction = (SqlTransaction)xTran;
                ExecuteNonQuery(Com, P);
            }

        }




        private int getAlbaran(string xserie, int xnumero, string xn)
        {
            int Numero = -1;
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("DBO.JL_ALBARANBYSERIENUMERO(@NUMERO,@SERIE,@N)", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@SERIE", xserie));
                    Com.Parameters.Add(new SqlParameter("@NUMERO", xnumero));
                    Com.Parameters.Add(new SqlParameter("@N", xn));
                    Numero = (int)ExecuteScalar(Com);
                }
            }
            return Numero;
        }



        private Permiso getPermisoFromReader(IDataReader rd)
        {
            Permiso P = null;
            try
            {
                P = new Permiso((int)rd["IDPERMISO"]);
            }
            catch (Exception e)
            {
                throw e;
            }
            return P;
        }

        private void GuardarCabecera(Remito R, IDbConnection xCon, IDbTransaction xTran, int xNumero)
        {
            List<IDataParameter> P = new List<IDataParameter>();
            decimal ImporteBruto = R.TotalBruto();
            decimal Impuestos = R.TotalImpuestos();
            decimal Costo = R.Costo();
            decimal Importe = R.Importe();
            decimal CostoIva = R.TotalCostoIva();
            if (R is EntregaCuenta)
            {
                ImporteBruto = 0;
                Impuestos = 0;
                Costo = 0;
                Importe = 0;
                CostoIva = 0;
            }
            DateTime F = DateTime.Today;
            P.Add(new SqlParameter("@NUMSERIE", R.Serie));
            P.Add(new SqlParameter("@NUMALBARAN", xNumero));
            P.Add(new SqlParameter("@N", 'B'));
            P.Add(new SqlParameter("@FACTURADO", 'T'));
            P.Add(new SqlParameter("@NUMSERIEFAC", R.Serie));
            P.Add(new SqlParameter("@NUMFAC", xNumero));
            P.Add(new SqlParameter("@CODCLIENTE", R.Cliente.IdCliente));
            P.Add(new SqlParameter("@CODVENDEDOR", R.CodVendedor));
            P.Add(new SqlParameter("@FECHA", F));
            P.Add(new SqlParameter("@TOTALBRUTO", ImporteBruto));
            P.Add(new SqlParameter("@TOTALIMPUESTOS", Impuestos));
            P.Add(new SqlParameter("@TOTALNETO", Importe));
            P.Add(new SqlParameter("@TOTALCOSTE", Costo));
            P.Add(new SqlParameter("@CODMONEDA", R.Moneda.Codmoneda));
            P.Add(new SqlParameter("@IVAINCLUIDO", 'T'));
            P.Add(new SqlParameter("@CODTARIFA", R.Tarifa));
            P.Add(new SqlParameter("@TIPODOC", R.TipoDoc()));
            P.Add(new SqlParameter("@TIPODOCFAC", R.TipoDoc()));
            P.Add(new SqlParameter("@FACTORMONEDA", R.FactorMoneda));
            P.Add(new SqlParameter("@CAJA", R.SerieCaja()));
            P.Add(new SqlParameter("@Z", R.NumeroZ()));
            P.Add(new SqlParameter("@TOTALCOSTEIVA", CostoIva));
            P.Add(new SqlParameter("@SALA", -1));
            P.Add(new SqlParameter("@MESA", -1));
            P.Add(new SqlParameter("@IDESTADO", -1));
            using (SqlCommand Com = new SqlCommand("INSERT INTO ALBVENTACAB(NUMSERIE,NUMALBARAN,N,FACTURADO,NUMSERIEFAC,NUMFAC,CODCLIENTE,CODVENDEDOR,FECHA,TOTALBRUTO,TOTALIMPUESTOS,TOTALNETO,TOTALCOSTE,CODMONEDA,IVAINCLUIDO,CODTARIFA,TIPODOC,TIPODOCFAC,Z,CAJA,TOTALCOSTEIVA,FACTORMONEDA,SALA,MESA,IDESTADO) VALUES (@NUMSERIE,@NUMALBARAN,@N,@FACTURADO,@NUMSERIEFAC,@NUMFAC,@CODCLIENTE,@CODVENDEDOR,@FECHA,@TOTALBRUTO,@TOTALIMPUESTOS,@TOTALNETO,@TOTALCOSTE,@CODMONEDA,@IVAINCLUIDO,@CODTARIFA,@TIPODOC,@TIPODOCFAC,@Z,@CAJA,@TOTALCOSTEIVA,@FACTORMONEDA,@SALA,@MESA,@IDESTADO)", (SqlConnection)xCon))
            {
                Com.Transaction = (SqlTransaction)xTran;
                ExecuteNonQuery(Com, P);
            }
        }

        private List<Serie> getSeries(string _Caja, List<int> xTipoDoc)
        {
            List<Serie> Series = new List<Serie>();
            string cadena = "";
            List<IDataParameter> P = new List<IDataParameter>();
            StringBuilder SB = new StringBuilder("SELECT SERIE as SERIE,IDTIPODOC as NUMERO  FROM SERIESCAJA WHERE IDCAJA = @CAJA  AND IDTIPODOC IN (");
            foreach (int Numero in xTipoDoc)
            {
                cadena = cadena + (",@ID" + Numero.ToString());
                P.Add(new SqlParameter("@ID" + Numero.ToString(), Numero));
            }

            SB.Append(cadena.Remove(0, 1));
            SB.Append(")");


            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand(SB.ToString(), (SqlConnection)Con))
                {
                    P.Add(new SqlParameter("@CAJA", _Caja));
                    using (IDataReader Reader = ExecuteReader(Com, P))
                    {
                        while (Reader.Read())
                            Series.Add(getSerieFromReader(Reader));
                    }
                }
            }


            return Series;
        }

        private Serie getSerieFromReader(IDataReader rd)
        {
            Serie S = null;
            try
            {
                S = new Serie((string)rd["SERIE"], (int)rd["NUMERO"]);
            }
            catch (Exception e)
            {
                throw e;
            }
            return S;
        }



        private int getNumeroZ(string xCaja)
        {
            int Numero = -1;
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT CONVERT(INT,MAX(NUMERO+1)) AS NUMERO FROM ARQUEOS WHERE (FO = 1) AND (Arqueo = 'Z') AND (CAJA = @CAJA)", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@CAJA", xCaja));

                    Numero = (int)ExecuteScalar(Com);
                }
            }
            return Numero;
        }

        public Moneda getMonedaByID(int xMoneda)
        {
            return General.getInstance().getMonedaByID(xMoneda);
        }

        private bool NuevoMovimiento(MovimientoGeneral M, IDbConnection xConexion, IDbTransaction xTran)
        {
            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@ORIGEN", M.Origen));
            P.Add(new SqlParameter("@TIPODOCUMENTO", M.Tipodocumento));
            P.Add(new SqlParameter("@SERIE", M.Serie));
            P.Add(new SqlParameter("@NUMERO", M.Numero));
            P.Add(new SqlParameter("@N", "B"));
            P.Add(new SqlParameter("@POSICION", M.Posicion));
            P.Add(new SqlParameter("@FECHADOCUMENTO", M.Fecha));
            P.Add(new SqlParameter("@FECHAVENCIMIENTO", M.FechaVencimiento));
            P.Add(new SqlParameter("@CODIGOINTERNO", M.Codcliente));
            P.Add(new SqlParameter("@IMPORTE", M.Importe));
            P.Add(new SqlParameter("@CODFORMAPAGO", M.FormaPago));
            P.Add(new SqlParameter("@CODTIPOPAGO", M.TipoPago));
            P.Add(new SqlParameter("@ESTADO", M.Estado));
            P.Add(new SqlParameter("@FACTORMONEDA", M.Factormoneda));
            P.Add(new SqlParameter("@CODMONEDA", M.Moneda.Codmoneda));
            P.Add(new SqlParameter("@ZSALDADO", M.Zsaldado));
            P.Add(new SqlParameter("@GENAPUNTE", M.GenApunte));
            P.Add(new SqlParameter("@FECHASALDADO", M.Fecha));
            P.Add(new SqlParameter("@SUBCTA", M.SubCta));
            string Query = "INSERT INTO TESORERIA(ORIGEN,TIPODOCUMENTO,SERIE,NUMERO,N,POSICION,FECHADOCUMENTO,FECHAVENCIMIENTO,CODIGOINTERNO,IMPORTE,CODFORMAPAGO,CODTIPOPAGO,ESTADO,FACTORMONEDA,CODMONEDA,ZSALDADO,GENAPUNTE,SUBCTA,FECHASALDADO) VALUES (@ORIGEN,@TIPODOCUMENTO,@SERIE,@NUMERO,@N,@POSICION,@FECHADOCUMENTO,@FECHAVENCIMIENTO,@CODIGOINTERNO,@IMPORTE,@CODFORMAPAGO,@CODTIPOPAGO,@ESTADO,@FACTORMONEDA,@CODMONEDA,@ZSALDADO,@GENAPUNTE,@SUBCTA,@FECHASALDADO)";
            using (SqlCommand Com = new SqlCommand(Query, (SqlConnection)xConexion))
            {
                Com.Transaction = (SqlTransaction)xTran;
                ExecuteNonQuery(Com, P);
            }
            return true;

        }

        private bool GenerarPago(MovimientoGeneral M, int xNumeroRecibo, IDbConnection xConexion, IDbTransaction xTran)
        {
            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@ORIGEN", M.Origen));
            P.Add(new SqlParameter("@TIPODOCUMENTO", M.Tipodocumento));
            P.Add(new SqlParameter("@SERIE", M.Serie));
            P.Add(new SqlParameter("@NUMERO", M.Numero));
            P.Add(new SqlParameter("@N", "B"));
            P.Add(new SqlParameter("@POSICION", M.Posicion));
            P.Add(new SqlParameter("@CODFORMAPAGO", M.FormaPago));
            P.Add(new SqlParameter("@TIPOPAGO", M.TipoPago));
            P.Add(new SqlParameter("@ESTADO", M.Estado));
            P.Add(new SqlParameter("@FECHA", M.Fecha));
            P.Add(new SqlParameter("@GEN", M.GenApunte));
            P.Add(new SqlParameter("@FACTORMONEDA", M.Factormoneda));
            P.Add(new SqlParameter("@Z", M.Zsaldado));
            P.Add(new SqlParameter("@IMPORTE", M.Importe));
            P.Add(new SqlParameter("@CAJA", M.Cajasaldado));
            P.Add(new SqlParameter("@REMESA", xNumeroRecibo));
            P.Add(new SqlParameter("@SUDO", M.Sudocumento));
            P.Add(new SqlParameter("@MORA", M.Mora));
            P.Add(new SqlParameter("@FECHASALDADO", DateTime.Now));

            string Query = "UPDATE TESORERIA SET CODFORMAPAGO = @CODFORMAPAGO, CODTIPOPAGO = @TIPOPAGO, ESTADO = @ESTADO, FECHASALDADO = @FECHASALDADO, GENAPUNTE = @GEN, FACTORMONEDA = @FACTORMONEDA, ZSALDADO = @Z, IMPORTE = @IMPORTE, CAJASALDADO = @CAJA, NUMEROREMESA = @REMESA,SUDOCUMENTO = @SUDO, MORA = @MORA WHERE ORIGEN = @ORIGEN AND N = @N AND TIPODOCUMENTO = @TIPODOCUMENTO AND SERIE = @SERIE AND NUMERO = @NUMERO AND POSICION = @POSICION";
            using (SqlCommand Com = new SqlCommand(Query, (SqlConnection)xConexion))
            {
                Com.Transaction = (SqlTransaction)xTran;
                ExecuteNonQuery(Com, P);
            }
            return true;
        }

        public int getMonedaByRecibo(string xSerie, int xID)
        {
            int Numero = -1;
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT MAX(CODMONEDA) NUMERO FROM TESORERIA WHERE NUMEROREMESA = @ID and SUDOCUMENTO = @SERIE", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@ID", xID));
                    Com.Parameters.Add(new SqlParameter("@SERIE", xSerie));
                    Numero = (int)ExecuteScalar(Com);
                }
            }
            return Numero;
        }





        //public int ActualizarMovimientos(List<object> xMovimientos, string xSerieRecibos)
        //{
        //    int zNumero = -1; //Buffer Numero recibo a generar
        //    zNumero = NumeroRecibo(xSerieRecibos);

        //    try
        //    {
        //        if (_Connection.State == System.Data.ConnectionState.Closed)
        //            _Connection.Open();
        //        _Command = new SqlCommand();
        //        _Transaccion = (SqlTransaction)Connection.BeginTransaction();
        //        foreach (MovimientoGeneral M in xMovimientos)
        //        {
        //            if (M.Estado == "S")
        //                GenerarPago(M, zNumero, Connection);
        //            else
        //            {
        //                M.Posicion = Convert.ToByte(getPosicion(M.Numero, M.Serie, _Transaccion) + 1);
        //                NuevoMovimiento(M, Connection);
        //            }

        //        }

        //        _Transaccion.Commit();

        //    }
        //    catch (Exception E)
        //    {
        //        _Transaccion.Rollback();
        //        return -1;
        //    }
        //    finally
        //    {
        //        _Connection.Close();
        //    }

        //    return zNumero;

        //}


        private int ActualizarMovimientos(List<object> xMovimientos, string xSerieRecibos, IDbConnection xCon, IDbTransaction xTran)
        {
            int Numero = -1;
                Numero = NumeroRecibo(xSerieRecibos, xCon, xTran, 20);
                foreach (MovimientoGeneral M in xMovimientos)
                {
                    if (M.Estado == "S")
                        GenerarPago(M, Numero, xCon, xTran);
                    else
                    {
                        M.Posicion = Convert.ToByte(getPosicion(M.Numero, M.Serie, xCon, xTran) + 1);
                        NuevoMovimiento(M, xCon, xTran);
                    }
                } 
            return Numero;
        }






        public Empresa getClavesEmpresa(int xSucursal)
        {
            Empresa E = null;

            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT * FROM CLAVESEMPRESA WHERE (SUCURSAL = @SUCURSAL)", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@SUCURSAL", xSucursal));
                    using (IDataReader Reader = ExecuteReader(Com))
                    {
                        if (Reader.Read())
                        {
                            E = (Empresa)getEmpresaFromReader(Reader);
                        }
                    }
                }

            }

            return E;
        }

        private Empresa getEmpresaFromReader(IDataReader rd)
        {
            try
            {
                int xSucursal = (int)(rd["Sucursal"]);
                string xClave = (string)(rd["Clave"]);
                int xCodEmpresa = (int)(rd["CodEmpresa"]);
                Empresa E = new Empresa(xSucursal, xClave, xCodEmpresa);
                return E;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CambiarClaveUsuario(int xcodUsuario, string xpassword)
        {
            throw new NotImplementedException();
        }


        public object getCajaByID(string xNombreMaquina, object xUser)
        {

            List<int> Configs = new List<int>();
            Configs.Add(10);
            Configs.Add(43);
            Configs.Add(44);
            Configs.Add(45);
            Configs.Add(54);
            Configs.Add(53);
            Configs.Add(46);
            Configs.Add(56);
            Configs.Add(55);
            Configs.Add(57);
            Configs.Add(66);


            List<Config> ltsConfigs = getConfig(xNombreMaquina, Configs);
            string Serie = ltsConfigs.Find(Obj => Obj.ID == 10).Dato;



            decimal xFM = (decimal)getCotizacion();


            int Z = getNumeroZ(Serie);

            List<int> Lista = new List<int>();
            Lista.Add(19);
            Lista.Add(20);
            Lista.Add(21);
            Lista.Add(22);
            Lista.Add(23);
            Lista.Add(62);



            List<Serie> Series = getSeries(Serie, Lista);
            CajaGeneral C = new CajaGeneral(ltsConfigs, Z, xFM, Series);



            if (xUser != null)
                C.Usuario = (Usuario)xUser;

            return C;
        }

        private List<Config> getConfig(string xNombreMaquina, List<int> xIDs)
        {

            string cadena = "";
            List<IDataParameter> P = new List<IDataParameter>();
            StringBuilder SB = new StringBuilder("SELECT  VALOR AS SERIE,ID AS NUMERO  FROM PARTERMINAL WHERE IDTERMINAL = @EQUIPO  AND ID IN (");
            foreach (int Numero in xIDs)
            {
                cadena = cadena + (",@ID" + Numero.ToString());
                P.Add(new SqlParameter("@ID" + Numero.ToString(), Numero));
            }

            SB.Append(cadena.Remove(0, 1));
            SB.Append(")");

            List<Config> Configs = new List<Config>();
            using (SqlConnection Con = new SqlConnection(GeneralConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand(SB.ToString(), (SqlConnection)Con))
                {
                    P.Add(new SqlParameter("@EQUIPO", xNombreMaquina));
                    using (IDataReader Reader = ExecuteReader(Com, P))
                    {
                        while (Reader.Read())
                            Configs.Add(getConfigFromReader(Reader));
                    }
                }
            }
            return Configs;
        }

        private Config getConfigFromReader(IDataReader rd)
        {
            Config S = null;
            try
            {
                S = new Config((string)rd["SERIE"], (int)rd["NUMERO"]);
            }
            catch (Exception e)
            {
                throw e;
            }
            return S;
        }

        public object Parametros(string xNombreMaquina, List<int> Indexs)
        {
            string cadena = "";
            DataTable DT;
            List<IDataParameter> P = new List<IDataParameter>();
            StringBuilder SB = new StringBuilder("SELECT  PF.ID,PF.CLAVE,PT.VALOR  FROM PARTERMINAL AS PT INNER JOIN PARAMETROSFRONT PF ON PF.ID = PT.ID WHERE PT.IDTERMINAL = @EQUIPO  AND PT.ID IN (");
            foreach (int Numero in Indexs)
            {
                cadena = cadena + (",@ID" + Numero.ToString());
                P.Add(new SqlParameter("@ID" + Numero.ToString(), Numero));
            }

            SB.Append(cadena.Remove(0, 1));
            SB.Append(") ORDER BY PT.ID ASC");

            using (SqlConnection Con = new SqlConnection(GeneralConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand(SB.ToString(), (SqlConnection)Con))
                {
                    P.Add(new SqlParameter("@EQUIPO", xNombreMaquina));
                    DT = new DataTable();
                    DT.Load(ExecuteReader(Com, P));
                }
            }
            return DT;
        }

        public void UpdateParameters(List<Config> xLista, string xNombreEquipo)
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
                {
                    Con.Open();
                    using (SqlTransaction Tran = Con.BeginTransaction())
                    {
                        DeleteParameters(xNombreEquipo, Tran, Con);
                        AddParameters(xLista, xNombreEquipo, Tran, Con);
                        Tran.Commit();
                    }

                }

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private void AddParameters(List<Config> xLista, string xNombreEquipo, IDbTransaction xTran, IDbConnection xCon)
        {
            List<string> Querys = new List<string>();
            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@EQUIPO", xNombreEquipo));
            foreach (Config C in xLista)
            {
                P.Add(new SqlParameter("@ID" + C.ID, C.ID));
                P.Add(new SqlParameter("@VALOR" + C.ID, C.Dato));

                Querys.Add("INSERT INTO PARTERMINAL (IDTERMINAL,ID,CLAVE,VALOR) VALUES (@EQUIPO,@ID" + C.ID + ",DBO.JL_NOMBREBYCODIGO(@ID" + C.ID + "),@VALOR" + C.ID + ")");

            }
            using (SqlCommand Com = new SqlCommand("", (SqlConnection)xCon))
            {
                Com.Transaction = (SqlTransaction)xTran;
                try
                {
                    ExecuteNonQuery(Com, P, Querys);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        private void DeleteParameters(string xNombreEquipo, IDbTransaction xTran, IDbConnection xCon)
        {
            using (SqlCommand Com = new SqlCommand("DELETE FROM PARTERMINAL where IDTERMINAL = @EQUIPO", (SqlConnection)xCon))
            {
                Com.Parameters.Add(new SqlParameter("@EQUIPO", xNombreEquipo));
                Com.Transaction = (SqlTransaction)xTran;
                try
                {
                    ExecuteNonQuery(Com);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }


        public bool PuedoCobrar(int z, string xCaja)
        {
            object Caja = -1;
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT ISNULL(NUMERO,0) AS NUMERO FROM ARQUEOS WHERE (FO = 1) AND (Arqueo = 'Z') AND (CAJA = @CAJA) AND NUMERO = @Z", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@Z", z));
                    Com.Parameters.Add(new SqlParameter("@CAJA", xCaja));
                    Caja = ExecuteScalar(Com);
                }
            }
            if (Caja == null)
                return true;
            return false;
        }

        public void AnularMovimientos(List<object> list,List<object> xRemitos,object xClaves,object xCajaGeneral,bool xImprimir)
        {
            Movimiento Mov;
            try
            {
                using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
                {
                    Con.Open();
                    using (SqlTransaction Tran = Con.BeginTransaction())
                    {
                        foreach (Movimiento M in list)
                        {
                            if (M.TipoDoc != 19 && M.TipoDoc != 21)
                            {
                                Mov = (Movimiento)M;
                                DejarPendiente(Mov, Con, Tran);
                            }
                        }
                        GenerarRemitos(xRemitos, xClaves, xCajaGeneral, xImprimir,Con,Tran);
                        Tran.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void DejarPendiente(Movimiento mov, IDbConnection xCon, IDbTransaction xTran)
        {

            string Query = "UPDATE TESORERIA SET ESTADO = 'P', NUMEROREMESA = -1,SUDOCUMENTO = '', GENAPUNTE = 'VENCIMIENTO', CAJASALDADO = 0 WHERE ORIGEN = 'C' AND N = 'B' AND SERIE = @SERIE AND NUMERO = @NUMERO AND POSICION = @LINEA";
            using (SqlCommand Com = new SqlCommand(Query, (SqlConnection)xCon))
            {
                Com.Parameters.Add(new SqlParameter("@SERIE", mov.Serie));
                Com.Parameters.Add(new SqlParameter("@NUMERO", mov.Numero));
                Com.Parameters.Add(new SqlParameter("@LINEA", mov.Linea));
                Com.Transaction = (SqlTransaction)xTran;
                ExecuteNonQuery(Com);
            }
        }

        public Hashtable getSaldo(int xCodCliente)
        {
            Hashtable Saldos = new Hashtable();
            try
            {
                Saldos.Add(1, obtSaldo(xCodCliente, 1));
                Saldos.Add(2, obtSaldo(xCodCliente, 2));
            }
            catch (Exception)
            {

            }
            return Saldos;

        }

        private decimal obtSaldo(int xCodCliente, int xCodMoneda)
        {

            decimal Numero = -1;
            using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
            {
                Con.Open();
                using (SqlCommand Com = new SqlCommand("SELECT CAST(ISNULL(SUM(T.IMPORTE),0) AS DECIMAL(16, 2)) AS IMPORTE FROM TESORERIA T WHERE T.ORIGEN = 'C' AND (T.TIPODOCUMENTO = 'L' OR T.TIPODOCUMENTO = 'F') AND T.N='B' AND T.CODIGOINTERNO = @CLIENTE AND T.CODMONEDA = @MONEDA AND T.ESTADO = 'P'  ", (SqlConnection)Con))
                {
                    Com.Parameters.Add(new SqlParameter("@CLIENTE", xCodCliente));
                    Com.Parameters.Add(new SqlParameter("@MONEDA", xCodMoneda));
                    Numero = Convert.ToDecimal(ExecuteScalar(Com));
                }
            }
            return Numero;

        }



        //public void GuardarCFE(object xCFE, IDbConnection xConexion, IDbTransaction xTran)
        //{
        //    CFE CFE = (CFE)xCFE;
        //    List<IDataParameter> P = new List<IDataParameter>();
        //    P.Add(new SqlParameter("@TIPOCFE", CFE.Tipo));
        //    P.Add(new SqlParameter("@SERIECFE", CFE.Serie));
        //    P.Add(new SqlParameter("@NUMEROCFE", CFE.Numero));
        //    P.Add(new SqlParameter("@LINKCFE", CFE.Link));
        //    P.Add(new SqlParameter("@SERIEALB", CFE.SerieAlbaran));
        //    P.Add(new SqlParameter("@NUMEROALB", CFE.NumeroAlbaran));
        //    P.Add(new SqlParameter("@SERIEFAC", CFE.SerieFactura));
        //    P.Add(new SqlParameter("@NUMEROFAC", CFE.NumeroFactura));
        //    DbCommand Command = new SqlCommand("INSERT INTO TESORERIACFE(TIPOCFE,SERIECFE,NUMEROCFE,LINKCFE,SERIEALB,NUMEROALB,SERIEFAC,NUMEROFAC) VALUES (@TIPOCFE,@SERIECFE,@NUMEROCFE,@LINKCFE,@SERIEALB,@NUMEROALB,@SERIEFAC,@NUMEROFAC)", (SqlConnection)xConexion);
        //    Command.Transaction = (SqlTransaction)xTran;
        //    ExecuteNonQuery(Command, P);
        //}



        public int GenerarEntrega(object xEntrega, object xCaja)
        {
            try
            {
                int NumeroR = -1;
                int NumeroE = -1;
                Remito R = (Remito)xEntrega;
                using (SqlConnection Con = new SqlConnection(GlobalConnectionString))
                {
                    Con.Open();
                    using (SqlTransaction Tran = Con.BeginTransaction())
                    {
                        CajaGeneral C = (CajaGeneral)xCaja;
                        NumeroR = NumeroRecibo(C.Recibos, Con, Tran, 20);
                        NumeroE = NumeroRecibo(R.Serie, Con, Tran, 62);
                        R.Recibo = NumeroR;
                        GuardarCabecera(R, Con, Tran, NumeroE);
                        GuardarVentaLin(R, Con, Tran, NumeroE);
                        GuardarFVentas(R, Con, Tran, NumeroE);
                        //
                        List<IDataParameter> P = new List<IDataParameter>();

                        P.Add(new SqlParameter("@ESTADO1", R.Estado()));
                        P.Add(new SqlParameter("@ESTADO2", 'P'));
                        P.Add(new SqlParameter("@GENAPUNTE1", R.GenApunte()));
                        P.Add(new SqlParameter("@GENAPUNTE2", "VENCIMIENTO"));
                        P.Add(new SqlParameter("@ZSALDADO1", R.NumeroZ()));
                        P.Add(new SqlParameter("@ZSALDADO2", Convert.ToInt32(0)));
                        P.Add(new SqlParameter("@CAJASALDADO1", R.SerieCaja()));
                        P.Add(new SqlParameter("@CAJASALDADO2", string.Empty));
                        P.Add(new SqlParameter("@NUMEROREMESA1", NumeroR));
                        P.Add(new SqlParameter("@NUMEROREMESA2", -1));
                        P.Add(new SqlParameter("@SUDOCUMENTO1", R.Sudocumento()));
                        P.Add(new SqlParameter("@SUDOCUMENTO2", string.Empty));
                        P.Add(new SqlParameter("@IMPORTE1", R.Importe()));
                        P.Add(new SqlParameter("@IMPORTE2", (R.Importe() * -1)));
                        P.Add(new SqlParameter("@CODFORMAPAGO1", R.FormaPago()));
                        P.Add(new SqlParameter("@CODFORMAPAGO2", 2));
                        P.Add(new SqlParameter("@CODTIPOPAGO1", R.TipoPago()));
                        P.Add(new SqlParameter("@CODTIPOPAGO2", 7));
                        P.Add(new SqlParameter("@POSICION1", 1));
                        P.Add(new SqlParameter("@POSICION2", 2));


                        P.Add(new SqlParameter("@ORIGEN", "C"));
                        P.Add(new SqlParameter("@TIPODOCUMENTO", "F"));
                        P.Add(new SqlParameter("@SERIE", R.Serie));
                        P.Add(new SqlParameter("@NUMERO", NumeroE));
                        P.Add(new SqlParameter("@N", 'B'));

                        P.Add(new SqlParameter("@FECHADOCUMENTO", DateTime.Today));
                        P.Add(new SqlParameter("@FECHAVENCIMIENTO", DateTime.Today));
                        P.Add(new SqlParameter("@CODIGOINTERNO", R.Cliente.IdCliente));
                        P.Add(new SqlParameter("@FECHASALDADO", DateTime.Today));
                        P.Add(new SqlParameter("@FACTORMONEDA", R.FactorMoneda));
                        P.Add(new SqlParameter("@CODMONEDA", R.Moneda.Codmoneda));
                        P.Add(new SqlParameter("@SUBCTA", 1));
                        List<string> Q = new List<string>();
                        Q.Add("INSERT INTO TESORERIA(ORIGEN,TIPODOCUMENTO,SERIE,NUMERO,N,POSICION,FECHADOCUMENTO,FECHAVENCIMIENTO,CODIGOINTERNO,IMPORTE,CODFORMAPAGO,CODTIPOPAGO,ESTADO,FECHASALDADO,FACTORMONEDA,CODMONEDA,ZSALDADO,CAJASALDADO,NUMEROREMESA,GENAPUNTE,SUDOCUMENTO,SUBCTA) VALUES(@ORIGEN, @TIPODOCUMENTO, @SERIE, @NUMERO, @N, @POSICION1, @FECHADOCUMENTO, @FECHAVENCIMIENTO, @CODIGOINTERNO, @IMPORTE1, @CODFORMAPAGO1, @CODTIPOPAGO1, @ESTADO1, @FECHASALDADO, @FACTORMONEDA, @CODMONEDA, @ZSALDADO1, @CAJASALDADO1, @NUMEROREMESA1, @GENAPUNTE1,@SUDOCUMENTO1, @SUBCTA)");
                        Q.Add("INSERT INTO TESORERIA(ORIGEN,TIPODOCUMENTO,SERIE,NUMERO,N,POSICION,FECHADOCUMENTO,FECHAVENCIMIENTO,CODIGOINTERNO,IMPORTE,CODFORMAPAGO,CODTIPOPAGO,ESTADO,FECHASALDADO,FACTORMONEDA,CODMONEDA,ZSALDADO,CAJASALDADO,NUMEROREMESA,GENAPUNTE,SUDOCUMENTO,SUBCTA) VALUES(@ORIGEN, @TIPODOCUMENTO, @SERIE, @NUMERO, @N, @POSICION2, @FECHADOCUMENTO, @FECHAVENCIMIENTO, @CODIGOINTERNO, @IMPORTE2, @CODFORMAPAGO2, @CODTIPOPAGO2, @ESTADO2, @FECHASALDADO, @FACTORMONEDA, @CODMONEDA, @ZSALDADO2, @CAJASALDADO2, @NUMEROREMESA2, @GENAPUNTE2,@SUDOCUMENTO2, @SUBCTA)");
                        using (SqlCommand Com = new SqlCommand("", (SqlConnection)Con))
                        {
                            Com.Transaction = (SqlTransaction)Tran;
                            ExecuteNonQuery(Com, P, Q);
                        }
                        Tran.Commit();
                    }
                }
                return NumeroR;
            }
            catch (Exception e)
            {
                throw e;

            }
        }


        #region Heredados

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









        #endregion

    }
}
