using ConnectBakery.Application.Shared.Dtos.Base;
using ConnectBakery.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectBakery.Application.Shared.Dtos
{
    public class ClientDto : PersonneDto
    {

        public ClientType ClientType { get; set; }
        public string? UserId { get; set; }
        //public UserDto User { get; set; }
       
    }
}
