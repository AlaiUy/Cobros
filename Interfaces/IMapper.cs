using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguiñagalde.Interfaces
{
    public interface IMapper
    {
        void Add(object o);
        void Update(object o);
        void Delete(object o);
    }
}
