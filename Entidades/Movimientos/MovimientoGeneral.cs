using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguiñagalde.Entidades
{
    public class MovimientoGeneral : Movimiento,IEnumerable
    {
        private string _descripcion;
        private int _tipocliente;
        private string _estado;
        private string _origen;
        private byte _Posicion;

      
        private int _codcliente;
        private int _TipoPago;
        private int _FormaPago;
        private DateTime _fechasaldado;
        private decimal _factormoneda;
        private int _zsaldado;
        private string _cajasaldado;
        private decimal _ImportePagado;
        private string _genApunte;
        private DateTime _FechaVencimiento;
        private string _tipodocumento;
        private int _numeroremesa;
        private decimal _mora;
        private string _sudocumento;
        private int _NumeroDoc;
        private string _SerieDoc;
        private int _codTarifa;
        private DateTime _VencimientoContado;
 

        #region Propiedades

        public int TipoPago
        {
            get { return _TipoPago; }
            set { _TipoPago = value; }
        }



        public int FormaPago
        {
            get { return _FormaPago; }
            set { _FormaPago = value; }
        }


        public int Codcliente
        {
            get { return _codcliente; }
            set { _codcliente = value; }
        }

     
        

        public string SerieDoc
        {
            get { return _SerieDoc; }
            set { _SerieDoc = value; }
        }

        public int NumeroDoc
        {
            get { return _NumeroDoc; }
            set { _NumeroDoc = value; }
        }



    

        public string Sudocumento
        {
            get { return _sudocumento; }
            set { _sudocumento = value; }
        }


        public decimal Mora
        {
            get { return _mora; }
            set { _mora = value; }
        }



        public string Origen
        {
            get { return _origen; }
        }
        public byte Posicion
        {
            get { return _Posicion; }
            set { _Posicion = value; }

        }
        public string Cajasaldado
        {
            get { return _cajasaldado; }
            set { _cajasaldado = value; }
        }
        public int Zsaldado
        {
            get { return _zsaldado; }
            set { _zsaldado = value; }
        }
        public decimal Factormoneda
        {
            get { return _factormoneda; }
            set { _factormoneda = value; }
        }
        public decimal ImportePagado
        {
            get { return _ImportePagado; }
            set { _ImportePagado = value; }
        }
        public DateTime Saldado
        {
            get { return _fechasaldado; }
            set { _fechasaldado = value; }
        }
        public DateTime FechaVencimiento
        {
            get { return _FechaVencimiento; }
            set { _FechaVencimiento = value; }
        }
        public string Tipodocumento
        {
            get { return _tipodocumento; }
            set { _tipodocumento = value; }
        }
        public int Numeroremesa
        {
            get { return _numeroremesa; }
            set { _numeroremesa = value; }
        }
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        public string GenApunte
        {
            get { return _genApunte; }
            set { _genApunte = value; }
        }

        public int Tarifa
        {
            get
            {
                return _codTarifa;
            }

            //set
            //{
            //    _codTarifa = value;
            //}
        }

        public DateTime VencimientoContado
        {
            get
            {
                return _VencimientoContado;
            }

            set
            {
                _VencimientoContado = value;
            }
        }

        public int Tipocliente
        {
            get
            {
                return _tipocliente;
            }

            set
            {
                _tipocliente = value;
            }
        }

      
        #endregion

        #region Constructores

        public MovimientoGeneral(int xNumero, string xSerie, string xDescipcion, decimal xImporte, DateTime xFecha, Moneda xMoneda, byte xLinea, string xOrigen, int xTarifa, decimal xCotizacion,int xSubcta,int xTipoDoc)
            : base(xNumero, xSerie, xFecha, xLinea, xCotizacion,xImporte,xSubcta,xMoneda,xTipoDoc)
        {

            _descripcion = xDescipcion;
            _Posicion = xLinea;
            _origen = xOrigen;
            _codTarifa = xTarifa;


        }

        //public MovimientoGeneral(int xNumero, string xSerie, string xDescipcion, decimal xImporte, DateTime xFecha, Moneda xMoneda, byte xLinea, string xOrigen, decimal xCotizacion)
        //    : base(xNumero, xSerie, xFecha, xLinea, xCotizacion)
        //{
        //    _descripcion = xDescipcion;
        //    _Moneda = xMoneda;
        //    _Posicion = xLinea;
        //    _origen = xOrigen;
        //    _importe = xImporte;

        //}


        #endregion


        #region Metodos

        public override decimal getMora()
        {

            if (this._estado == "S")
                return 0;

            if(TipoDoc  == 62 || TipoDoc == 18)
                return 0;

            if (this.Importe < 0)
                return 0;


            if (_tipocliente == 9)
                return 0;


            if (this._FechaVencimiento > DateTime.Today)
            {
                return 0;
            }
            if (this.ImportePagado != 0)
            {
                decimal MT = MoraTotal(getDiasVencidos(), Moneda.Mora, Importe);
                //decimal Prorrateo = MT / ((Importe + MT) / this.ImportePagado);
                return decimal.Round(MT / ((Importe + MT) / this.ImportePagado), 2);
            }

            return decimal.Round(MoraTotal(getDiasVencidos(), Moneda.Mora, Importe), 2);
        }

        public static decimal MoraTotal(int xDiasVencidos, decimal xCoefMora, decimal xImporte)
        {
            return (xCoefMora / 360) * xDiasVencidos * xImporte;
        }

        public static decimal MoraParcial(int xDiasVencidos, decimal xCoefMora, decimal xImporte,decimal xImportePagado)
        {
            decimal MT = MoraTotal(xDiasVencidos, xCoefMora, xImporte);
            return MT / ((xImporte + MT) / xImportePagado);
        }


        //public static decimal mMora(int xDias, decimal xImporte, decimal xMonto, Moneda xM)
        //{


        //    if (xImporte < 0)
        //        return 0;

        //    if (xMonto != 0)
        //    {
        //        decimal MT = MoraTotal(xDias, xM.Mora, xImporte);
        //        decimal Prorrateo = MT / ((xImporte + MT) / xMonto);
        //        return decimal.Round(MT / ((xImporte + MT) / xMonto), 2);
        //    }
        //    return decimal.Round(MoraTotal(xDias, xM.Mora, xImporte), 2);
        //}




        public static decimal Descuento(int xDias, decimal xImporte, decimal xMonto, decimal xDescuento, DateTime xFechaContado,int xFormaPago,bool KeepGoing)
        {
            if (xDias > 0)
                return 0;
            
            if(!KeepGoing)
                if (xFechaContado < DateTime.Today)
                    return 0;

            decimal x = Math.Truncate(xDescuento) /100;

            if ((xDescuento * 100) > 130)
                xDescuento = Convert.ToDecimal(125 / 100);

            if (xMonto == (xImporte - Math.Abs(DescuentoTotal(xImporte, xDescuento,xFormaPago))))
                return Math.Abs(DescuentoTotal(xImporte, xDescuento, xFormaPago)) * -1;

            if (xMonto == 0 && xImporte > 0)
                return Math.Abs(DescuentoTotal(xImporte, xDescuento, xFormaPago)) * -1;

            if (xMonto > 0)
            {
                decimal DT = Math.Abs(DescuentoTotal(xImporte, xDescuento, xFormaPago));
                decimal Coef = xMonto / (xImporte - DT);
                return Math.Abs(DT * Coef) * -1;
            }
            return 0;
        }



        public override decimal getDescuento(decimal xDescuento,int xFormaPago,bool KeepGoing)
        {

            if (xDescuento < 1)
                return 0;

            if (this._estado == "S")
                return 0;

            if (_codTarifa == 1)
                return 0;

            if (_tipocliente == 9)
                return 0;

            if (_codTarifa == 1)
                return 0;

            if (TipoDoc == 62 || TipoDoc == 18)
                return 0;

                if (this._FechaVencimiento < DateTime.Today)
                return 0;

            if (!KeepGoing)
                if (VencimientoContado < DateTime.Today)
                return 0;

            

            return Descuento(getDiasVencidos(), Importe, _ImportePagado, xDescuento, _VencimientoContado, xFormaPago, KeepGoing);
        }

        private static decimal DescuentoTotal(decimal xImporte, decimal xDescuento,int xFormaPago)
        {
            switch (xFormaPago)
            {
                
                case 1:
                    return xImporte - (xImporte / (decimal)1.25);
                case 2:
                    return xImporte - (xImporte / (decimal)1.25);
                case 3:
                    return xImporte - (xImporte / (decimal)1.20);
                default:
                    return xImporte - (xImporte / (decimal)xDescuento);
            }
            
        }

        public override int getDiasVencidos()
        {
            TimeSpan TS;
            TS = DateTime.Today - this._FechaVencimiento;
            if (TS.Days > 0)
            {
                return Math.Abs(TS.Days);
            }
            return 0;
        }

        private int getDiasFacturados()
        {
            TimeSpan TS;
            TS = DateTime.Today - this.Fecha;
            if (TS.Days > 0)
            {
                return Math.Abs(TS.Days);
            }
            return 0;
        }


        #endregion


        public override int CompareTo(object obj)
        {
            if (obj == null)
                return 0;
            MovimientoGeneral I = (MovimientoGeneral)obj;
            if (I.Fecha < Fecha)
                return 1;
            else if (I.Fecha > Fecha)
                return -1;
            else
                return 0;
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }


}

