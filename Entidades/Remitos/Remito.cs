using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguiñagalde.Entidades
{
    public abstract class Remito
    {
        private int _numero;
        List<Movimiento> _Movimiento;
        private ClienteActivo _Cliente;
        private List<LineaRemito> _Lineas;
        private Moneda _Moneda;
        private string _serie;
        private DateTime _fecha;
        private int _codTarifa;
        private SubCuenta _IS;
        decimal _FactorMoneda;
        int _CodVendedor;
        private string _Comentario;
        private int _tipodoc;
        private int _Recibo;
        private int _Z;
        private string _Caja;
        private string _SerieRecibo;




        public string Comentario
        {
            get { return _Comentario; }
        }




        public List<LineaRemito> Lineas
        {
            get { return _Lineas; }
            set { _Lineas = value; }
        }


        public int CodVendedor
        {
            get { return _CodVendedor; }
            //set { _CodVendedor = value; }
        }

        public decimal FactorMoneda
        {
            get {
                if (_FactorMoneda < 1)
                    return 1;
                return _FactorMoneda;
            }
            set { _FactorMoneda = value; }
        }


        public SubCuenta IS
        {
            get { return _IS; }
            
        }

        public int Tarifa
        {
            get { return _codTarifa; }
            //set { _codTarifa = value; }
        }




        public List<Movimiento> Movimiento
        {
            get { return _Movimiento; }
            set { _Movimiento = value; }
        }

        public ClienteActivo Cliente
        {
            get { return _Cliente; }
            //set { _Cliente = value; }
        }

        public Moneda Moneda
        {
            get { return _Moneda; }
            //set { _Moneda = value; }
        }


        public int Numero
        {
            get { return _numero; }
           set { _numero = value; }
        }
        

        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }


        public DateTime Fecha
        {
            get { return _fecha; }
            // set { _fecha = value; }
        }

        public int Recibo
        {
            get
            {
                return _Recibo;
            }

            set
            {
                _Recibo = value;
            }
        }

        public int Z
        {
            get
            {
                return _Z;
            }

            set
            {
                _Z = value;
            }
        }

        public string Caja
        {
            get
            {
                return _Caja;
            }

            set
            {
                _Caja = value;
            }
        }

        public string SerieRecibo
        {
            get
            {
                return _SerieRecibo;
            }

            set
            {
                _SerieRecibo = value;
            }
        }

        protected Remito(int xNumero,string xSerie, DateTime xFecha, Moneda xMoneda, ClienteActivo xCliente, List<LineaRemito> xLineas, int xCodTarifa, string xComentario, int xCodVendedor, SubCuenta xSubcuenta)
        {
            _serie = xSerie;
            _fecha = xFecha;
            _Moneda = xMoneda;
            _Cliente = xCliente;
            _Lineas = xLineas;
            _codTarifa = xCodTarifa;
            _Comentario = xComentario;
            _CodVendedor = xCodVendedor;
            _numero = xNumero;
            _IS = xSubcuenta;

        }


        public abstract int TipoDoc();



        public abstract int NumeroCFE();

        public abstract decimal Importe();
        /* {
             decimal zSuma = 0;
             foreach (LineaRemito L in _Lineas)
             {
                 zSuma += L.Total();
             }
             return decimal.Round(zSuma, 2);
         }*/

        public abstract decimal TotalBruto();
        /*{
            decimal zSuma = 0;
            foreach (LineaRemito L in _Lineas)
            {
                zSuma += L.Precio * L.Unidadestotal;
            }
            return decimal.Round(zSuma, 2);
        }*/

        public abstract decimal TotalImpuestos();
        /* {
             decimal zSuma = 0;
             foreach (LineaRemito L in _Lineas)
             {
                 zSuma += -(Math.Abs(L.Total()) - Math.Abs(L.Precio));
             }
             return zSuma;
         }*/

        public abstract decimal Costo();
        /* {
             decimal zSuma = 0;
             foreach (LineaRemito L in _Lineas)
             {
                 zSuma += L.Costo;
             }
             return Math.Abs(zSuma);
         }*/

        public abstract decimal TotalIva();
        /* {
             return this.Importe() - this.TotalBruto(); 
         }*/

        public abstract decimal TotalCostoIva();
        /* {
             decimal zSuma = 0;
             foreach (LineaRemito L in _Lineas)
             {
                 zSuma += L.Costeiva;
             }
             return Math.Abs(zSuma);
         }*/

        public abstract bool CFE();
        /*{
            bool zCFE = false;
            foreach(Movimiento M in this.Movimiento)
            {
                if (M.CFE != null)
                    zCFE = true;
                else
                    zCFE = false;
            }
            return zCFE;
        }*/


        public abstract string Adenda();
        /*{
            string Salto = "|";
            string Titulo = " **** DOCUMENTOS QUE AFECTA **** ";
            string Adenda = "";
            int Index = 0;
            foreach(Movimiento M in this.Movimiento)
            {  
                if(Index == 1)
                {
                    Index = 0;
                    Adenda = Adenda + " " + M.Serie + " " + M.Numero.ToString() + Salto;
                }
                else
                {
                    Index+=1;
                    Adenda = Adenda + " " + M.Serie + " " + M.Numero.ToString();
                }
                
            }
            return Titulo + Salto + Salto + Adenda;
        }*/

        public abstract int FormaPago();


        public abstract int TipoPago();

        public abstract char Estado();

        public abstract string GenApunte();
        public abstract int Remesa();
        public abstract int NumeroZ();
        public abstract string SerieCaja();
       
        public abstract string Sudocumento();
       
    }
}
