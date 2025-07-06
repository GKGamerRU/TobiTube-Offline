using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobiTube_Offline.Interfaces
{
    public interface IGenerator<T>
    {
        IEnumerable<T> Generate();
    }
}
