using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguiñagalde.Interfaces
{
    public interface IMapperAriculos:IMapper
    {
        void Descatalogar(string xCodigo);
    }
}
