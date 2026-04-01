using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Interfaces
{
    internal interface IAdapter<T> 
    {
        T Get(object[] values);
    }
}
