using ConnectBakery.Application.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectBakery.Application.Shared.Interfaces
{
    public interface IStockService
    {
        Task<StockDto> GetById(Guid id);
        Task<StockDto> GetByProduct(Guid productId);
        Task<IEnumerable<StockDto>> GetAll();
        Task<Guid> Create(StockDto ordre);
        Task<Guid> Update(StockDto ordre);

        Task<Guid> Delete(Guid id);
    }
}
