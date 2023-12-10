using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Application.Shared.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectBakery.Application.Shared.Interfaces
{
    public interface IClientService
    {
        Task<OutPutDto<ClientDto>> GetById(Guid id);
        Task<OutPutDto<IEnumerable<ClientDto>>> GetAll();

        Task<OutPutDto<Guid>> Create(ClientDto product);
        Task<OutPutDto<Guid>> Update(ClientDto product);

        Task<OutPutDto<Guid>> Delete(Guid id);
    }
}
