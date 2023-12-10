using ConnectBakery.Application.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectBakery.Application.Shared.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> GetById(Guid id);
        Task<IEnumerable<OrderDto>> GetByClient(Guid clientId);
        Task<IEnumerable<OrderDto>> GetAll();
        Task<Guid> Create(OrderDto ordre);
        Task<Guid> Update(OrderDto ordre);

        Task<Guid> Delete(Guid id);
    }
}
