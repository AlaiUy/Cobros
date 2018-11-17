using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguiñagalde.Entidades
{
    public class PersonaAutorizada
    {
        private string _Nombre;
        private string _Direccion;
        private int _Telefono;
        private int _celular;
        private string _email;
        private DateTime _FechaInicial;



        private string _observaciones;
        private int _documento;
        private DateTime _FechaFinal;
        private int _cta;

        public int Cta
        {
            get { return _cta; }
            set { _cta = value; }
        }


        public PersonaAutorizada(string xNombre, string xDireccion, int xDocumento)
        {
            _Nombre = xNombre;
            _Direccion = xDireccion;
            _documento = xDocumento;

        }

        public DateTime FechaInicial
        {
            get { return _FechaInicial; }
            set { _FechaInicial = value; }
        }


        public int Documento
        {
            get { return _documento; }
            set { _documento = value; }
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

        public int Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public DateTime FechaFinal
        {
            get { return _FechaFinal; }
            set { _FechaFinal = value; }
        }


        public string Observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        public int Celular
        {
            get { return _celular; }
            set { _celular = value; }
        }

        public override string ToString()
        {
            return _Nombre + " - " + _documento;
        }
    }
}
