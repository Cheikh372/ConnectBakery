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
    public interface IUserService
    {
        Task<OutPutDto<UserDto>> GetById(Guid id);
        Task<OutPutDto<UserDto>> GetByUserName(string userName);
        Task<OutPutDto<UserDto>> GetByEmail(string Email);
        Task<OutPutDto<IEnumerable<UserDto>>> GetAll();

        Task<OutPutDto<string>> Create(UserDto product);
        Task<OutPutDto<string>> Update(UserDto product);

        Task<OutPutDto<string>> Delete(Guid id);

       Task<IdentityResult> RegistrerUser(UserDto user);
    }
}
