using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguiñagalde.Entidades
{
    public class SubCuenta
    {
        int _idCliente;
        int _Codigo;
        string _Nombre;
        string _Direccion;
        string _Telefono;
        string _celular;
        bool _descatalogado;
        List<PersonaAutorizada> _Autorizados;
        string _tipo;
        string _rut;

        public string Rut
        {
            get { return _rut; }
            set { _rut = value; }
        }




        public string Tipo
        {
            get
            {
                return _tipo;
            }
            set { _tipo = value; }
        }


        public List<PersonaAutorizada> Autorizados
        {
            get { return _Autorizados; }
            set { _Autorizados = value; }
        }

        public SubCuenta(int xIdCliente, int xCodigo)
        {
            _idCliente = xIdCliente;
            _Codigo = xCodigo;
        }

        public bool Descatalogado
        {
            get { return _descatalogado; }
            set { _descatalogado = value; }
        }

        public int IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        public int Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }


        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }

        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        public string Celular
        {
            get { return _celular; }
            set { _celular = value; }
        }

        public override string ToString()
        {
            return _Codigo + " - " + _Nombre;
        }


        public bool Autorizado(int xID)
        {
            foreach (PersonaAutorizada P in _Autorizados)
            {
                if (P.Documento == xID)
                    return true;
            }
            return false;
        }
    }
}
