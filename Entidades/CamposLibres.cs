using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguiñagalde.Entidades
{
    public class CamposLibres
    {
        private int _codCliente;
        private string _Actividad;
        private string _Comerciales;
        private string _Antiguedad;
        private string _OtrasObservaciones;
        private string _Plasticos;
        private string _email;
        private string _Vehiculos;
        private string _Bienes;
        private string _Civil;
        private string _Alquiler;
        private int _ConyugeIngresos;
        private string _ConyugeActividad;
        private string _Cargo;
        private int _Ingresos;
        private string _ConyugeCargo;
        private string _ConyugeAntiguedad;
        private string _Conyuge;

        public string Conyuge
        {
            get { return _Conyuge; }
            set { _Conyuge = value; }
        }


        public string ConyugeCargo
        {
            get { return _ConyugeCargo; }
            set { _ConyugeCargo = value; }
        }


        public string ConyugeAntiguedad
        {
            get { return _ConyugeAntiguedad; }
            set { _ConyugeAntiguedad = value; }
        }

        public int Ingresos
        {
            get { return _Ingresos; }
            set { _Ingresos = value; }
        }


        public string Cargo
        {
            get { return _Cargo; }
            set { _Cargo = value; }
        }


        public string ConyugeActividad
        {
            get { return _ConyugeActividad; }
            set { _ConyugeActividad = value; }
        }


        public int ConyugeIngresos
        {
            get { return _ConyugeIngresos; }
            set { _ConyugeIngresos = value; }
        }


        public string Alquiler
        {
            get { return _Alquiler; }
            set { _Alquiler = value; }
        }


        public string Civil
        {
            get { return _Civil; }
            set { _Civil = value; }
        }




        public string Bienes
        {
            get { return _Bienes; }
            set { _Bienes = value; }
        }


        public string Vehiculos
        {
            get { return _Vehiculos; }
            set { _Vehiculos = value; }
        }


        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }


        public string Plasticos
        {
            get { return _Plasticos; }
            set { _Plasticos = value; }
        }


        public string OtrasObservaciones
        {
            get { return _OtrasObservaciones; }
            set { _OtrasObservaciones = value; }
        }


        public string Comerciales
        {
            get { return _Comerciales; }
            set { _Comerciales = value; }
        }


        public string Antiguedad
        {
            get { return _Antiguedad; }
            set { _Antiguedad = value; }
        }


        public string Actividad
        {
            get { return _Actividad; }
            set { _Actividad = value; }
        }

        public CamposLibres(int xCodCliente)
        {
            _codCliente = xCodCliente;
        }
    }
}
