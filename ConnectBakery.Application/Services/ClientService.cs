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
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IMapper _mapper;
        
        public ClientService(IServiceProvider serviceProvider) 
        {
            _clientRepository = serviceProvider.GetRequiredService<IRepository<Client>>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();

            //_context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<OutPutDto<Guid>> Create(ClientDto client)
        {
            var newclient =_mapper.Map<Client>(client);

            await _clientRepository.AddAsync(newclient);
            await _clientRepository.SaveAsync();

            return new OutPutDto<Guid> { Value = newclient.Id, Code = ResponseConstant.Success };
        }

        public async Task<OutPutDto<Guid>> Delete(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client is null)
                throw new Exception(ResponseConstant.NotFound);

            _clientRepository.Remove(client);
            await _clientRepository.SaveAsync();

            return new OutPutDto<Guid> { Value = client.Id, Code = ResponseConstant.Success };
        }

        public async Task<OutPutDto<IEnumerable<ClientDto>>> GetAll()
        {
            var clients = await _clientRepository.GetAllAsync();

            if (clients is null)
                throw new Exception(ResponseConstant.NotFound);

            var clientList = _mapper.Map<IEnumerable<ClientDto>> (clients);

            return  new OutPutDto<IEnumerable<ClientDto>> { Value = clientList, Code = ResponseConstant.Success };
        }

        public async Task<OutPutDto<ClientDto>> GetById(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client is null)
                throw new Exception(ResponseConstant.NotFound);

            return new OutPutDto<ClientDto> { Value = _mapper.Map<ClientDto>(client), Code = ResponseConstant.Success };
        }

        public async Task<OutPutDto<Guid>> Update(ClientDto client)
        {
            var odlclient = await _clientRepository.GetByIdAsync(client.Id);

            if (odlclient is null)
                throw new Exception(ResponseConstant.NotFound);

            var newClient =  _mapper.Map<Client>(client);
            
            _clientRepository.Update(newClient, odlclient);
            await _clientRepository.SaveAsync();

            return new OutPutDto<Guid> { Value = client.Id, Code = ResponseConstant.Success };
        }
    }
}
