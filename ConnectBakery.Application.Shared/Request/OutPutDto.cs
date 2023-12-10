using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectBakery.Application.Shared.Request
{
    public class OutPutDto<T>
    {
        public T? Value { get; set; } 
        public object Object { get; set; } 
        public string Code { get; set; } = string.Empty;
    }
}
