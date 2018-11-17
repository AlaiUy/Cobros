using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguiñagalde.Entidades
{
    public abstract class Persona
    {
        private int _IdCliente;
        private string _Nombre;
        private string _NombreComercial;
        private decimal _Lineacredito;
        private string _Cedula;
        private string _Rut;
        private int _Type;

        private byte _Cierre;
        private string _Direccion;
        private string _DireccionAlternativa;
        private string _Telefono;
        private string _Celular;
        private string _Pais;
        private string _Dpto;
        private string _Postal;
        private DateTime _Fecha;
        private bool _Descatalogado;
        private string _Ruta;
        private FPago _FormaPago;
        private Tarifa _Tarifa;
        private Moneda _Moneda;
        private string _Cobrador;

        public int IdCliente
        {
            get
            {
                return _IdCliente;
            }
        }

        public string Nombre
        {
            get
            {
                return _Nombre;
            }
            set
            {
                _Nombre = value;
            }
        }

        public string NombreComercial
        {
            get
            {
                return _NombreComercial;
            }

            set
            {
                _NombreComercial = value;
            }
        }

        public decimal Lineacredito
        {
            get
            {
                return _Lineacredito;
            }

            set
            {
                _Lineacredito = value;
            }
        }

        public string Rut
        {
            get
            {
                return _Rut;
            }

            set
            {
                _Rut = value;
            }
        }

        public int Type
        {
            get
            {
                return _Type;
            }

            set
            {
                _Type = value;
            }
        }

        public string Direccion
        {
            get
            {
                return _Direccion;
            }

            set
            {
                _Direccion = value;
            }
        }

        public string Pais
        {
            get
            {
                return _Pais;
            }

            set
            {
                _Pais = value;
            }
        }

        public string Postal
        {
            get
            {
                return _Postal;
            }

            set
            {
                _Postal = value;
            }
        }

        public string Telefono
        {
            get
            {
                return _Telefono;
            }

            set
            {
                _Telefono = value;
            }
        }

        public string Dpto
        {
            get
            {
                return _Dpto;
            }

            set
            {
                _Dpto = value;
            }
        }

        public string DireccionAlternativa
        {
            get
            {
                return _DireccionAlternativa;
            }

            set
            {
                _DireccionAlternativa = value;
            }
        }

        public byte Cierre
        {
            get
            {
                return _Cierre;
            }

            set
            {
                _Cierre = value;
            }
        }

        public string Celular
        {
            get
            {
                return _Celular;
            }

            set
            {
                _Celular = value;
            }
        }

        public bool Descatalogado
        {
            get
            {
                return _Descatalogado;
            }

            set
            {
                _Descatalogado = value;
            }
        }

        public DateTime Fecha
        {
            get
            {
                return _Fecha;
            }

            set
            {
                _Fecha = value;
            }
        }

        public string Ruta
        {
            get
            {
                return _Ruta;
            }

            set
            {
                _Ruta = value;
            }
        }

        public FPago FormaPago
        {
            get
            {
                return _FormaPago;
            }

            set
            {
                _FormaPago = value;
            }
        }

        public Tarifa Tarifa
        {
            get
            {
                return _Tarifa;
            }

            set
            {
                _Tarifa = value;
            }
        }

        public Moneda Moneda
        {
            get
            {
                return _Moneda;
            }

            set
            {
                _Moneda = value;
            }
        }

        public string Cobrador
        {
            get
            {
                return _Cobrador;
            }

            set
            {
                _Cobrador = value;
            }
        }

        public string Cedula
        {
            get
            {
                return _Cedula;
            }

            set
            {
                _Cedula = value;
            }
        }

        public Persona(int xIdCliente, string xNombre, string xCedula)
        {
            _Cedula = xCedula;
            _IdCliente = xIdCliente;
            _Nombre = xNombre;
        }
    }
}
