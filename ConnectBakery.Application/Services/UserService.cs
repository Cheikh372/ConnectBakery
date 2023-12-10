using AutoMapper;
using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Application.Shared.Interfaces;
using ConnectBakery.Application.Shared.Request;
using ConnectBakery.Common.Constantes;
using ConnectBakery.DAL;
using ConnectBakery.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace ConnectBakery.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IServiceProvider serviceProvider)
        {
            _userRepository = serviceProvider.GetRequiredService<IRepository<User>>();
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();

            //_context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<OutPutDto<UserDto>> GetByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            return new OutPutDto<UserDto> { Value  = _mapper.Map<UserDto>(user), Object = user, Code = ResponseConstant.Success }; 
        }
        public async Task<OutPutDto<UserDto>> GetByEmail(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            return new OutPutDto<UserDto> { Value = _mapper.Map<UserDto>(user), Object = user, Code = ResponseConstant.Success };
        }
        public async Task<IdentityResult> RegistrerUser(UserDto user)
        {
            if (user is null)
                throw new Exception(ResponseConstant.Error);

            var newuser = _mapper.Map<User>(user);

            if (string.IsNullOrWhiteSpace(newuser.Id))
            {
                newuser.Id = Guid.NewGuid().ToString();
            }
            if(string.IsNullOrWhiteSpace(newuser.UserName))
            {
                newuser.UserName = newuser.Email;
            }
            var result = await _userManager.CreateAsync(newuser, "Bakery123!");

            return result;
        }

        public async Task<OutPutDto<string>> Create(UserDto user)
        {
           

            var response = await RegistrerUser(user);

            if (!response.Succeeded) 
                throw new Exception(ResponseConstant.Error);

            await _userRepository.SaveAsync();

            return new OutPutDto<string> { Object = user, Code = ResponseConstant.Success };
        }

        public async Task<OutPutDto<string>> Delete(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
                throw new Exception(ResponseConstant.NotFound);

            _userRepository.Remove(user);
            await _userRepository.SaveAsync();

            return new OutPutDto<string> { Value = user.Id, Code = ResponseConstant.Success };
        }

        public async Task<OutPutDto<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();

            if (users is null)
                throw new Exception(ResponseConstant.NotFound);

            var userList = _mapper.Map<IEnumerable<UserDto>>(users);

            return new OutPutDto<IEnumerable<UserDto>> { Value = userList, Code = ResponseConstant.Success };
        }

        public async Task<OutPutDto<UserDto>> GetById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
                throw new Exception(ResponseConstant.NotFound);

            return new OutPutDto<UserDto> { Value = _mapper.Map<UserDto>(user), Code = ResponseConstant.Success };
        }

        public async Task<OutPutDto<string>> Update(UserDto user)
        {
            var odluser = await _userRepository.GetByIdAsync(Guid.Parse(user.Id));

            if (odluser is null)
                throw new Exception(ResponseConstant.NotFound);

            var newUser = _mapper.Map<User>(user);

            _userRepository.Update(newUser, odluser);
            await _userRepository.SaveAsync();

            return new OutPutDto<string> { Value = user.Id, Code = ResponseConstant.Success };
        }

    }
}
