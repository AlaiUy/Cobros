using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguiñagalde.Entidades
{
    public abstract class Caja
    {
        private int _Z;
        private decimal _Cotizacion;
        private Usuario _Usuario;
        private List<Config> _Configs;
        private string _Id;








        public Usuario Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        public decimal Cotizacion
        {
            get { return _Cotizacion; }

        }

        public int Z
        {
            get { return _Z; }
            set { _Z = value; }

        }

        public List<Config> Config
        {
            get
            {
                return _Configs;
            }

            set
            {
                _Configs = value;
            }
        }

        public string Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public Caja(List<Config> xConfigs, int xNumeroZ, decimal xCotizacion)
        {
            _Id = xConfigs.Find(Config => Config.ID == 10).Dato;
            _Z = xNumeroZ;
            _Cotizacion = xCotizacion;
            _Configs = xConfigs;
        }

        public Caja(string xSerieCaja, int xNumeroZ, decimal xCotizacion)
        {
            _Id = xSerieCaja;
            _Z = xNumeroZ;
            _Cotizacion = xCotizacion;
        }

    }
}
