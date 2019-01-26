using AutoMapper;
using Hospital.Model.Identity;
using Hospital.Repository.Abstract;
using Hospital.Service.Abstract;
using Hospital.Service.InDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Concrete
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(IMapper mapper, IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<bool> ChangeEmail(ChangeEmailInDTO model)
        {
            if(model == null) return false;

            var user = await _userRepository.FindAsync(model.UserId);
            if (user == null) return false;

            var result =  await _userManager.ChangeEmailAsync(user, model.NewEmail, model.Token);

            return result.Succeeded;

        }

        public async Task<bool> ChangePassword(ChangePasswordInDTO model)
        {
            if (model == null) return false;

            var user = await _userRepository.FindAsync(model.UserId);
            if (user == null) return false;
            if (model.ConfirmNewPassword != model.NewPassword) return false;
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            
            return result.Succeeded;
        }

        public async Task<bool> ChangePhoneNumber(ChangePhoneInDTO model)
        {
            if (model == null) return false;

            var user = await _userRepository.FindAsync(model.UserID);
            if (user == null) return false;

            var result = await _userManager.ChangePhoneNumberAsync(user,model.NewPhoneNumber,model.Token);

            return result.Succeeded;
        }

    }
}
