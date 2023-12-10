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
    public class EmployeService : IEmployeService
    {
        private readonly IRepository<Employe> _employeRepository;
        //private readonly IUserService _userUserService;
        private readonly IMapper _mapper;

        public EmployeService(IServiceProvider serviceProvider)
        {
            _employeRepository = serviceProvider.GetRequiredService<IRepository<Employe>>();
            //_userUserService = serviceProvider.GetRequiredService<IUserService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<OutPutDto<Guid>> Create(EmployeDto employe)
        {
            if(_employeRepository.Find(e => e.PhoneNumber ==  employe.PhoneNumber).FirstOrDefault() is not null)
            {
                return new OutPutDto<Guid> { Object = $"Ce numéro {employe.PhoneNumber} ", Code = ResponseConstant.PhoneNumberAlreadyUsed }; 
            }

            if (_employeRepository.Find(e => e.Email == employe.Email).FirstOrDefault() is not null)
            {
                return new OutPutDto<Guid> { Object = $"Cet Email {employe.Email} ", Code = ResponseConstant.EmailAlreadyUsed };

            }
            var newemploye = _mapper.Map<Employe>(employe);

            // on l'enregistre comme user
            //if (employe.IsUser)
            //{
            //    newemploye = await CreateEmployeAsUserAsync(newemploye);
            //}

            return await CreateEmploye(newemploye);
        }

        //private async Task<Employe> CreateEmployeAsUserAsync(Employe employe)
        //{
        //    var user = new UserDto();

        //    user.Email = employe.Email;
        //    user.PhoneNumber = employe.PhoneNumber;
        //    var response = await _userUserService.RegistrerUser(user);
            
        //    if (response.Succeeded)
        //    {
        //        var userCreated = await _userUserService.GetByEmail(user.Email);

        //        if(userCreated.Value != null) 
        //        {
        //            employe.IsUser = true;
        //            employe.UserId = userCreated.Value.Id;
        //        }
               
        //    }
        //    else
        //    {
        //        employe.IsUser = false;
        //        employe.UserId = null;
        //    }

        //    return employe;
        //}
        
        
        private async Task<OutPutDto<Guid>> CreateEmploye(Employe employe)
        {
            await _employeRepository.AddAsync(employe);
            await _employeRepository.SaveAsync();

            return new OutPutDto<Guid> { Value = employe.Id, Code = ResponseConstant.Success };
        }
        
        public async Task<OutPutDto<Guid>> Delete(Guid id)
        {
            var employe = await _employeRepository.GetByIdAsync(id);

            if (employe is null)
                throw new Exception(ResponseConstant.NotFound);

            _employeRepository.Remove(employe);
            await _employeRepository.SaveAsync();

            return new OutPutDto<Guid> { Value = employe.Id, Code = ResponseConstant.Success };
        }

        public async Task<OutPutDto<IEnumerable<EmployeDto>>> GetAll()
        {
            var employes = await _employeRepository.GetAllAsync();

            if (employes is null)
                throw new Exception(ResponseConstant.NotFound);

            var employeList = _mapper.Map<IEnumerable<EmployeDto>>(employes);

            return new OutPutDto<IEnumerable<EmployeDto>> { Value = employeList, Code = ResponseConstant.Success };
        }

        public async Task<OutPutDto<EmployeDto>> GetById(Guid id)
        {
            var employe = await _employeRepository.GetByIdAsync(id);

            if (employe is null)
                throw new Exception(ResponseConstant.NotFound);

            return new OutPutDto<EmployeDto> { Value = _mapper.Map<EmployeDto>(employe), Code = ResponseConstant.Success };
        }

        public async Task<OutPutDto<Guid>> Update(EmployeDto employe)
        {
            var oldEmploye= await _employeRepository.GetByIdAsync(employe.Id);

            if (oldEmploye is null)
                throw new Exception(ResponseConstant.NotFound);

            var newEmploye = _mapper.Map<Employe>(employe);

            _employeRepository.Update(newEmploye, oldEmploye);
            await _employeRepository.SaveAsync();

            return new OutPutDto<Guid> { Value = employe.Id, Code = ResponseConstant.Success };
        }
    }
}
