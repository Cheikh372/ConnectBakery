using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Application.Shared.Request;
using ConnectBakery.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConnectBakery.Application.Shared.Interfaces
{
    public interface IProductService
    {
        Task<OutPutDto<ProductDto>> GetById(Guid id);
        Task<OutPutDto<ProductDto>> GetByProductType(ProductType type);
        Task<OutPutDto<IEnumerable<ProductDto>>> GetAll();

        Task<OutPutDto<Guid>> Create(ProductDto product);
        Task<OutPutDto<Guid>> Update(ProductDto product);

        Task<OutPutDto<Guid>> Delete(Guid id);

    }
}
