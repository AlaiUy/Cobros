using Aguiñagalde.Entidades;
using Aguiñagalde.FabricaMappers;
using Aguiñagalde.Interfaces;
using Aguiñagalde.Reportes;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Aguiñagalde.Gestoras
{
    public class GCobros : IObservable
    {

        private static IMapperCobros DBCobros;
        private List<IObserver> _Observers = new List<IObserver>();
        private List<Moneda> _ListaMonedas;
        private CajaGeneral _Caja;
        private Empresa Claves;
        private static readonly object padlock = new object();
        private static GCobros _Instance = null;


        public List<Moneda> ListaMonedas
        {
            get { return _ListaMonedas; }
            set { _ListaMonedas = value; }
        }


        public static GCobros getInstance()
        {
            if (_Instance == null)
            {
                lock (padlock)
                {
                    if (_Instance == null)
                        _Instance = new GCobros();
                }
            }

            return _Instance;
        }






        public CajaGeneral Caja
        {
            get { return _Caja; }
        }


        private GCobros()
        {
            DBCobros = (IMapperCobros)Factory.getMapper(this.GetType());
        }
        

        public decimal CoefMoneda(int Moneda)
        {
            foreach (Moneda M in _ListaMonedas)
                if (M.Codmoneda == Moneda)
                    return M.Mora;
            return 0;
        }



        public void UpdateParameters(List<Config> xListConfigs)
        {
            DBCobros.UpdateParameters(xListConfigs, Environment.MachineName);
        }

        public void Iniciar(string xUsuario, string xPassword)
        {



            notifyObservers("Cargando Base de datos");

            if (DBCobros == null)
                DBCobros = (IMapperCobros)Factory.getMapper(this.GetType());


            notifyObservers("Validando Usuario");

            Usuario U = (Usuario)DBCobros.getUsuario(xUsuario, xPassword);
            if (U == null)
                throw new Exception("El usuario no es correcto");

            if (!U.Permiso(2))
                throw new Exception("No puedes usar el programa");






            notifyObservers("Cargando Caja");
            _Caja = (CajaGeneral)DBCobros.getCajaByID(Environment.MachineName, U);

            notifyObservers("Cargando Monedas");
            _ListaMonedas = new List<Moneda>();
            foreach (object M in DBCobros.getListaMonedas())
                _ListaMonedas.Add((Moneda)M);

            notifyObservers("Cargando Claves");
            CargarClaves();




            GClientes.Instance().Iniciar();



        }




        public void Register(IObserver xObserver)
        {
            _Observers.Add(xObserver);
        }

        public void UnRegister(IObserver xObserver)
        {
            _Observers.Remove(xObserver);
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


        public void notifyObservers(object xObj)
        {
            IObserver Obs;
            foreach (IObserver O in _Observers)
            {
                Obs = (IObserver)O;
                Obs.Update(xObj);
            }
        }


        private CFE LeerCFERetorno(string xRuta, Remito xR)
        {
            CFE C = null;

            XmlReader document = new XmlTextReader(_Caja.SalidaCFE + xRuta + ".xml");

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
            File.Move(_Caja.SalidaCFE + xRuta + ".xml", _Caja.BackCFE + xRuta + ".xml");
            return C;
        }



        public bool CajaAbierta()
        {
            return DBCobros.PuedoCobrar(_Caja.Z, _Caja.Id);
        }

        private void CargarClaves()
        {
            Claves = (Empresa)DBCobros.getClavesEmpresa(_Caja.Sucursal);
        }

        public void Pagar(List<MovimientoGeneral> xMovimientos, bool xMora, ClienteActivo xCliente, bool xDescuento, int xFormaPago, bool xKeep)
        {
            if (xMovimientos.Count < 1)
                throw new Exception("No hay movimientos que pagar");


            List<Movimiento> MovimientosPesos = new List<Movimiento>();
            List<Movimiento> MovimientosDolares = new List<Movimiento>();

            MovimientosPesos = MovimientosDeMoneda(xMovimientos, 1);
            MovimientosDolares = MovimientosDeMoneda(xMovimientos, 2);


            int NumRecibo = -1;
            List<object> Clasificados;
            List<object> Remitos = new List<object>();


            if (MovimientosPesos.Count > 0)
            {
                Clasificados = new List<object>();
                Remitos = new List<object>();
                Remitos = Documentos(MovimientosPesos, xCliente, xMora, _ListaMonedas.Find(x => x.Codmoneda == 1), xDescuento, xFormaPago, xKeep);
                foreach (MovimientoGeneral M in MovimientosPesos)
                {
                    if (xMora)
                        M.Mora = M.getMora();

                    if (M.Mora == 0 && xDescuento)
                        M.Mora = Math.Round(M.getDescuento(_Caja.NumeroDescuento, xFormaPago, xKeep), 2);

                    if (M.ImportePagado != 0)
                    {
                        MovimientoGeneral Pendiente = CrearMovimientoPendiente(M);
                        if (Math.Abs(Pendiente.Importe) > Convert.ToDecimal(0.05))
                            Clasificados.Add(Pendiente);
                    }
                    MovimientoGeneral Saldado = CrearMovimientoSaldado(M);
                    Clasificados.Add(Saldado);
                }
                NumRecibo = DBCobros.GenerarRecibo(Clasificados, Caja,Remitos,_Caja.Imprimir,Claves);
                if (NumRecibo > -1)
                {
                    Impresion Imprimir = new Impresion();
                    
                    Recibo R = ObtenerReciboByID(Caja.Recibos, NumRecibo, xCliente.IdCliente, 1);
                    Hashtable Saldos = ObtenerSaldoByMoneda(R.Cliente.IdCliente);
                    decimal P = Convert.ToDecimal(Saldos[1]);
                    decimal D = Convert.ToDecimal(Saldos[1]);
                    Imprimir.Imprimir(R, true, Saldos);
                    NumRecibo = -1;
                }

                else
                    throw new Exception("No se pudo generar el recibo en Pesos");
            }

            if (MovimientosDolares.Count > 0)
            {
                Clasificados = new List<object>();
                Remitos = new List<object>();
                Remitos = Documentos(MovimientosDolares, xCliente, xMora, _ListaMonedas.Find(x => x.Codmoneda == 2), xDescuento, xFormaPago, xKeep);
                foreach (MovimientoGeneral M in MovimientosDolares)
                {

                    if (xMora)
                        M.Mora = M.getMora();

                    if (M.Mora == 0 && xDescuento)
                        M.Mora = Math.Round(M.getDescuento(_Caja.NumeroDescuento, xFormaPago, xKeep), 2);

                    if (M.ImportePagado != 0)
                    {
                        MovimientoGeneral Pendiente = CrearMovimientoPendiente(M);
                        if (Math.Abs(Pendiente.Importe) > Convert.ToDecimal(0.05))
                            Clasificados.Add(Pendiente);
                    }
                    MovimientoGeneral Saldado = CrearMovimientoSaldado(M);
                    Clasificados.Add(Saldado);
                }
                NumRecibo = DBCobros.GenerarRecibo(Clasificados, Caja, Remitos, _Caja.Imprimir, Claves);
                if (NumRecibo > -1)
                {
                    Impresion Imprimir = new Impresion();
                    Recibo R = ObtenerReciboByID(Caja.Recibos, NumRecibo, xCliente.IdCliente, 2);
                    Hashtable Saldos = ObtenerSaldoByMoneda(R.Cliente.IdCliente);
                    decimal P = Convert.ToDecimal(Saldos[1]);
                    decimal D = Convert.ToDecimal(Saldos[2]);
                    Imprimir.Imprimir(R, true, Saldos);
                }
                else
                    throw new Exception("No se pudo generar el recibo en Pesos");

            }
        }

        private Hashtable ObtenerSaldoByMoneda(int xCodCliente)
        {
            return DBCobros.getSaldo(xCodCliente);
        }

        public DataTable getParametros()
        {
            List<int> Configs = new List<int>();
            Configs.Add(10);
            Configs.Add(44);
            Configs.Add(45);
            Configs.Add(43);
            Configs.Add(54);
            Configs.Add(53);
            Configs.Add(46);
            Configs.Add(56);
            Configs.Add(55);
            Configs.Add(57);
            Configs.Add(66);
            return (DataTable)DBCobros.Parametros(Environment.MachineName, Configs);
        }







        // ** Restructiracion Pagar ** ///

        private List<Movimiento> MovimientosDeMoneda(List<MovimientoGeneral> xLista, int xCodMoneda)
        {
            List<Movimiento> L = new List<Movimiento>();
            //List<MovimientoGeneral> Lista = new List<MovimientoGeneral>();
            //foreach (MovimientoGeneral M in xLista)
            //{
            //    if (M.Moneda.Codmoneda == xCodMoneda)
            //        //Lista.Add(M);
            //}
            foreach (MovimientoGeneral M in xLista.FindAll(x => x.Moneda.Codmoneda == xCodMoneda))
            {
                L.Add((Movimiento)M);
            }
            return L;
            //return Lista;
        }






        private MovimientoGeneral CrearMovimientoSaldado(MovimientoGeneral xM)
        {
            xM.Importe = xM.ImportePagado - xM.Mora;
            xM.ImportePagado = 0;
            MovimientoGeneral S = (MovimientoGeneral)xM.Clone();
            if (S.Moneda.Codmoneda == 1)
                S.FormaPago = 1;
            else
                S.FormaPago = 3;
            S.Estado = "S";
            S.TipoPago = 1;
            S.GenApunte = "SALDADO (F/F)";
            S.TipoPago = 1;
            S.Cajasaldado = Caja.Id;
            S.Sudocumento = Caja.Recibos;
            S.Zsaldado = Caja.Z;
            if (S.Importe < 0)
                throw new Exception("No se puede generar el recibo");
            return S;
        }

        private MovimientoGeneral CrearMovimientoPendiente(MovimientoGeneral xMovimientoSaldar)
        {
            MovimientoGeneral Pendiente = (MovimientoGeneral)xMovimientoSaldar.Clone();
            Pendiente.Importe -= (xMovimientoSaldar.ImportePagado - xMovimientoSaldar.Mora);
            Pendiente.ImportePagado = 0;
            Pendiente.Mora = 0;
            Pendiente.Estado = "P";
            return Pendiente;
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
                    Movimiento M = R.Movimiento[Index];
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


        private void PrintAndSaveRemitos(List<object> xList, bool xImprimir, int xNumRecibo)
        {
            foreach (object o in xList)
            {
                try
                {
                    Remito Re = (Remito)o;
                    Re.Recibo = xNumRecibo;
                    Re.SerieRecibo = _Caja.Recibos;
                    Re.Caja = _Caja.Id;
                    Re.Z = Caja.Z;
                    int NumeroRemito = DBCobros.GenerarRemitos(Re, Claves, _Caja, xImprimir);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message + " -- " + "Print And Save");
                }
            }
        }

        public bool LeerXMLRetorno(string xNombre, CajaGeneral xCaja)
        {
            System.Threading.Thread.Sleep(3000);
            bool Lectura = false;
            FileStream xFile;
            FileInfo FI;
            string Lugar = xCaja.SalidaCFE.Trim();
            string xArchivo = xNombre + ".xml";
            string Archivo = Lugar + xArchivo;

            while (!Lectura)
            {
                try
                {
                    if (File.Exists(Archivo))
                    {
                        FI = new FileInfo(Archivo);

                        xFile = FI.OpenWrite();
                        Lectura = true;
                        xFile.Close();
                    }
                }
                catch (Exception)
                {
                    Lectura = false;

                }
            }
            XmlReader Reader = new XmlTextReader(Archivo);
            while (Reader.Read())
            {
                XmlNodeType Type = Reader.NodeType;
                if (Type == XmlNodeType.CDATA)
                {
                    XmlDocument TempDoc = new XmlDocument();
                    TempDoc.LoadXml(Reader.ReadContentAsString());
                    XmlNodeList Nodos = TempDoc.GetElementsByTagName("CFEStatus");
                    int Index;
                    for (Index = 0; Index <= Nodos.Count - 1; Index++)
                    {
                        switch (Nodos[Index].InnerXml)
                        {
                            case "8":
                            case "3":
                            case "1":
                                Reader.Close();
                                return false;
                            case "2":
                            case "5":
                            case "4":
                                Reader.Close();
                                return true;
                        }
                    }
                }
            }
            Reader.Close();
            return false;
        }

        //private void GenerarXMLRemito(Remito xR, Empresa Claves, CajaGeneral xCaja, bool xImprimir)
        //{
        //    Remito R = (Remito)xR;
        //    XmlTextWriter Writer = new XmlTextWriter(xCaja.EntradaCFE.Trim() + xR.Serie + xR.Numero + ".xml", Encoding.UTF8);
        //    Writer.WriteStartDocument();
        //    Writer.WriteStartElement("EnvioCFE");
        //    Writer.WriteStartElement("Encabezado");
        //    Writer.WriteStartElement("EmpCodigo");
        //    Writer.WriteString(Claves.CodEmpresa.ToString());
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("EmpPK");
        //    Writer.WriteString(Claves.EmpPK);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("EmpCA");
        //    Writer.WriteString(Claves.Clave.ToString());
        //    Writer.WriteEndElement();
        //    Writer.WriteEndElement();

        //    Writer.WriteStartElement("CFE");

        //    Writer.WriteStartElement("CFEItem");

        //    Writer.WriteStartElement("IdDoc");

        //    Writer.WriteStartElement("CFETipoCFE");
        //    Writer.WriteValue(R.NumeroCFE());
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFESerie");

        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFENro");

        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFEImpresora");
        //    Writer.WriteString(xCaja.Impresora);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFEImp");
        //    if (xImprimir)
        //        Writer.WriteString("S");
        //    else
        //        Writer.WriteString("N");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFEImpCantidad");
        //    Writer.WriteValue(1);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFEFchEmis");
        //    Writer.WriteString(R.Fecha.ToString("yyyy-MM-dd"));
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFEPeriodoDesde");

        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFEPeriodoHasta");

        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFEMntBruto");
        //    Writer.WriteValue(1);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFEFmaPago");
        //    // revisar aca///
        //    Writer.WriteValue(1);
        //    ////
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFEFchVenc");
        //    Writer.WriteString(DateTime.Today.ToShortDateString());
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFETipoTraslado");
        //    Writer.WriteValue(1);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFEAdenda");
        //    Writer.WriteString(R.Adenda());
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CAESeq");
        //    Writer.WriteString("0");
        //    Writer.WriteEndElement();


        //    //            Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFENumReferencia");
        //    Writer.WriteValue(1);

        //    Writer.WriteEndElement();


        //    //Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFEImpFormato");

        //    Writer.WriteValue(1);

        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFEIdCompra");
        //    Writer.WriteValue(0);

        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFEQrCode");
        //    Writer.WriteValue(1);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFEDatosAvanzados");
        //    Writer.WriteValue(1);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("CFERepImpresa");
        //    Writer.WriteValue(1);
        //    Writer.WriteEndElement();
        //    Writer.WriteEndElement();




        //    #region Emisor
        //    /* DATOS EMISOR */
        //    Writer.WriteStartElement("Emisor");

        //    Writer.WriteStartElement("EmiRznSoc");
        //    Writer.WriteString("Ferreteria y Barraca Aguiñagalde");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("EmiComercial");
        //    Writer.WriteString("Hector B. Aguiñagalde");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("EmiGiroEmis");
        //    //'.WriteString("NI IDEA")
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("EmiTelefono");
        //    Writer.WriteString("25106");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("EmiTelefono2");
        //    //'.WriteString("473 20501");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("EmiCorreoEmisor");
        //    Writer.WriteString("eticket@aguinagalde.com.uy");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("EmiSucursal");
        //    Writer.WriteString("1");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("EmiDomFiscal");
        //    Writer.WriteString("Barbieri 1080");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("EmiCiudad");
        //    Writer.WriteString("Salto");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("EmiDepartamento");
        //    Writer.WriteString("Salto");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("EmiInfAdicional");
        //    Writer.WriteEndElement();
        //    Writer.WriteEndElement();
        //    #endregion


        //    #region Receptor

        //    Writer.WriteStartElement("Receptor");
        //    Writer.WriteStartElement("RcpTipoDocRecep");
        //    Writer.WriteValue(R.Cliente.TipoDocumento(R.IS.Codigo));
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("RcpTipoDocDscRecep");
        //    Writer.WriteString("");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("RcpCodPaisRecep");
        //    Writer.WriteString("UY");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("RcpDocRecep");
        //    Writer.WriteString(R.Cliente.Documento(R.IS.Codigo));
        //    Writer.WriteEndElement();
        //    //acordate aca porner el subcuenta de la bonificacion corresponditnete;
        //    //Writer.WriteValue(R.Cliente.Documento(0));



        //    Writer.WriteStartElement("RcpRznSocRecep");
        //    Writer.WriteString(R.Cliente.NombreSubCuenta(R.IS.Codigo));
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("RcpDirRecep");
        //    Writer.WriteString(R.Cliente.DireccionSubCuenta(R.IS.Codigo));
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("RcpCiudadRecep");
        //    Writer.WriteString("Salto");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("RcpDeptoRecep");
        //    Writer.WriteString("Salto");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("RcpCP");
        //    Writer.WriteString("");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("RcpCorreoRecep");
        //    Writer.WriteString(R.Cliente.CamposLibres.Email);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("RcpInfAdiRecep");
        //    Writer.WriteString("");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("RcpDirPaisRecep");
        //    Writer.WriteString("");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("RcpDstEntregaRecep");
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("RcpEmlArchivos");
        //    Writer.WriteValue(1);
        //    Writer.WriteEndElement();
        //    // End If
        //    Writer.WriteEndElement();

        //    #endregion

        //    #region Totales
        //    Writer.WriteStartElement("Totales");

        //    Writer.WriteStartElement("TotTpoMoneda");
        //    Writer.WriteString(R.Moneda.CFESubfijo());
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotTpoCambio");
        //    Writer.WriteValue(R.FactorMoneda);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotMntNoGrv");
        //    Writer.WriteValue(0);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotMntExpoyAsim");
        //    Writer.WriteValue(0);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotMntImpuestoPerc");
        //    Writer.WriteValue(0);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotMntIVaenSusp");
        //    Writer.WriteValue(0);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotMntNetoIvaTasaMin");

        //    Writer.WriteValue(0);

        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotMntNetoIVATasaBasica");
        //    Writer.WriteValue(Math.Abs(R.TotalBruto()));
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotMntNetoIVAOtra");
        //    Writer.WriteValue(0);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotIVATasaMin");
        //    Writer.WriteValue(10);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotIVATasaBasica");
        //    Writer.WriteValue(22);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotMntIVATasaMin");
        //    Writer.WriteValue(0);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotMntIVATasaBasica");
        //    Writer.WriteValue(Math.Abs((Math.Abs(R.Importe()) - Math.Abs(R.TotalBruto()))));
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotMntIVAOtra");
        //    Writer.WriteValue(0);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotMntTotal");
        //    Writer.WriteValue(Math.Abs(R.Importe()));
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotMntTotRetenido");
        //    Writer.WriteValue(0);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotMntCreditoFiscal");
        //    Writer.WriteValue(0);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("RetencPercepTot");
        //    Writer.WriteEndElement();


        //    Writer.WriteStartElement("TotMontoNF");
        //    Writer.WriteValue(0);
        //    Writer.WriteEndElement();
        //    Writer.WriteStartElement("TotMntPagar");
        //    Writer.WriteValue(Math.Abs(R.Importe()));
        //    Writer.WriteEndElement();
        //    Writer.WriteEndElement();


        //    #endregion






        //    Writer.WriteStartElement("Detalle");
        //    foreach (LineaRemito L in R.Lineas)
        //    {
        //        Writer.WriteStartElement("Item");
        //        Writer.WriteStartElement("CodItem");
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("IteIndFact");
        //        Writer.WriteValue(3);
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("IteIndAgenteResp");
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("IteNomItem");
        //        Writer.WriteString(L.Descripcion);
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("IteDscItem");
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("IteCantidad");
        //        Writer.WriteValue(Math.Abs(L.Unidadestotal));
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("IteUniMed");
        //        Writer.WriteString("C/U");
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("ItePrecioUnitario");
        //        Writer.WriteValue(Math.Abs(L.Total()));
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("IteDescuentoPct");
        //        Writer.WriteValue(0);
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("IteDescuentoMonto");
        //        Writer.WriteValue(0);
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("SubDescuento");
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("RetencPercep");
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("IteMontoItem");
        //        Writer.WriteValue(Math.Abs(L.Total()));
        //        Writer.WriteEndElement();
        //        Writer.WriteEndElement();
        //    }
        //    Writer.WriteEndElement();

        //    Writer.WriteStartElement("Referencia");
        //    int Index = 0;
        //    if (R.CFE())
        //    {
        //        while (Index < 39 && Index < R.Movimiento.Count)
        //        {
        //            MovimientoGeneral M = R.Movimiento[Index];
        //            Writer.WriteStartElement("ReferenciaItem");
        //            Writer.WriteStartElement("RefNroLinRef");
        //            Writer.WriteValue(Index);
        //            Writer.WriteEndElement();
        //            Writer.WriteStartElement("RefIndGlobal");
        //            Writer.WriteValue(0);
        //            Writer.WriteEndElement();
        //            Writer.WriteStartElement("RefTpoDocRef");
        //            Writer.WriteValue(M.CFE.Tipo);
        //            Writer.WriteEndElement();
        //            Writer.WriteStartElement("RefSerie");
        //            Writer.WriteString(M.CFE.Serie);
        //            Writer.WriteEndElement();
        //            Writer.WriteStartElement("RefNroCFERef");
        //            Writer.WriteValue(M.CFE.Numero);
        //            Writer.WriteEndElement();
        //            Writer.WriteStartElement("RefRazonRef");
        //            Writer.WriteEndElement();
        //            Writer.WriteStartElement("RefFechaCFEref");
        //            Writer.WriteString(M.Fecha.ToString("yyyy-MM-dd"));
        //            Writer.WriteEndElement();
        //            Writer.WriteEndElement();
        //            Index += 1;
        //        }

        //    }
        //    else
        //    {
        //        Writer.WriteStartElement("ReferenciaItem");
        //        Writer.WriteStartElement("RefNroLinRef");
        //        Writer.WriteValue(1);
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("RefIndGlobal");
        //        Writer.WriteValue(1);
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("RefTpoDocRef");
        //        Writer.WriteValue(R.NumeroCFE());
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("RefSerie");
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("RefNroCFERef");
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("RefRazonRef");
        //        Writer.WriteString("Documento a anular es un documento anterior al inicio de la facturacion electronica");
        //        Writer.WriteEndElement();
        //        Writer.WriteStartElement("RefFechaCFEref");
        //        Writer.WriteString(R.Fecha.ToString("yyyy-MM-dd"));
        //        Writer.WriteEndElement();


        //        Writer.WriteEndElement();
        //        Index += 1;
        //    }
        //    Writer.WriteEndElement();
        //    Writer.WriteEndElement();
        //    Writer.WriteEndElement();
        //    Writer.WriteEndDocument();
        //    Writer.Close();
        //}




        //private void AsignarNumeros(List<object> xList)
        //{
        //    foreach (object O in xList)
        //    {
        //        Remito R = (Remito)O;
        //        R.Numero = DBCobros.ObtenerNumeroDocumento(R.TipoDoc(), R.Serie);
        //    }

        //}

        public void ReimprimirRecibo(int xNumRecibo, string xSerie, int xCodCliente, int xCodMoneda, DateTime xFecha)
        {
            Impresion Imprimir = new Impresion();
            Recibo R = ObtenerReciboByID(xSerie, xNumRecibo, xCodCliente, xCodMoneda, xFecha);
            Hashtable Saldos = ObtenerSaldoByMoneda(R.Cliente.IdCliente);
            Imprimir.Imprimir(R, false, Saldos);
        }



        public Recibo ObtenerReciboByID(string xSerie, int xID, int xCodCliente, int xMoneda)
        {
            if (xID < 1)
            {
                throw new Exception("Numero de recibo incorrecto");
            }


            ClienteActivo C = (ClienteActivo)GClientes.Instance().getByID(xCodCliente.ToString(), false);

            List<object> L = new List<object>();
            L = DBCobros.getMovimientosByRecibo(xSerie, xID,xCodCliente);
            Recibo R;
            R = new Recibo(DateTime.Today, C, xSerie, xID, L, xMoneda);
            return R;
        }

        public Recibo ObtenerReciboByID(string xSerie, int xID, int xCodCliente, int xMoneda, DateTime xFecha)
        {
            if (xID < 1)
            {
                throw new Exception("Numero de recibo incorrecto");
            }


            ClienteActivo C = (ClienteActivo)GClientes.Instance().getByID(xCodCliente.ToString(), false);

            List<object> L = new List<object>();
            L = DBCobros.getMovimientosByRecibo(xSerie, xID,xCodCliente);
            Recibo R;
            R = new Recibo(xFecha, C, xSerie, xID, L, xMoneda);
            return R;
        }



        public DataTable VerRecibos()
        {
            int zCaja = _Caja.Z;
            string sSerie = _Caja.Id;
            return (DataTable)DBCobros.getAllRecibos(zCaja, sSerie);


        }

        private List<object> Documentos(List<Movimiento> xMovs, ClienteActivo xCliente, bool xMora, Moneda xMoneda, bool xDescuento, int xFormaPago, bool xKeep)
        {
            if (xMovs.Count < 1)
                return null;

            if (xCliente == null)
                return null;

            if (!xMora && !xDescuento)
                return null;

            if (xMoneda == null)
                return null;

            decimal zCotizacion;
            if (xMoneda.Codmoneda == 1)
                zCotizacion = 1;
            else
                zCotizacion = GCobros.getInstance().Caja.Cotizacion;

            List<object> Lts = new List<object>();

            foreach (SubCuenta SC in xCliente.SubCuentas)
            {
                decimal zGlobal = 0, zCFE = 0, zMora = 0;
                List<Movimiento> ListaCFEs = new List<Movimiento>();
                List<Movimiento> ListaGlobal = new List<Movimiento>();
                List<Movimiento> ListaMora = new List<Movimiento>();
                foreach (Movimiento M in xMovs)
                {
                    if (SC.Codigo == M.SubCta)
                    {
                        if (M.CFE != null)
                        {
                            zCFE += Math.Round(M.getDescuento(_Caja.NumeroDescuento, xFormaPago, xKeep), 2);
                            ListaCFEs.Add(M);
                        }
                        else
                        {
                            zGlobal += Math.Round(M.getDescuento(_Caja.NumeroDescuento, xFormaPago, xKeep), 2);
                            ListaGlobal.Add(M);
                        }
                        if (xMora)
                        {
                            zMora += M.getMora();
                            ListaMora.Add(M);
                        }
                    }
                }
                if (xDescuento)
                {
                    if (zCFE < 0)
                    {
                        Lts.Add(CrearBonificacion(Math.Abs(zCFE), xMoneda, xCliente, SC, ListaCFEs, zCotizacion));
                    }
                    if (zGlobal < 0)
                    {
                        Lts.Add(CrearBonificacion(Math.Abs(zGlobal), xMoneda, xCliente, SC, ListaGlobal, zCotizacion));
                    }
                }
                if (zMora > 0)
                {
                    Lts.Add(CrearMora(zMora, xMoneda, xCliente, SC, ListaMora, zCotizacion));
                }
            }
            return Lts;
        }

        private Remito CrearBonificacion(decimal xImporte, Moneda xMoneda, ClienteActivo xCliente, SubCuenta xSubCuenta, object xMovs, decimal xFactorMoneda)
        {

            string Serie = "";
            int Z = GCobros.getInstance().Caja.Z;
            string Caja = GCobros.getInstance().Caja.Id;
            List<Movimiento> L = new List<Movimiento>();
            if (xMovs != null)
            {
                if (xMovs is Movimiento)
                {
                    L = new List<Movimiento>();
                    L.Add((Movimiento)xMovs);
                }
                L = (List<Movimiento>)xMovs;
            }

            List<LineaRemito> Lineas = new List<LineaRemito>();
            LineaRemito Linea = null;

            Serie = GCobros.getInstance().Caja.SerieDescuento;
            Linea = CrearLinea(xImporte, 100001, "DESCUENTO", Serie, "AJUSTE PRECIOS POR BONIF.", Lineas.Count() + 1, xMoneda, 22);
            Linea.Unid1 = -1;
            Linea.Unid2 = -1;
            Linea.Unid3 = -1;
            Linea.Unid4 = -1;
            Linea.Unidadestotal = -1;
            Lineas.Add(Linea);
            SubCuenta SC;
            if (xSubCuenta != null)
               SC = xSubCuenta;
            else
                SC = new SubCuenta(xCliente.IdCliente, 1);
            Remito R = null;
            R = new RBonificacion(-1, Serie, DateTime.Today, xMoneda,2, _Caja.Usuario.CodVendedor,xCliente,Lineas,"POR RECIBO: ",SC);

            R.Movimiento = L;
            
            R.FactorMoneda = xFactorMoneda;
            R.SerieRecibo = _Caja.Recibos;

            return R;
        }

        private Remito CrearBonificacion(decimal xImporte, Moneda xMoneda, ClienteActivo xCliente, SubCuenta xSubCuenta, object xMovs, decimal xFactorMoneda,string xDescripcionLinea)
        {

            string Serie = "";
            int Z = GCobros.getInstance().Caja.Z;
            string Caja = GCobros.getInstance().Caja.Id;
            List<Movimiento> L = new List<Movimiento>();
            if (xMovs != null)
            {
                if (xMovs is Movimiento)
                {
                    L = new List<Movimiento>();
                    L.Add((Movimiento)xMovs);
                }
                
            }

            List<LineaRemito> Lineas = new List<LineaRemito>();
            LineaRemito Linea = null;

            Serie = GCobros.getInstance().Caja.SerieDescuento;
            Linea = CrearLinea(xImporte, 100001, "DESCUENTO", Serie, xDescripcionLinea, Lineas.Count() + 1, xMoneda, 22);
            Linea.Unid1 = -1;
            Linea.Unid2 = -1;
            Linea.Unid3 = -1;
            Linea.Unid4 = -1;
            Linea.Unidadestotal = -1;
            Lineas.Add(Linea);
            SubCuenta SC;
            if (xSubCuenta != null)
                SC = xSubCuenta;
            else
                SC = new SubCuenta(xCliente.IdCliente, 1);
            Remito R = null;
            R = new RBonificacion(-1, Serie, DateTime.Today, xMoneda, 2, _Caja.Usuario.CodVendedor, xCliente, Lineas, "POR RECIBO: ", SC);

            R.Movimiento = L;

            R.FactorMoneda = xFactorMoneda;
            R.SerieRecibo = _Caja.Recibos;

            return R;
        }

         

        private Remito CrearDebito(decimal xImporte, int xCodMoneda, Persona xPersona, SubCuenta xSubCta, object xMovimientos, decimal xCotizacion)
        {
            Moneda M = _ListaMonedas.Find(x => x.Codmoneda == xCodMoneda);
            List<LineaRemito> Lineas = new List<LineaRemito>();
            LineaRemito Linea = CrearLinea(xImporte, 67281, "GASTOS ADMINISTRATIVOS",_Caja.Debito, "DEBITO AUTOMATICO", 1, M, 22);
            Lineas.Add(Linea);
            Debito Deb = new Debito(-1,_Caja.Debito, DateTime.Today, M, (ClienteActivo)xPersona, Lineas, 2, "", _Caja.Usuario.CodVendedor, xSubCta);
            return Deb;
        }

        private LineaRemito CrearLinea(decimal xImporte,int xCodigoInterno, string xReferencia, string xSerie, string xDescripcion,int xNumLinea,Moneda xMoneda,decimal xIva)
        {
            LineaRemito Linea = new LineaRemito(xCodigoInterno, xReferencia, xSerie,xMoneda.Codmoneda,xIva,xDescripcion);
            
            Linea.Precio = xImporte / Convert.ToDecimal(1.22);
            Linea.Dto = 0;
            Linea.Precioiva = Linea.Total();
            Linea.Tipoimpuesto = 1;
            Linea.Codtarifa = 10;
            Linea.CodAlmacen = "LB";
            Linea.Precioiva = xImporte;
            Linea.Udsexpansion = 1;
            Linea.Totalexpansion = xImporte;
            Linea.Costeiva = 0;
            Linea.Fechaentrega = DateTime.Today;
            Linea.NumLin = xNumLinea;

            return Linea;
        }



        private Remito CrearMora(decimal xImporte, Moneda xMoneda, ClienteActivo xCliente, SubCuenta xSubCuenta, object xMovs, decimal xCotizacion)
        {
            string Serie = _Caja.Mora;
            List<LineaRemito> Lineas = new List<LineaRemito>();
            LineaRemito Linea = new LineaRemito(100000, "MORA",Serie,xMoneda.Codmoneda,22,"INTERESES MORA");
            int Z = GCobros.getInstance().Caja.Z;
            string Caja = GCobros.getInstance().Caja.Id;
            Linea.Unid1 = 1;
            Linea.Unid2 = 1;
            Linea.Unid3 = 1;
            Linea.Unid4 = 1;
            Linea.Unidadestotal = 1;
            Linea.Precio = xImporte / Convert.ToDecimal(1.22);
            Linea.Dto = 0;
            Linea.Precioiva = Linea.Total();
            Linea.Tipoimpuesto = 1;
            Linea.Codtarifa = 10;
            Linea.CodAlmacen = "LB";
            Linea.Precioiva = xImporte;
            Linea.Udsexpansion = 1;
            Linea.Totalexpansion = xImporte;
            Linea.Costeiva = 0;
            Linea.Fechaentrega = DateTime.Today;
            Linea.NumLin = Lineas.Count();
            Lineas.Add(Linea);
            List<Movimiento> L = new List<Movimiento>();
            if (xMovs != null)
            {
                if (xMovs is Movimiento)
                {
                    L = new List<Movimiento>();
                    L.Add((Movimiento)xMovs);
                }
                L = (List<Movimiento>)xMovs;
            }
            SubCuenta SC;
            if (xSubCuenta != null)
                SC = xSubCuenta;
            else
                SC = new SubCuenta(xCliente.IdCliente, 1);
            RMora Mora = new RMora(-1, Serie, DateTime.Today, xMoneda, 2, _Caja.Usuario.CodVendedor, xCliente, Lineas, "Mora por atraso",SC);
            Mora.Movimiento = L;
            Mora.FactorMoneda = xCotizacion;
            Mora.SerieRecibo = _Caja.Recibos;
            return Mora;
        }



        public void AnularRecibo(int xNumero, string xSerie, int xCodCliente, int xMoneda)
        {
            Recibo Recibo = ObtenerReciboByID(xSerie, xNumero, xCodCliente, xMoneda);


            if (Recibo.Importe() <= 0)
                throw new Exception("No se puede anular el recibo");

            List<Movimiento> Anular;
            List<object> Remitos = new List<object>();
            decimal cotizacion = 1;
            Anular = Recibo.BM();
            ClienteActivo Cliente = GClientes.Instance().getByID(xCodCliente.ToString(), false);

            if (Recibo.Moneda == 2)
                cotizacion = _Caja.Cotizacion;

            if (Anular != null && Anular.Count > 0)
            {
                
                foreach (Movimiento M in Anular)
                {
                    switch (M.TipoDoc)
                    {
                        case 21: //Bonificacion
                            Remito Deb = CrearDebito(M.Importe,Recibo.Moneda, (Persona)Cliente, Cliente.SubCuentas.Find(S => S.Codigo == M.SubCta), M, cotizacion);
                            Deb.Recibo = Recibo.Numero;
                            Deb.SerieRecibo = Recibo.Serie;
                            Remitos.Add(Deb);
                            break;
                        case 19: //Mora
                            Remito Bon = CrearBonificacion(Math.Abs(M.Importe),_ListaMonedas.Find(Mon => Mon.Codmoneda == Recibo.Moneda),Cliente, Cliente.SubCuentas.Find(S => S.Codigo == M.SubCta),M,cotizacion,"ANULA DEBITO: " + M.Serie + " - " + M.Numero);
                                Bon.Recibo = Recibo.Numero;
                                Bon.SerieRecibo = Recibo.Serie;
                                Remitos.Add(Bon);
                            break;
                    }
                }
            }
            try
            {
                DBCobros.AnularMovimientos(Recibo.Movimientos(),Remitos,Claves,_Caja,false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #region EntregaCuenta


        public int GenerarEntrega(decimal xImporte, int xMoneda, ClienteActivo xCliente)
        {
            Moneda M = _ListaMonedas.Find(Mon => Mon.Codmoneda == xMoneda);
            List<LineaRemito> Lineas = new List<LineaRemito>();
            LineaRemito Linea = CrearLinea(xImporte, 100003, "ENTREGA", _Caja.EntregaCuenta, "ENTREGA A CUENTA", Lineas.Count + 1, M, 22);
           
            Lineas.Add(Linea);
            decimal Cotizacion = 1;
            EntregaCuenta Entrega;
            if (xMoneda == 2)
                Cotizacion = _Caja.Cotizacion;
            Entrega = new EntregaCuenta(-1,_Caja.EntregaCuenta,DateTime.Now,M,xCliente,Lineas,2,"",_Caja.Usuario.CodVendedor,2,2,xImporte,_Caja.Z,_Caja.Id, _Caja.Recibos,null);
            Entrega.Caja = _Caja.Id;
            Entrega.Z = Caja.Z;
            Entrega.SerieRecibo = _Caja.Recibos;
            Entrega.FactorMoneda = Cotizacion;
            return DBCobros.GenerarEntrega(Entrega,_Caja);
        }


        #endregion


        #region Adjudicacion

        // ----- Adjudicacion -----//

        public void adjudicar(List<MovimientoGeneral> xListaMovimientos)
        {
            if (xListaMovimientos == null)
                return;
            if (xListaMovimientos.Count < 1)
                return;
            if (SumaZero(xListaMovimientos) != 0)
                throw new Exception("Los importes a adjudicar no son validos");
        }

        private decimal SumaZero(List<MovimientoGeneral> xListaMovimientos)
        {
            int zCodMoneda = xListaMovimientos[0].Moneda.Codmoneda;
            decimal zSuma = 0;
            foreach (MovimientoGeneral M in xListaMovimientos)
            {
                if (M.Moneda.Codmoneda != zCodMoneda)
                    throw new Exception("No se puede adjudicar facturas de distintas monedas");
                if (M.ImportePagado != 0)
                    zSuma = M.ImportePagado;
                else
                    zSuma += M.Importe;
            }
            return zSuma;
        }

        public void notifyObservers()
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
