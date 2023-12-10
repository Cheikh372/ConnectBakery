using AutoMapper;
using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Application.Shared.Interfaces;
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
    public class OrderService : IOrderService 
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        //private readonly IUserService _userUserService;
        private readonly IMapper _mapper;

        public OrderService(IServiceProvider serviceProvider)
        {
            _orderRepository = serviceProvider.GetRequiredService<IRepository<Order>>();
            _orderItemRepository = serviceProvider.GetRequiredService<IRepository<OrderItem>>();
            //_userUserService = serviceProvider.GetRequiredService<IUserService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<Guid> Create(OrderDto order)
        {

            // control du soteck ici
            var ordreToSave = _mapper.Map<Order>(order);

            await _orderRepository.AddAsync(ordreToSave);

            var orderItemToSave = new List<OrderItem>();

            foreach(var item in order.OrderItems)
            {
                var orderItem = _mapper.Map<OrderItem>(item);
                orderItem.OrderId = ordreToSave.Id;
                orderItemToSave.Add(orderItem);
            }

            await _orderItemRepository.AddRangeAsync(orderItemToSave);
            
            await _orderRepository.SaveAsync();

            return ordreToSave.Id;
        }

        
        public async Task<Guid> Delete(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order is null)
                throw new Exception(ResponseConstant.NotFound);

            _orderRepository.Remove(order);

           
            var orderItemToRemove = _orderItemRepository.Find(o => o.OrderId == order.Id);

            if(orderItemToRemove is not null)
            {
                await _orderItemRepository.AddRangeAsync(orderItemToRemove);
            }

            await _orderRepository.SaveAsync();

            return order.Id;
        }

        public async Task<IEnumerable<OrderDto>> GetAll()
        {
            var orders = await _orderRepository.GetAllAsync();

            if (orders is null)
                return Enumerable.Empty<OrderDto>();

            var orderList = new List<OrderDto>();

            foreach ( var order in orders)
            {
                var orderItem = _orderItemRepository.Find(o => o.OrderId == order.Id);

                var orderWithItem = _mapper.Map<OrderDto>(order);
                
                if (orderWithItem is not null)
                {
                    orderWithItem.OrderItems.AddRange(_mapper.Map<List<OrderItemDto>>(orderItem));
                }

                orderList.Add(orderWithItem);
            }
            return orderList;
        }

        public async Task<OrderDto?> GetById(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order is null)
                return null;

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetByClient(Guid orderId)
        {
            var order = (await _orderRepository.GetAllAsync()).Where(o => o.ClientId == orderId);

            if (order is null)
                return Enumerable.Empty<OrderDto>();

            return _mapper.Map<IEnumerable<OrderDto>>(order);
        }

        public async Task<Guid> Update(OrderDto order)
        {
            var oldOrder = await _orderRepository.GetByIdAsync(order.Id);

            if (oldOrder is null)
                throw new Exception(ResponseConstant.NotFound);

            var newOrder = _mapper.Map<Order>(order);

            _orderRepository.Update(newOrder, oldOrder);
            
            foreach(var item in order.OrderItems)
            {
                var oldOrderItem = await _orderItemRepository.GetByIdAsync(item.Id);

                var newOrderItem = _mapper.Map<OrderItem>(item);

                if (oldOrderItem is null)
                {
                    await _orderItemRepository.AddAsync(newOrderItem);
                }
                _orderItemRepository.Update(newOrderItem, oldOrderItem);
            }

            await _orderRepository.SaveAsync();

            return order.Id;
        }

    }
}
