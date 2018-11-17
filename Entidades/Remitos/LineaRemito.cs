using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguiñagalde.Entidades
{
    public class LineaRemito
    {
        private int _NumLin;
        private int _CodArticulo;
        private string _Referencia;
        private decimal _Cantidad;
        private decimal _Precio;
        private decimal _DTO;
        private int _CodMoneda;
        private string _CodAlmacen;
        private string _Descripcion;
        private string _Serie;
        private int _Numero;
        private char _N;
        private string _Color;
        private string _Talla;
        private decimal _Unid1;
        private decimal _Unid2;
        private decimal _Unid3;
        private decimal _Unid4;
        private decimal _Unidadestotal;
        private decimal _Dto;
        private decimal _Costo;
        private decimal _Preciodefecto;
        private byte _Tipoimpuesto;
        private decimal _Iva;
        private int _Codtarifa;
        private decimal _Precioiva;
        private decimal _Udsexpansion;
        private char _Expandida;
        private decimal _Totalexpansion;
        private decimal _Costeiva;
        private DateTime _Fechaentrega;
        private decimal _Numkgentrega;

        public decimal Numkgentrega
        {
            get { return _Numkgentrega; }
            set { _Numkgentrega = value; }
        }

        public DateTime Fechaentrega
        {
            get { return _Fechaentrega; }
            set { _Fechaentrega = value; }
        }

        public decimal Costeiva
        {
            get { return _Costeiva; }
            set { _Costeiva = value; }
        }

        public decimal Totalexpansion
        {
            get { return _Totalexpansion; }
            set { _Totalexpansion = value; }
        }

        public char Expandida
        {
            get { return _Expandida; }
            set { _Expandida = value; }
        }

        public decimal Udsexpansion
        {
            get { return _Udsexpansion; }
            set { _Udsexpansion = value; }
        }


        public decimal Precioiva
        {
            get { return _Precioiva; }
            set { _Precioiva = value; }
        }

        public int Codtarifa
        {
            get { return _Codtarifa; }
            set { _Codtarifa = value; }
        }

        public decimal Iva
        {
            get { return _Iva; }
            set { _Iva = value; }
        }


        public byte Tipoimpuesto
        {
            get { return _Tipoimpuesto; }
            set { _Tipoimpuesto = value; }
        }


        public decimal Preciodefecto
        {
            get { return _Preciodefecto; }
            set { _Preciodefecto = value; }
        }

        public decimal Costo
        {
            get { return _Costo; }
            set { _Costo = value; }
        }

        public decimal Dto
        {
            get { return _Dto; }
            set { _Dto = value; }
        }

        public decimal Unidadestotal
        {
            get { return _Unidadestotal; }
            set { _Unidadestotal = value; }
        }

        public decimal Unid1
        {
            get { return _Unid1; }
            set { _Unid1 = value; }
        }
        

        public decimal Unid2
        {
            get { return _Unid2; }
            set { _Unid2 = value; }
        }
        

        public decimal Unid3
        {
            get { return _Unid3; }
            set { _Unid3 = value; }
        }
        

        public decimal Unid4
        {
            get { return _Unid4; }
            set { _Unid4 = value; }
        }



        

        

        public LineaRemito(int xCodArticulo,string xReferencia,string xSerie,int xMoneda,decimal xIva)
        {
            _Serie = xSerie;
            _CodArticulo = xCodArticulo;
            _Referencia = xReferencia;
            _N = 'B';
            _Color = ".";
            _Talla = ".";
            _Expandida = 'F';
            _Numkgentrega = 0;
            _Iva = xIva;
        }

        public LineaRemito(int xCodArticulo, string xReferencia,string xSerie,int xMoneda,decimal xIva, string xDescripcion)
        {
            _CodArticulo = xCodArticulo;
            _Referencia = xReferencia;
            _Descripcion = xDescripcion;
            _Serie = xSerie;
            _N = 'B';
            _Color = ".";
            _Talla = ".";
            _Expandida = 'F';
            _Numkgentrega = 0;
            _Iva = xIva;
        }



        public int NumLin
        {
            set { _NumLin = value; }
            get { return _NumLin; }
        }

        public int CodArticulo
        {
            get { return _CodArticulo; }
        }

        public string Referencia
        {
            get { return _Referencia; }
        }

        public decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        public decimal Precio
        {
            get { return _Precio; }
            set { _Precio = value; }
        }

        public decimal Descuento
        {
            get { return _DTO; }
            set { _DTO = value; }
        }

        public int CodMoneda
        {
            get { return _CodMoneda; }
            set { _CodMoneda = value; }
        }

        public string CodAlmacen
        {
            get { return _CodAlmacen; }
            set { _CodAlmacen = value; }
        }

      

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public string Serie
        {
            get { return _Serie; }
            set { _Serie = value; }
        }

        public int Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        public string Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        public char N
        {
            get { return _N; }
            set { _N = value; }
        }


        public string Talla
        {
            get { return _Talla; }
            set { _Talla = value; }
        }


        public decimal Total()
        {
            return _Unidadestotal * (_Precio * (1+ (_Iva / 100)));
        }

        public decimal PrecioDefecto()
        {
            if (this.Total() < 0)
                return this.Total() * -1;
            else
                return this.Total();
        }

        public decimal TotalBruto()
        {
            return _Precio * _Unidadestotal;
        }

        public decimal TotalIva()
        {
            return Total() - (Total() /(1+(_Iva/100)));
        }

        

     


    }
}
