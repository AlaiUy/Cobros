using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguiñagalde.Entidades
{
    public class EntregaCuenta : Remito
    {
        private decimal _Importe;


       

        public EntregaCuenta(int xNumero,string xSerie, DateTime xFecha, Moneda xMoneda, ClienteActivo xCliente, List<LineaRemito> xLineas, int xCodTarifa, string xComentario, int xCodVendedor, int xCodFormaPago, int xTipoPago,decimal xImporte,int xZ,string xCaja,string xSerieRecibo,SubCuenta xSubcuenta) : base(xNumero,xSerie, xFecha, xMoneda, xCliente, xLineas, xCodTarifa, xComentario, xCodVendedor,xSubcuenta)
        {
            _Importe = xImporte;
        }



        public override string Adenda()
        {
            return "Entrega a cuenta";
        }

        public override bool CFE()
        {
            return false;
        }

        public override decimal Costo()
        {
            return 0;
        }

     

        public override decimal Importe()
        {
            return _Importe;
        }

        public override int NumeroCFE()
        {
            return 0;
        }

        public override int TipoDoc()
        {
            return 62;
        }

        public override int FormaPago()
        {
            if (base.Recibo > 0)
                if (Moneda.Codmoneda == 1)
                    return 1;
                else
                    return 3;
            return 2;
        }

        public override int TipoPago()
        {
            if (base.Recibo > 0)
                return 1;
            return 7;
        }


        public override decimal TotalBruto()
        {
            return 0;
        }

        public override decimal TotalCostoIva()
        {
            return 0;
        }

        public override decimal TotalImpuestos()
        {
            return 0;
        }

        public override decimal TotalIva()
        {
            return 0;
        }

        public override char Estado()
        {
            if (base.Recibo > 0)
                return 'S';
            return 'P';
        }

        public override string GenApunte()
        {
            if (Recibo > 0)
                return "SALDADO (F/F)";
            return "VENCIMIENTO";
        }

        public override int Remesa()
        {
            if (Recibo > 0)
                return Recibo;
            return -1;
        }

        public override int NumeroZ()
        {
            if (Recibo > 0)
                return base.Z;
            return 0;
        }

        public override string SerieCaja()
        {
            if (Recibo > 0)
                return base.Caja;
            return string.Empty;
        }

        public override string Sudocumento()
        {
            if (Recibo > 0)
                return base.SerieRecibo;
            return string.Empty;
        }
    }
}
