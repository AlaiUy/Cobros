using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aguiñagalde.Entidades;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Threading;
using Microsoft.VisualBasic.Devices;

namespace Aguiñagalde.XMLManager
{
    public class XMLInfo
    {
        private XMLInfo() { }

        private static XMLInfo _Instance = null;

        public static XMLInfo getInstance()
        {
            if (_Instance == null)
                _Instance = new XMLInfo();
            return _Instance;
        }

        public Clearing ObtenerInfoClering(string xPathXML)
        {
            Clearing Temporal = new Clearing();

         
            var Registro = from c in XElement.Load(xPathXML).Elements("consulta_documento")
                        select c;

          
            
            // Execute the query 
            foreach (var DatosPersonales in Registro)
            {
                Temporal.Nombre  = (string)DatosPersonales.Element("nom1");
                Temporal.Apellido = (string)DatosPersonales.Element("ape1");
                Temporal.Snombre = (string)DatosPersonales.Element("nom2");
                Temporal.Documento = (string)DatosPersonales.Element("nro_identificacion");
                Temporal.Segundoapellido = (string)DatosPersonales.Element("ape2");
                Temporal.Civil = (string)(DatosPersonales.Element("estado_civil"));
                Temporal.Nacimiento = (string)DatosPersonales.Element("fecha_nacimiento");
                Temporal.Sexo = (string)DatosPersonales.Element("sexo");
                Temporal.Nacionalidad = (int)DatosPersonales.Element("nacionalidad");
                Temporal.Codigo = (string)DatosPersonales.Element("cod_identificacion");
                Temporal.Cancelaciones = (int)DatosPersonales.Element("resumen").Element("cancelaciones");
                Temporal.Incumplimientos = (int)DatosPersonales.Element("resumen").Element("incumplimientos");
            }


            Registro = from c in XElement.Load(xPathXML).Elements("consulta_incumplimientos").Elements("incumplimiento")
                        select c;


            List<ClearinIncumplimiento> L = new List<ClearinIncumplimiento>();

            


            foreach (var Incumplimientos in Registro)
            {
                ClearinIncumplimiento IC = new ClearinIncumplimiento();
                IC.Compra = (string)Incumplimientos.Element("fecha_compra");
                IC.Empresa = (string)Incumplimientos.Element("empresa");
                IC.Fecha = (string)Incumplimientos.Element("fecha_registro");
                IC.Lugar = (string)Incumplimientos.Element("centro");
                IC.Moneda = (string)Incumplimientos.Element("moneda_saldo");
                IC.Monto = (string)Incumplimientos.Element("monto_saldo");
                L.Add(IC);
            }

            Temporal.DatosIncumplidos1 = L;


            Registro = from c in XElement.Load(xPathXML).Elements("consulta_documento").Elements("domicilio")
                           select c;



            // Execute the query 
            foreach (var Domicilio in Registro)
            {
                Temporal.Calle = (string)Domicilio.Element("direccion");
                Temporal.Ciudad = (string)Domicilio.Element("localidad");
            }



         

            


            return Temporal;
        }

        public void GenerarXMLRemito(Remito xR,int xNumero,Empresa Claves,CajaGeneral xCaja,bool xImprimir)
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
            if(xImprimir)
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
                    MovimientoGeneral M = R.Movimiento[Index];
                    Writer.WriteStartElement("ReferenciaItem");
                    Writer.WriteStartElement("RefNroLinRef");
                    Writer.WriteValue(Index+1);
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


        public bool LeerXMLRetorno(string xNombre,CajaGeneral xCaja)
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
                catch (Exception ex)
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
    }
}
