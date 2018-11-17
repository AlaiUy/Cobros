using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguiñagalde.Entidades
{
    public class MovimientoRecibo : Movimiento, IEnumerable
    {

       

        public MovimientoRecibo(int xNumero, string xSerie, DateTime xFecha, int xLinea, decimal xCotizacion,decimal xImporte,int xSubCta,Moneda xCodMoneda,int xTipo) : base(xNumero, xSerie, xFecha, xLinea, xCotizacion,xImporte,xSubCta,xCodMoneda,xTipo)
        {
            
        }

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

        public override decimal getDescuento(decimal xDescuento, int xFormaPago, bool KeepGoing)
        {
            throw new NotImplementedException();
        }

        public override int getDiasVencidos()
        {
            throw new NotImplementedException();
        }

        public override decimal getMora()
        {
            throw new NotImplementedException();
        }
    }
}
