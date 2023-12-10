using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Application.Shared.Request;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectBakery.Application.Shared.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItemDto?> GetById(Guid id);
        Task<IEnumerable<OrderItemDto>> GetByOrder(Guid orderId);
        Task<IEnumerable<OrderItemDto>> GetByProductId(Guid productId);
        Task<IEnumerable<OrderItemDto>> GetAll();
        Task<Guid> Create(OrderItemDto ordreItem);
        Task<Guid> Update(OrderItemDto ordreItem);
        Task<Guid> Delete(Guid id);

    }
}
