using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguiñagalde.Entidades
{
    public class Empresa
    {
        private string _EmpPK;

        public string EmpPK
        {
            get { return _EmpPK; }
        }

        private int _Sucursal;

        public int Sucursal
        {
            get { return _Sucursal; }

        }
        private string _Clave;

        public string Clave
        {
            get { return _Clave; }
        }
        private int _CodEmpresa;

        public int CodEmpresa
        {
            get { return _CodEmpresa; }
        }

        public Empresa(int xSucursal, string xClave, int xCodEmpresa)
        {
            _Sucursal = xSucursal;
            _Clave = xClave;
            _CodEmpresa = xCodEmpresa;
            _EmpPK = "Z0Zk3HZokB+N2w28HkJdRw==";
        }
    }
}
