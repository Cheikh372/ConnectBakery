using ConnectBakery.Application.Shared.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectBakery.Application.Shared.Dtos
{
    public class EmployeDto : PersonneDto
    {
        public string Qualification { get; set; } = string.Empty;
        public decimal Salaire { get; set; }
        public string? UserId { get; set; }
        //public UserDto User { get; set; }
    }
}
