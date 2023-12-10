using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectBakery.Application.Shared.Request
{
    public class IntputDto<T>  
    {
        public T? Value { get; set; }
    }
}
