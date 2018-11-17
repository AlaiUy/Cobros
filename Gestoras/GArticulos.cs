using Aguiñagalde.FabricaMappers;
using Aguiñagalde.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguiñagalde.Gestoras
{
    public class GArticulos
    {
        private static GArticulos _Instance = null;
        private static IMapperAriculos DBArticulos;
        private static readonly object padlock = new object();

        public static GArticulos getInstance()
        {
            if (_Instance == null)
            {
                lock (padlock)
                {
                    if (_Instance == null)
                        _Instance = new GArticulos();
                }
            }

            return _Instance;
        }

        public GArticulos()
        {
            DBArticulos = (IMapperAriculos)Factory.getMapper(this.GetType());
        }


        public void Descatalogar(string xCodigo)
        {
            if (!Tools.Numeros.IsNumeric(xCodigo))
                return;
            DBArticulos.Descatalogar(xCodigo);
        }

    }
}
