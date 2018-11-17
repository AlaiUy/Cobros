using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguiñagalde.Entidades
{
    public class CFE
    {
        private int _Tipo;
        private string _Serie;
        private int _Numero;
        private string _Link;
        private string _SerieAlbaran;
        private int _NumeroAlbaran;
        private string _SerieFactura;
        private int _NumeroFactura;

        public CFE(int xTipo, string xSerie, int xNumero, string xLink, string xSerieAlbaran, int xNumeroAlbaran, string xSerieFactura, int xNumeroFactura)
        {
            _Tipo = xTipo;
            _Serie = xSerie;
            _Numero = xNumero;
            _Link = xLink;
            _SerieAlbaran = xSerieAlbaran;
            _NumeroAlbaran = xNumeroAlbaran;
            _SerieFactura = xSerieFactura;
            _NumeroFactura = xNumeroFactura;
        }

        public int Tipo
        {
            get { return _Tipo; }

        }


        public string Serie
        {
            get { return _Serie; }

        }



        public int Numero
        {
            get { return _Numero; }

        }


        public string Link
        {
            get { return _Link; }

        }


        public string SerieAlbaran
        {
            get { return _SerieAlbaran; }

        }


        public int NumeroAlbaran
        {
            get { return _NumeroAlbaran; }

        }


        public string SerieFactura
        {
            get { return _SerieFactura; }

        }


        public int NumeroFactura
        {
            get { return _NumeroFactura; }

        }
    }
}
