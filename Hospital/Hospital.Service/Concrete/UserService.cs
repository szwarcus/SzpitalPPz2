using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Model.Entities;
using Hospital.Model.Identity;
using Hospital.Repository.Abstract;
using Hospital.Service.Abstract;
using Hospital.Service.OutDTOs;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace Hospital.Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<ApplicationUserDTO>> GetAllUsersByRole(string roleName)
        {
            var usersByRole = await _userManager.GetUsersInRoleAsync(roleName);

            List<ApplicationUserDTO> result = usersByRole.Select(usr => new ApplicationUserDTO(new Guid(usr.Id), usr.FirstName, usr.LastName)).ToList();

            return result;
        }

    }
}