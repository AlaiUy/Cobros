using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguiñagalde.Entidades
{
    public class CajaGeneral : Caja
    {
        private string _EntradaCFE;
        private string _SalidaCFE;
        private string _BackCFE;
        private string _DescuentoInco;
        private bool _imprimir;
        private byte _Sucursal;
        private bool _ModificaDescuento;
        private int _PlazoDescuento;
        private decimal _NumeroDescuento;
        private string _EntregaCuenta;
        private string _Impresora;
        private string _SerieDescuento;
        private string _Recibos;
        private string _Mora;
        private string _Debito;
        private string _TemporalCFE;

        public string Recibos
        {
            get { return _Recibos; }

        }


        public string Mora
        {
            get { return _Mora; }

        }



        public string SerieDescuento
        {
            get { return _SerieDescuento; }
        }



        public string Impresora
        {
            get { return _Impresora; }
            set { _Impresora = value; }
        }



        public string EntregaCuenta
        {
            get { return _EntregaCuenta; }
            set { _EntregaCuenta = value; }
        }

        public string EntradaCFE
        {
            get
            {
                return _EntradaCFE;
            }
            set { _EntradaCFE = value; }


        }

        public string SalidaCFE
        {
            get
            {
                return _SalidaCFE;
            }
            set { _SalidaCFE = value; }

        }

        public string BackCFE
        {
            get
            {
                return _BackCFE;
            }

            set
            {
                _BackCFE = value;
            }
        }

        public string DescuentoInco
        {
            get
            {
                return _DescuentoInco;
            }


        }

        public bool Imprimir
        {
            get
            {
                return _imprimir;
            }

            set
            {
                _imprimir = value;
            }
        }

        public byte Sucursal
        {
            get
            {
                return _Sucursal;
            }

            set
            {
                _Sucursal = value;
            }
        }

        public bool ModificaDescuento
        {
            get
            {
                return _ModificaDescuento;
            }

            set
            {
                _ModificaDescuento = value;
            }
        }

        public int PlazoDescuento
        {
            get
            {
                return _PlazoDescuento;
            }

            set
            {
                _PlazoDescuento = value;
            }
        }

        public decimal NumeroDescuento
        {
            get
            {
                return _NumeroDescuento;
            }

            set
            {
                _NumeroDescuento = value;
            }
        }

        public string Debito
        {
            get
            {
                return _Debito;
            }

            set
            {
                _Debito = value;
            }
        }

        public string TemporalCFE
        {
            get
            {
                return _TemporalCFE;
            }

            set
            {
                _TemporalCFE = value;
            }
        }

        public CajaGeneral(List<Config> xConfigs, int xNumeroZ, decimal xCotizacion, List<Serie> xSeries)
            : base(xConfigs, xNumeroZ, xCotizacion)
        {

            
            _imprimir = getDBoolean(BuscarIndex(xConfigs, 53));
            _Sucursal = Convert.ToByte(BuscarIndex(xConfigs, 46));
            _ModificaDescuento = getDBoolean(BuscarIndex(xConfigs, 56));
            _PlazoDescuento = Convert.ToInt32(BuscarIndex(xConfigs, 55));
            _NumeroDescuento = Convert.ToDecimal(BuscarIndex(xConfigs, 57));
            _Mora = BuscarIndex(xSeries, 19).Trim();
            _Recibos = BuscarIndex(xSeries, 20).Trim();
            _SerieDescuento = BuscarIndex(xSeries, 21).Trim();
            _EntregaCuenta = BuscarIndex(xSeries, 62).Trim();
            _DescuentoInco = BuscarIndex(xSeries, 22).Trim();
            _Debito = BuscarIndex(xSeries, 23).Trim();
            _EntradaCFE = BuscarIndex(xConfigs, 44).Trim();
            _SalidaCFE = BuscarIndex(xConfigs, 45).Trim();
            _Impresora = BuscarIndex(xConfigs, 43).Trim();
            _BackCFE = BuscarIndex(xConfigs, 54).Trim();
            _TemporalCFE = BuscarIndex(xConfigs, 66).Trim();

        }


        private string BuscarIndex(List<Serie> Lista, int xId)
        {
            if (Lista.Exists(x => x.ID == xId))
                return Lista.Find(x => x.ID == xId).NroSerie; 
            return string.Empty;
        }

        private string BuscarIndex(List<Config> Lista, int xId)
        {
            foreach (Config S in Lista)
            {
                if (S.ID == xId)
                    return S.Dato;
            }
            return string.Empty;
        }
        private bool getDBoolean(object Value)
        {
            string V = (string)Value;
            if (V == "T")
                return true;
            return false;
        }
    }
}
