using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Application.Shared.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConnectBakery.Application.Shared.Interfaces
{
    public interface IEmployeService
    {
        Task<OutPutDto<EmployeDto>> GetById(Guid id);
        Task<OutPutDto<IEnumerable<EmployeDto>>> GetAll();
        Task<OutPutDto<Guid>> Create(EmployeDto product);
        Task<OutPutDto<Guid>> Update(EmployeDto product);
        Task<OutPutDto<Guid>> Delete(Guid id);

    }
}
