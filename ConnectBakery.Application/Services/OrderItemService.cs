using AutoMapper;
using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Application.Shared.Interfaces;
using ConnectBakery.Application.Shared.Request;
using ConnectBakery.Common.Constantes;
using ConnectBakery.DAL;
using ConnectBakery.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectBakery.Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItem> _orderItemRepository;
        //private readonly IUserService _userUserService;
        private readonly IMapper _mapper;

        public OrderItemService(IServiceProvider serviceProvider)
        {
            _orderItemRepository = serviceProvider.GetRequiredService<IRepository<OrderItem>>();
            //_userUserService = serviceProvider.GetRequiredService<IUserService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<Guid> Create(OrderItemDto ordreItem)
        {

            // control du soteck ici
            var ordreItemToSave = _mapper.Map<OrderItem>(ordreItem);

            await _orderItemRepository.AddAsync(ordreItemToSave);
            await _orderItemRepository.SaveAsync();

            return ordreItemToSave.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);

            if (orderItem is null)
                throw new Exception(ResponseConstant.NotFound);

            _orderItemRepository.Remove(orderItem);
            await _orderItemRepository.SaveAsync();

            return orderItem.Id;
        }

        public async Task<IEnumerable<OrderItemDto>> GetAll()
        {
            var orderItems = await _orderItemRepository.GetAllAsync();

            if (orderItems is null)
                return Enumerable.Empty<OrderItemDto>();

            var employeList = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);

            return employeList;
        }

        public async Task<OrderItemDto?> GetById(Guid id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);

            if (orderItem is null)
                return null;

            return _mapper.Map<OrderItemDto>(orderItem);
        }

        public  async  Task<IEnumerable<OrderItemDto>> GetByOrder(Guid orderId)
        {
            var orderItem =  (await _orderItemRepository.GetAllAsync()).Where(o => o.OrderId == orderId);

            if (orderItem is null)
                return Enumerable.Empty<OrderItemDto>();

            return   _mapper.Map<IEnumerable<OrderItemDto>>(orderItem);
        }

        public async Task<IEnumerable<OrderItemDto>>  GetByProductId(Guid productId)
        {
            var orderItem = (await _orderItemRepository.GetAllAsync()).Where(o => o.OrderId == productId);

            if (orderItem is null)
                return Enumerable.Empty<OrderItemDto>();

            return _mapper.Map<IEnumerable<OrderItemDto>>(orderItem);
        }

        public async Task<Guid> Update(OrderItemDto ordreItem)
        {
            var oldEmploye = await _orderItemRepository.GetByIdAsync(ordreItem.Id);

            if (oldEmploye is null)
                throw new Exception(ResponseConstant.NotFound);

            var newOrderItem = _mapper.Map<OrderItem>(ordreItem);

            _orderItemRepository.Update(newOrderItem, oldEmploye);
            await _orderItemRepository.SaveAsync();

            return ordreItem.Id;
        }

      
    }
}
