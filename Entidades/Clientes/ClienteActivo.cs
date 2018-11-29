using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;




namespace Aguiñagalde.Entidades
{
    [DataContract]
    public class ClienteActivo : Persona, ICloneable
    {
        private string _Email;
        private decimal _tope;
        private DateTime _Apertura;
        private string _fax;
        private List<PersonaAutorizada> _Autorizadas;
        private string _Observaciones;
        private CamposLibres _CamposLibres;
        private List<SubCuenta> _SubCuentas;
        private bool _MonedaUnica;
        private bool _Activo;
        private bool _Orden;
        private bool _Fidelizado;
        private bool _ActivoDia;
        private bool _DIC;
        private DateTime _FecUltimoPago;
        private DateTime _FecUltimaCompra;
        private bool _Interno; // para saber si solo fue un cliente cdia.
        private bool _SoloTitular;
        private bool _Cerrada;






        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }








        public bool SoloTitular
        {
            get { return _SoloTitular; }
            set { _SoloTitular = value; }
        }




        public bool Interno
        {
            get { return _Interno; }
            set { _Interno = value; }
        }

        public DateTime FecUltimaCompra
        {
            get { return _FecUltimaCompra; }
            set { _FecUltimaCompra = value; }
        }

        public DateTime FecUltimoPago
        {
            get { return _FecUltimoPago; }
            set { _FecUltimoPago = value; }
        }
        private bool _isBloqueo;

        public bool IsBloqueo
        {
            get { return _isBloqueo; }
            set { _isBloqueo = value; }
        }


        public DateTime UltimaCompra
        {
            get { return _FecUltimaCompra; }
            set { _FecUltimaCompra = value; }
        }


        public bool DIC
        {
            get { return _DIC; }
            set { _DIC = value; }
        }


        public bool isActivoDia
        {
            get { return _ActivoDia; }
            set { _ActivoDia = value; }
        }


        public bool isFidelizado
        {
            get { return _Fidelizado; }
            set { _Fidelizado = value; }
        }



        public bool isOrden
        {
            get { return _Orden; }
            set { _Orden = value; }
        }


        public bool isActivo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }



        public bool isMonedaUnica
        {
            get { return _MonedaUnica; }
            set { _MonedaUnica = value; }
        }










        #region Propiedades

        public List<SubCuenta> SubCuentas
        {
            get { return _SubCuentas; }
            set { _SubCuentas = value; }
        }




        public List<PersonaAutorizada> Autorizadas
        {
            get
            {
                if (_Autorizadas == null)
                {
                    _Autorizadas = this.MisAutorizados();
                }
                return _Autorizadas;
            }
        }




        public string Tipo()
        {
            switch (Type)
            {
                case 1:
                    return "Cuenta Corriente";
                case 2:
                    return "Moroso";
                case 4:
                    return "Credito Dia";
                case 5:
                    return "Pre Moroso";
                case 7:
                    return "Incobrable";
                case 9:
                    return "Empleado";
                default:
                    return "Cliente no catalogado";
            }
        }


        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }




        public DateTime Apertura
        {
            get { return _Apertura; }
            set { _Apertura = value; }
        }








        public CamposLibres CamposLibres
        {
            get { return _CamposLibres; }
        }







        public decimal Tope
        {
            get { return _tope; }
            set { _tope = value; }
        }




















        public string Observaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
        }

        public bool Cerrada
        {
            get
            {
                return _Cerrada;
            }

            set
            {
                _Cerrada = value;
            }
        }



        #endregion

        #region Constructores


        public ClienteActivo(int xIdCliente, string xNombre, string xCedula) : base(xIdCliente, xNombre, xCedula)
        {
            _CamposLibres = new CamposLibres(xIdCliente);
            _SubCuentas = new List<SubCuenta>();

        }

        public ClienteActivo(int ID, string nombre, Entidades.CamposLibres xCP, string xCedula) : base(ID, nombre, xCedula)
        {
            this._CamposLibres = xCP;
            _SubCuentas = new List<SubCuenta>();

        }

        public string Documento(int xSubcuenta)
        {
            foreach (SubCuenta IS in _SubCuentas)
            {
                if (IS.Codigo == xSubcuenta)
                {
                    if (IS.Rut.Length == 12)
                        return IS.Rut;
                }
            }
            if (VerificaCedula(this.Cedula))
                return this.Cedula.ToString();

            return IdCliente.ToString();
        }

        public int TipoDocumento(int xSubCuenta)
        {
            foreach (SubCuenta S in _SubCuentas)
            {
                if (S.Codigo == xSubCuenta)
                {
                    if (S.Rut.Length == 12)
                        return 2;
                }
            }

            if (this.Cedula.ToString().Length > 6 && this.Cedula.ToString().Length < 9)
            {
                if (VerificaCedula(this.Cedula))
                    return 3;
            }

            return 4;
        }

        #endregion


        #region Funciones

        public List<PersonaAutorizada> MisAutorizados()
        {
            List<PersonaAutorizada> Lista = new List<PersonaAutorizada>();
            foreach (SubCuenta S in this.SubCuentas)
            {
                foreach (PersonaAutorizada P in S.Autorizados)
                {
                    Lista.Add(P);
                }
            }
            return Lista.ToList();
        }

        public int TipoDocumento()
        {
            int zNumero = -1;

            if (this.Rut.Length == 12)
                return 2;

            if (this.IdCliente > 6 && this.IdCliente < 9)
                return 3;

            return zNumero;
        }

        private bool VerificaCedula(string pNumero)
        {
            //Control inicial sobre la cantidad de números ingresados. 
            if (pNumero.ToString().Length > 5 && pNumero.ToString().Length < 9)
            {

                int[] _formula = { 2, 9, 8, 7, 6, 3, 4 };
                int _suma = 0;
                int _guion = 0;
                int _aux = 0;
                int[] _numero = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };

                if (pNumero.ToString().Length == 8)
                {
                    _numero[0] = Convert.ToInt32(pNumero.ToString()[0].ToString());
                    _numero[1] = Convert.ToInt32(pNumero.ToString()[1].ToString());
                    _numero[2] = Convert.ToInt32(pNumero.ToString()[2].ToString());
                    _numero[3] = Convert.ToInt32(pNumero.ToString()[3].ToString());
                    _numero[4] = Convert.ToInt32(pNumero.ToString()[4].ToString());
                    _numero[5] = Convert.ToInt32(pNumero.ToString()[5].ToString());
                    _numero[6] = Convert.ToInt32(pNumero.ToString()[6].ToString());
                    _numero[7] = Convert.ToInt32(pNumero.ToString()[7].ToString());
                }

                //Para cédulas menores a un millón. 
                if (pNumero.ToString().Length == 7)
                {
                    _numero[0] = 0;
                    _numero[1] = Convert.ToInt32(pNumero.ToString()[0].ToString());
                    _numero[2] = Convert.ToInt32(pNumero.ToString()[1].ToString());
                    _numero[3] = Convert.ToInt32(pNumero.ToString()[2].ToString());
                    _numero[4] = Convert.ToInt32(pNumero.ToString()[3].ToString());
                    _numero[5] = Convert.ToInt32(pNumero.ToString()[4].ToString());
                    _numero[6] = Convert.ToInt32(pNumero.ToString()[5].ToString());
                    _numero[7] = Convert.ToInt32(pNumero.ToString()[6].ToString());
                }

                if (pNumero.ToString().Length == 6)
                {
                    _numero[0] = 0;
                    _numero[1] = 0;
                    _numero[2] = Convert.ToInt32(pNumero.ToString()[0].ToString());
                    _numero[3] = Convert.ToInt32(pNumero.ToString()[1].ToString());
                    _numero[4] = Convert.ToInt32(pNumero.ToString()[2].ToString());
                    _numero[5] = Convert.ToInt32(pNumero.ToString()[3].ToString());
                    _numero[6] = Convert.ToInt32(pNumero.ToString()[4].ToString());
                    _numero[7] = Convert.ToInt32(pNumero.ToString()[5].ToString());

                }

                _suma = (_numero[0] * _formula[0]) + (_numero[1] * _formula[1]) + (_numero[2] * _formula[2]) + (_numero[3] * _formula[3]) + (_numero[4] * _formula[4]) + (_numero[5] * _formula[5]) + (_numero[6] * _formula[6]);

                for (int i = 0; i < 10; i++)
                {
                    _aux = _suma + i;
                    if (_aux % 10 == 0)
                    {
                        _guion = _aux - _suma;
                        i = 10;
                    }
                }

                if (_numero[7] == _guion)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return false;

        }

        public string NombreSubCuenta(int xSubCuenta)
        {
            foreach (SubCuenta IS in _SubCuentas)
            {
                if (IS.Codigo == xSubCuenta)
                {
                    return IS.Nombre;

                }
            }
            return this.Nombre;
        }

        public string DireccionSubCuenta(int xSubCuenta)
        {
            foreach (SubCuenta IS in _SubCuentas)
            {
                if (IS.Codigo == xSubCuenta)
                {
                    return IS.Direccion;

                }
            }
            return this.Direccion;
        }

        public string RutSubCuenta(int xSubCuenta)
        {
            foreach (SubCuenta IS in _SubCuentas)
            {
                if (IS.Codigo == xSubCuenta)
                {
                    return IS.Rut;

                }
            }
            return Rut;
        }


        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public string CodContable()
        {
            if (IdCliente.ToString().Length == 5)
                return "122" + IdCliente.ToString();
            return IdCliente.ToString();
        }
    }
}
