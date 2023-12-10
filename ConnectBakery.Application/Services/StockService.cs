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
    public class StockService : IStockService
    {
        private readonly IRepository<Stock> _stockRepository;
        //private readonly IUserService _userUserService;
        private readonly IMapper _mapper;

        public StockService(IServiceProvider serviceProvider)
        {
            _stockRepository = serviceProvider.GetRequiredService<IRepository<Stock>>();
            //_userUserService = serviceProvider.GetRequiredService<IUserService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<Guid> Create(StockDto stock)
        {

            // control du soteck ici
            var stockToSave = _mapper.Map<Stock>(stock);

            var stockExist = await _stockRepository.GetByIdAsync(stockToSave.ProductId); // si produit exist déja dans le stock
            if(stockExist is not null)
            {
                stockToSave.Quantity += stockExist.Quantity;
                _stockRepository.Update(stockToSave,stockExist);

            }
            else
            {
                await _stockRepository.AddAsync(stockToSave);

            }

            await _stockRepository.SaveAsync();

            return stockToSave.Id;
        }


        public async Task<Guid> Delete(Guid id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock is null)
                throw new Exception(ResponseConstant.NotFound);

            _stockRepository.Remove(stock);

            await _stockRepository.SaveAsync();

            return stock.Id;
        }

        public async Task<IEnumerable<StockDto>> GetAll()
        {
            var stocks = await _stockRepository.GetAllAsync();

            if (stocks is null)
                return Enumerable.Empty<StockDto>();

            var orderList = _mapper.Map<List<StockDto>>(stocks);

            return orderList;
        }

        public async Task<StockDto?> GetById(Guid id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock is null)
                return null;

            return _mapper.Map<StockDto>(stock);
        }

        public async Task<StockDto> GetByProduct(Guid productId)
        {
            var stock = (await _stockRepository.GetAllAsync()).FirstOrDefault(o => o.ProductId == productId);

            return _mapper.Map<StockDto>(stock);
        }

        public async Task<Guid> Update(StockDto stock)
        {
            var oldStock = await _stockRepository.GetByIdAsync(stock.Id);

            if (oldStock is null)
                throw new Exception(ResponseConstant.NotFound);

            var newStock = _mapper.Map<Stock>(stock);

            _stockRepository.Update(newStock, oldStock);

           
            await _stockRepository.SaveAsync();

            return stock.Id;
        }

    }
}
