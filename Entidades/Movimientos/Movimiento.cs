using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguiñagalde.Entidades
{
    public abstract class Movimiento : IComparable, ICloneable
    {

        private string _Serie;
        private int _Numero;
        private DateTime _Fecha;
        private int _Linea;
        private decimal _Cotizacion;
        private decimal _Importe;
        private int _SubCta;
        private Moneda _Moneda;
        private int _TipoDoc;
        private CFE _CFE;

        public string Serie
        {
            get { return _Serie; }

        }





        public int Numero
        {
            get { return _Numero; }

        }

        public DateTime Fecha
        {
            get
            {
                return _Fecha;
            }
        }

        public int Linea
        {
            get
            {
                return _Linea;
            }

            set
            {
                _Linea = value;
            }
        }

        public decimal Cotizacion
        {
            get
            {
                return _Cotizacion;
            }
        }

        public decimal Importe
        {
            get
            {
                return _Importe;
            }
            set
            {
                _Importe = value;
            }
        }

        public int SubCta
        {
            get
            {
                return _SubCta;
            }
        }

        public Moneda Moneda
        {
            get
            {
                return _Moneda;
            }

           
        }

        public int TipoDoc
        {
            get
            {
                return _TipoDoc;
            }
        }

        public CFE CFE
        {
            get
            {
                return _CFE;
            }

            set
            {
                _CFE = value;
            }
        }

        public Movimiento(int xNumero, string xSerie, DateTime xFecha, int xLinea, decimal xCotizacion,decimal xImporte,int xSubCta,Moneda xMoneda,int xNumeroTipo)
        {
            _Numero = xNumero;
            _Serie = xSerie;
            _Fecha = xFecha;
            _Linea = xLinea;
            _Cotizacion = xCotizacion;
            _Importe = xImporte;
            _Moneda = xMoneda;
            _TipoDoc = xNumeroTipo;
            _SubCta = xSubCta;
        }


        public abstract int CompareTo(object obj);
        public abstract object Clone();

        public abstract decimal getDescuento(decimal xDescuento, int xFormaPago, bool KeepGoing);

        public abstract int getDiasVencidos();

        public abstract decimal getMora();
    }
}
