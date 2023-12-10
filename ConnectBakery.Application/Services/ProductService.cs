using AutoMapper;
using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Application.Shared.Interfaces;
using ConnectBakery.Application.Shared.Request;
using ConnectBakery.Common.Constantes;
using ConnectBakery.Common.Enum;
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
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IServiceProvider serviceProvider) 
        {
            _productRepository = serviceProvider.GetRequiredService<IRepository<Product>>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();

            //_context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<OutPutDto<Guid>> Create(ProductDto product)
        {
            var newproduct =_mapper.Map<Product>(product);

            await _productRepository.AddAsync(newproduct);
            await _productRepository.SaveAsync();

            return new OutPutDto<Guid> { Value = product.Id, Code = ResponseConstant.Success }; 
        }

        public async Task<OutPutDto<Guid>> Delete(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
                throw new Exception("NotFound");

            _productRepository.Remove(product);
            await _productRepository.SaveAsync();

            return new OutPutDto<Guid> { Value = product.Id, Code = ResponseConstant.Success }; 
        }

        public async Task<OutPutDto<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();

            if (products is null)
                throw new Exception("NotFound");

            var productList = _mapper.Map<IEnumerable<ProductDto>> (products);

            return new OutPutDto<IEnumerable<ProductDto>> { Value = productList, Code = ResponseConstant.Success }; 
        }

        public async Task<OutPutDto<ProductDto>> GetById(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
                throw new Exception("NotFound");

            return new OutPutDto<ProductDto> { Value = _mapper.Map<ProductDto>(product), Code = ResponseConstant.Success }; 
        }
        public async Task<OutPutDto<ProductDto>> GetByProductType(ProductType type)
        {
            var product =  (await _productRepository.GetAllAsync()).Where(e => e.ProductType == type);

            if (product is null)
                throw new Exception("NotFound");

            return new OutPutDto<ProductDto> { Value = _mapper.Map<ProductDto>(product), Code = ResponseConstant.Success }; 
        }

        public async Task<OutPutDto<Guid>> Update(ProductDto product)
        {
            var oldPrduct = await _productRepository.GetByIdAsync(product.Id);

            if (oldPrduct is null)
                throw new Exception("NotFound");

            var newProduct =  _mapper.Map<Product>(oldPrduct);
            
            _productRepository.Update(newProduct, oldPrduct);
            await _productRepository.SaveAsync();

            return new OutPutDto<Guid> { Value = newProduct.Id, Code = ResponseConstant.Success };
        }
    }
}
