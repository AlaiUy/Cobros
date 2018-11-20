using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguiñagalde.Entidades
{
    public class Usuario
    {
        private int _codUsuario;
        private string _Nombre;
        private string _NombreUsuario;
        private string _EmailHost;
        private string _PassEmail;
        private int _CodVendedor;
        private string _Email;
        private string _Password;
        private List<Permiso> _Permisos;

        public Usuario(int xCodigo, List<Permiso> xList)
        {
            _codUsuario = xCodigo;
            _Permisos = xList;
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }


        public string PassEmail
        {
            get { return _PassEmail; }
            set { _PassEmail = value; }
        }


        public string EmailHost
        {
            get { return _EmailHost; }
            set { _EmailHost = value; }
        }


        public int CodVendedor
        {
            get { return _CodVendedor; }
            set { _CodVendedor = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public int CodUsuario
        {
            get { return _codUsuario; }
        }

        public List<Permiso> Permisos
        {
            get
            {
                return _Permisos;
            }

            set
            {
                _Permisos = value;
            }
        }

        public string NombreUsuario
        {
            get
            {
                return _NombreUsuario;
            }

            set
            {
                _NombreUsuario = value;
            }
        }

        public bool Permiso(int xPermiso)
        {
            if (_Permisos != null)
                for (int x = 0; x <= _Permisos.Count - 1; x++)
                    if (_Permisos[x].IdPermiso == xPermiso)
                        return true;
            return false;
        }
    }
}
