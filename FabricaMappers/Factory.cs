using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aguiñagalde.Interfaces;
using Aguiñagalde.DAL;

namespace Aguiñagalde.FabricaMappers
{
    public class Factory
    {
       
        public static IMapper getMapper(Type Class)
        {
            switch (Class.Name)
            {
                case "GClientes": return (IMapper)new ClientesMapper();

                case "GCuentas": return (IMapper)new CuentasMapper();

                case "GCobros": return (IMapper)new CobrosMapper();

                case "GArticulos": return (IMapper)new ArticulosMapper();
            }
            return null;
        }
    }
            

            
}
